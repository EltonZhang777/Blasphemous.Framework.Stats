# Spec 0003 — PR03 祷告持续时间（冷却）无法通过 JSON 修改（诊断 + 修复设计）

> 状态：diagnosed（2025-09 调试遗留 bug 复盘定位，本地 spec，gitignore 不进 git）。
> 来源：grilling 三轮推演（2025-09 桌面调试目录 `20250830 渎神1 数据魔改mod 修bug用祷文数据对比` 四个 JSON 复盘）。

## Problem Statement

用户曾以 4 个 JSON（`PR03_vanilla.json` + 3 份内容相同的 patched）作为补丁加载，期望修改 PR03 的数值，结果"均不生效"。经复盘定位，用户真实目标为：**修改 PR03 的施放持续时长/冷却时间（`Prayer.EffectTime`）从 2.0s 到 4.0s**（PR03 为瞬时祷文，持续时长仅表现为"施放后冷却"），并可能连带修改"施放后无敌/停止费尔蒙收集"时长（同为 `ItemTemporalEffectValues.effectTime = 2.0`）。探针实验（改伤害）对其他祷文（PR12/PR14）有效、对 PR03 无效，佐证问题特定于 PR03 的时长通道。

## 根因链（三重叠加，全部经源码验证）

1. **根因 A — 文件格式错位**：框架 DEBUG 加载走 `FileHandler.LoadDataAsJson<InventoryItemStatsPatch>("test_patch_prayer.json", ...)`，顶层必须是 `InventoryItemStatsPatch`（`name`/`activeType`/`statsPatches`）。桌面 JSON 顶层 `$type` 为 `InventoryItemData`、无 patch 包装，且改动写在只读的 `vanillaEffects` 内 → 加载失败或静默无效。
   - 证据：`StatsFramework.cs:65-68`（硬编码 4 个 test_patch 文件）、`test_patch_prayer.json` 顶层结构。
2. **根因 B — 改错字段（vanillaEffects 只读）**：`InventoryItemData.SetValueTo` 只处理 `inheritenceSettings` / `fervourCost` / `effectDeletions`（删除）/ `effectAdditions`（AddComponent）。`vanillaEffects` 仅是导出参照与 deletions 匹配基准，**从不写回**。
   - 证据：`InventoryItemData.cs:240-313`（`SetValueTo` 主体）。
3. **根因 C — 框架无写 `Prayer.EffectTime` 的路径（核心缺口）**：
   - 游戏侧：`Prayer.Awake()` 遍历所有 OnUse+LimitTime 的 `ObjectEffect`，取最大 `EffectTime` 初始化只读属性 `Prayer.EffectTime`（`{ get; private set; }`），此后无任何刷新路径；`PrayerUse.StartUsingPrayer()` 以 `prayer.EffectTime` 作为施放时长（`timeToEnd`）。
   - 框架侧：`SetValueTo` 对 Prayer 仅写了 `fervourNeeded`（fervourCost），**没有写 `Prayer.EffectTime` 的分支**；即便通过 `effectAdditions` 添加新时间效果组件，`Prayer.EffectTime` 也不会重算。
   - PR03 伤害探针无效的独立原因：PR03 走 `PR03_HitPatch`（Harmony prefix 接管 `OnApplyEffect`，伤害来自 `PatchController.Hits.PR03`），其 `IsActive = AnyActiveModificationOf<PenitentLightBeamEffectValues>()` 只查 `effectAdditions`；探针 JSON 的 `effectAdditions` 为空 → 永不激活 → 走原版。
   - 证据：解包源码 `Framework/Inventory/Prayer.cs`（Awake/EffectTime）、`Gameplay/GameControllers/Penitent/Abilities/PrayerUse.cs`（StartUsingPrayer）、`Tools/Items/PenitentLightBeamEffect.cs`（OnApplyEffect）；本仓库 `HitPatchController.cs:15-21`、`PrayerHitPatches.cs:36-37`。

## 修复设计（分步实现，本轮不写码）

### Step 1 — 冷却/施放持续时长（用户主诉求，最小改动）

- `InventoryItemData` 新增 `public float? prayerDuration;`，与 `fervourCost` 对称：
  - `GetValueFrom`：`_vanillaPrayerDuration = prayer.EffectTime`（存原值，供 Revert）。
  - `SetValueTo`：当 `prayerDuration.HasValue && obj is Prayer p` 时，`Traverse.Create(p).Property("EffectTime").SetValue(prayerDuration.Value)`；附 DEBUG 日志（如 `"Setting Prayer {id} EffectTime to {v}"`）。
  - `RevertValueTo`：`Traverse.Create(p).Property("EffectTime").SetValue(_vanillaPrayerDuration)`。
  - 位置选择依据：`PatchAllStats` 于每次 `OnLevelLoaded` 执行 → 每个场景都会重新写入，可覆盖物品重建后 `Awake()` 重算的值；Revert 与 `_vanillaFervourCost` 模式一致。
  - 实现注意：`EffectTime` 为 private setter 自动属性；Traverse `Property("EffectTime")` 应可访问 private setter，若失败则 fallback 写后备字段 `<EffectTime>k__BackingField`。

### Step 2 — 施放后无敌/停止费尔蒙收集时长（可后续分步）

- 该时长 = `ItemTemporalEffectValues` 组件（`temporalEffects: [StopFervourRecolection, Invulnerable]`，OnUse，`limitTime: true`，`effectTime: 2.0`）的 `ObjectEffect.EffectTime`。
- `vanillaEffects` 只读 → 修改既有组件须用"删除 + 重建"组合：`effectDeletions`（按 `ObjectEffectValuesMatchEvaluator` 全字段匹配原组件）删除旧组件，`effectAdditions` 添加 `ItemTemporalEffectValues`（`effectTime` 为新值、`temporalEffects` 同上）。
- 建议配套机制：`PatchItemStats` 应用物品 patch 后，对目标 Prayer **重算** `Prayer.EffectTime = max(所有 OnUse+LimitTime 效果的 EffectTime)`（复刻 `Prayer.Awake` 逻辑），使"通过 effectAdditions 加时间效果自动延长冷却"也成立——这是当前框架的另一处隐性缺口。
- 依赖关系：Step 2 的"重算"机制与 Step 1 的"直接写入"二选一或共存均可；推荐 Step 1 直接写入（精确、可独立 Revert），Step 2 重算作为通用兜底。

## 测试 patch 设计（正确格式）

文件置于 `resources/data/Stats Framework/test_patch_prayer.json`（DEBUG 构建自动加载；或复制至 Modding 对应目录），PR03 条目顶层必须是 `InventoryItemStatsPatch`：

```json
{
  "$type": "Blasphemous.Framework.Stats.StatsPatching.ItemStats.InventoryItemStatsPatch, StatsFramework",
  "name": "test_patch_pr03_duration",
  "activeType": "Unconditional",
  "statsPatches": [
    {
      "itemId": "PR03",
      "prayerDuration": 4.0
    }
  ]
}
```

- 注意：现有 `test_patch_prayer.json` 的 PR12/PR14 条目用 `"activeType": "Manually"`（需 `statspatch activate` 生效）；测试时建议 Unconditional 或 Manually+activate 均可，`statspatch list` 可确认激活状态。
- 伤害探针（若要验证 PR03 伤害通道）：须把 `PenitentLightBeamEffectValues` 放入 `effectAdditions`（而非 vanillaEffects），才会令 `AnyActiveModificationOf<PenitentLightBeamEffectValues>` 为真。

## 验收标准（Q9-C：行为 + 日志双确认）

1. 行为：施放 PR03 后，到能再次施放的等待时间由原版 2.0s 变为 4.0s（Step 1 完成时）；无敌/停止费尔蒙时长若一并修改则同样验证。
2. 日志：DEBUG 构建下 ModLog 输出写入后的 `Prayer.EffectTime` 值（Step 1 的 `"Setting Prayer PR03 EffectTime to 4"`）；`Prayer.Awake()` 的 `"Prayer PR03 Time:..."` 日志可作为原值基线。
3. 回归：既有 PR12/PR14 伤害 patch 不受影响（构建 0 errors + 游戏内实测仍生效）。

## Out of Scope

- 本轮（Q7=d）不修改任何框架代码；本 spec 为后续实现提供依据。
- 桌面 4 个 JSON 的 3 份 patched 内容相同属调试期粘贴/命名残留，无额外信息。
- 光束 areaPrefab 本身的存在时长（场景 prefab 配置）不在 JSON 可控范围，不做处理。
