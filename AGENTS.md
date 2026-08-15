# AGENTS.md

## 项目概述

**Blasphemous.Framework.Stats**（Stats Framework）—— 一个 Blasphemous（《渎神》）的 ModdingAPI 框架 Mod。它让其他 mod 通过 **JSON 配置文件**读写游戏内数据：忏悔者（Penitent）属性、敌人属性、背包物品（祷告/念珠/圣剑之心/遗物）的 effects 与 hit 数值。框架运行时用 Harmony 把配置值应用到游戏对象。

技术栈：C#（`net35`，`LangVersion=latest`）、Unity 2017.4.40f1、BepInEx 5、Harmony 2、Newtonsoft.Json。依赖：`Blasphemous.ModdingAPI 2.4.1`、`Blasphemous.Framework.UI 0.1.2`、`Blasphemous.CheatConsole 1.0.1`、`Blasphemous.ModdingReferences 4.0.67`（后三者来自 `NuGet.Config` 中的 `nuget.bepinex.dev` 源）。

## 构建与验证

```bash
dotnet build Blasphemous.Framework.Stats.sln -c Debug
```

- 构建产物：`Blasphemous.Framework.Stats/bin/Debug/StatsFramework.dll`（`TargetName=StatsFramework`）。
- 编译警告几乎全是 `CS1591`（公共成员缺 XML 注释，`GenerateDocumentationFile=True` 所致），属预期，可忽略。
- **没有自动化测试项目**。验证手段 = 构建成功 + 游戏内实测（CheatConsole 命令 + `ModLog` 输出）。
- csproj 的 `Development` target 在构建后把 DLL 和 `resources/` 拷入 Steam 的 `Modding/` 目录并打包 `publish/StatsFramework.zip`；路径硬编码为本机 Steam 目录，换机器需改。

## 目录结构

| 路径 | 职责 |
|---|---|
| `Main.cs` | BepInEx 入口（`Main`）。含全部 Harmony `Traverse` 反射读写工具方法（`GetValue`/`SetValue`/`SetValueIfNotNull`/`SetValueIfValidated`/`Validate`） |
| `StatsFramework.cs` | `BlasMod` 主类。`OnRegisterServices` 注册命令、加载 JSON、配置 JsonSerializerSettings；`OnLevelLoaded` 触发 `PatchController.PatchAllStats()` |
| `StatsPatching/` | **核心：JSON 可序列化的数据 patch 体系**（见下节） |
| `Components/` | 支撑接口与基类：`IStatsPatchable`、`IAccessible_Class/_Struct/_Polymorphic`、`BaseStatsPatch`、`EquipmentLoadout`、`ItemCollection<T>`、`SerializableVector3` |
| `Patches/` | 运行时控制器 + Harmony patch。`PatchController`（按类型批量应用 patch）、`HitPatchController` + `HitPatchData`（伤害计算）、`ItemPatches/`（祈祷特效重写）、`MechanicsPatches/`（机制修复） |
| `Commands/` | CheatConsole 命令：`statspatch`（list/activate/deactivate + DEBUG 下 exportjson）、`penitentstats`（DEBUG 下 export/importjson） |
| `Extensions/` | `InventoryManagerExtensions`（物品 ID → 对象、loadout 存取）、`ModHitExtensions`（自定义 Hit 注入）、`FileHandlerExtensions`/`ConfigHandlerExtensions`（带 JsonSerializerSettings 的读写）、`ModCommandExtensions` |
| `resources/data/Stats Framework/` | DEBUG 构建加载的 test patch JSON（`test_patch_*.json`），也是 JSON 格式范本 |

## 核心概念：Stats Patch 数据流

**三类 patch**，均继承 `BaseStatsPatch`（字段：`name`、`activeType`、`activeFlag`、`isActive`）：

- `PenitentStatsPatch` → `List<PenitentData>`（忏悔者属性）
- `EnemyStatsPatch` → `List<EnemyData>`（按 `entityId` 匹配的敌人属性）
- `InventoryItemStatsPatch` → `List<InventoryItemData>`（按 `itemId` 匹配的物品 effects / fervourCost / 继承设置）

**数据类模式**：每个 Data 类实现 `IAccessible_Class<T>`（`GetValueFrom`/`SetValueTo`）+ `IStatsPatchable`（`TryGetTargets`/`GetValueFromFirstTarget`/`SetValueToAllTargets`）。`GetValueFrom` 用于把游戏对象当前值读入 JSON 结构（导出），`SetValueToAllTargets` 用于把 JSON 值写回游戏对象（应用）。内部再组合 SubComponents（`AttributeValues`、`EntityStatsValues_Penitent/_Enemy`、`PlatformCharacterControllerValues_*`、`ObjectEffectValues` 及 Beads/Prayers/SwordHearts/Relics 派生类）。

**生命周期**：

1. 其他 mod 在 `OnRegisterServices` 调 `provider.RegisterStatsPatch(patch)`（`StatsPatchRegister` 扩展方法；按 `name` 去重，重复注册直接忽略）。
2. DEBUG 构建下框架自身从 `resources/data/Stats Framework/test_patch_*.json` 加载 4 个测试 patch（`StatsFramework.OnRegisterServices`）。
3. 每次加载游戏场景 → `StatsFramework.OnLevelLoaded` → `PatchController.PatchAllStats()`。
4. 每个 patch 先 `UpdateActive()` 按 `activeType` 刷新 `isActive`（`Unconditional` 恒真 / `OnFlag` 查 `Core.Events.GetFlag` / `Manually` 靠控制台或代码）。
5. 激活的 patch 对目标执行 `SetValueToAllTargets()`；物品 patch 失活时执行 `RevertValueToAllTargets()`（`InventoryItemData.isApplied` 防止重复应用）。

## 编码约定

- **访问私有成员一律用 `Main.GetValue<T>` / `Main.SetValue*`**（内部是 Harmony `Traverse`），不要直接反射或 `Traverse.Create` 散写。nullable 写入用 `SetValueIfNotNull`，条件校验用 `SetValueIfValidated`。
- **JSON 多态**：`JsonSerializerSettings` 必须带 `TypeNameHandling.Objects` + `StringEnumConverter`（见 `StatsFramework.OnRegisterServices`）。JSON 里每个 effect 需要 `"$type": "<完整类型名>, StatsFramework"`。新增 effect 派生类要同时加入 `InventoryItemData.scriptTypeToJsonType` 映射（vanilla MonoBehaviour 类型 ↔ JSON 类型），否则序列化/反序列化失败。
- **物品 ID 前缀约定**（`GetItemTypeFromId`）：`RE`=遗物、`RB`=念珠、`QI`=任务物品、`PR`=祷告、`CO`=收集品、`HE`=剑。
- **公共 API 写 XML 注释**（`<inheritdoc/>` 也行）；新公共类型缺注释会新增 CS1591 警告。
- **`#if DEBUG` 块**：仅 DEBUG 下存在（测试 patch 加载、导出命令、详尽日志）。别把 DEBUG 代码放进 Release 路径。
- **`net35` 限制**：语言特性（record、集合表达式、target-typed new）编译器允许，但运行时只能用 net35 存在的 BCL API；涉及 Unity/游戏内部 API 时按游戏版本（Unity 2017.4）查文档。
- 局部函数、现代 C# 语法在该仓库很常见，保持风格一致。

## 已知坑（改代码前先看）

- **物品 effect 的增删 = 操作 `MonoBehaviour`**：`SetValueTo` 用 `AddComponent` 加 mod effect、`Destroy` 删 vanilla effect（删除匹配逻辑 `ObjectEffectValuesMatchEvaluator`：同类型且非空字段全匹配才删）；`RevertValueTo` 反着做。改动时注意 `vanillaEffectsToIsDeleted` 与 `modMonoBehaviors` 状态一致性。
- **敌人 patch 只对当前场景存活的敌人生效**（`Entity.LivingEntities` + `FindObjectsOfType<Enemy>`）。新场景加载会重新 PatchAllStats。
- **祷告特效 patch**（`Patches/ItemPatches/PrayerHitPatches.cs`：PR03/PR07/PR09/PR12/PR14）是 Harmony **重写整个 `OnApplyEffect`** 的 hack（prefix 返回 false 接管原逻辑，按 `HitPatchController.Hits.*` 的 `IsActive` 决定是否生效）。改这些文件必须理解原版协程/调用链，否则会破坏祷告施放。
- **PR03 的 tint 材质**在第一个游戏场景加载时初始化（`StatsFramework.OnLevelLoaded` 调 `PR03_HitPatch.GetTintMaterial()`）——不要把这个调用挪到更早时机。
- **伤害计算在 `HitPatchController`**：`CalculateFinalDamageWithBonuses(base, multiplier, damageBonuses…)` = `(base + Σ bonus×系数) × multiplier`；祷告伤害乘 `CalculatePrayerDamageMultiplier(prayerBonusEfficiency)`。`HitPatchData.FinalDamage` 是现成的组合公式。
- `PatchItemStats` 里 loadout 保存/重穿的代码被**注释掉**（曾经为了安全替换装备），别误以为 patch 会脱装备。
- `MechanicsPatches/AttackSpeedUncapPatches.cs` 整体被注释（WIP），不要取消注释就提交。

## Git 工作流

- 开发在 `experimental` 分支；`main` 通过 PR 合入。
- **commit / push / PR 前必须先征求用户批准**（用户硬性要求，禁止自行 commit）。
- commit message 一律**全英文**（subject + body），格式遵循 conventional commits（`feat:`/`fix:`/`refactor:` 等，仓库历史即如此）。
