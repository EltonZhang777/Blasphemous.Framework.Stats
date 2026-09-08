# Stats Framework Context

本文件是 `Blasphemous.Framework.Stats` 的 single-context 领域词汇与边界说明。`AGENTS.md` 负责工作流、构建和编码规则；本文件负责领域概念的单一事实源。

## Stats Patch

**Stats Patch** 是一个 JSON 可序列化的数据修改单元。它包含名称、激活条件（`activeType` / `activeFlag`）和目标数据列表；`isActive` 是运行时状态，不是配置值的替代名称。

三种 patch 类型：

- `PenitentStatsPatch`：修改忏悔者（Penitent）属性。
- `EnemyStatsPatch`：按 `entityId` 匹配并修改敌人属性。
- `InventoryItemStatsPatch`：按 `itemId` 匹配并修改背包物品的 effects、fervour cost 和相关设置。

`BaseStatsPatch` 提供共同的 `name`、`activeType`、`activeFlag` 和 `isActive` 字段。`Data` 类型负责定位目标并读写目标值；`SubComponent` 类型负责读写目标内部的一组字段。

使用 **Stats Patch**，不要使用“配置项”或“属性覆盖”指代整个 patch。

## Patch lifecycle

1. Mod 在 `OnRegisterServices` 中通过 `RegisterStatsPatch` 注册 patch；按 `name` 去重。
2. `UpdateActive()` 按激活类型刷新 `isActive`：`Unconditional` 恒为真，`OnFlag` 查询游戏 flag，`Manually` 由控制台或代码控制。
3. 每次场景加载后，`StatsFramework.OnLevelLoaded` 调用 `PatchController.PatchAllStats()`。
4. 激活 patch 调用 `SetValueToAllTargets()`；物品 patch 失活时调用 `RevertValueToAllTargets()`，并用 `InventoryItemData.isApplied` 避免重复应用。

## Traverse Utils

**Traverse Utils** 是 `Blasphemous.NewbieEltonLibs.Extensions.GameLibs.TraverseUtils`，用于通过 Harmony Traverse 读取和写入游戏字段或属性。常用 API 为 `GetValue`、`SetValue`、`SetValueIfNotNull` 和 `Validate`。

本仓库访问游戏私有成员统一使用 Traverse Utils。新增访问逻辑应复用该工具，不应散写直接反射或 `Traverse.Create`。

## AutoModCommand

**AutoModCommand** 是 `Blasphemous.NewbieEltonLibs` 提供的 CheatConsole 命令基类。子命令使用 `ModSubCommand` attribute 声明，由基类生成 help 并校验参数长度。本仓库的 `statspatch` 和 `penitentstats` 命令采用此模式。

## Loadout

**Loadout** 是忏悔者当前装备组合：念珠槽位、圣剑之心、祷告和遗物槽位。`SaveCurrentEquipmentAsLoadout` 保存组合，`EquipItemsInLoadout` 重新装备组合。

Loadout 存取与 `UnityEngineIgnoreConverter`、本仓库的 `JsonSerializerSettings` 组装属于 mod 强耦合逻辑，不能仅因通用性而视为 `NewbieEltonLibs` 的替代 API。

## Item ID prefix

物品 ID 前缀用于推断物品类型：

| 前缀 | 类型 |
| --- | --- |
| `RE` | 遗物（Relic） |
| `RB` | 念珠（Bead） |
| `QI` | 任务物品（Quest item） |
| `PR` | 祷告（Prayer） |
| `CO` | 收集品（Collectible） |
| `HE` | 圣剑之心（Sword heart） |

代码中使用 `GetItemTypeFromId` 的既有约定；不要在调用点重复硬编码另一套类型判断。

## Effects and Hits

**Effect** 是挂在物品或祷告对象上的游戏 `MonoBehaviour` 行为；**Hit** 是一次伤害或命中数据。Effect 可以触发 Hit，但两者不是同义词。

物品 effect 的增删会操作 `MonoBehaviour`：应用时可能 `AddComponent` 并删除匹配的 vanilla effect，恢复时执行反向操作。维护 `vanillaEffectsToIsDeleted` 与 `modMonoBehaviors` 的对应关系，避免重复添加或错误恢复。

JSON 中 effect 的生命周期字段属于 effect values 的顶层；`hitData` 只描述伤害相关字段。`effectType`、`triggerOnlyOnce`、`useWhenCastingPrayer` 等字段放入 `hitData` 不会配置 effect 生命周期。

## Hit Patch

**Hit Patch** 是针对一次伤害计算的修改数据。`HitPatchData` 可包含 `baseDamage`、`attackDamageMultiplier`、`basePrayerDamage`、`prayerBonusEfficiency` 和 `hitValues`；`HitPatchController` 计算最终伤害并将其注入祷告 Hit。

普通伤害组合为：

`(base + Σ(damageBonus × coefficient)) × multiplier`

祷告伤害另乘 `CalculatePrayerDamageMultiplier(prayerBonusEfficiency)`。`HitPatchData.FinalDamage` 是现成的组合结果。`IsActive` 决定对应 Hit Patch 是否参与运行时效果。

## Domain boundaries

- `Stats Patch` 描述 JSON 驱动的数据应用单元；`Hit Patch` 只描述伤害计算，不替代完整的 Stats Patch。
- `Traverse Utils` 是访问游戏内部成员的工具边界；`AutoModCommand` 是命令声明的工具边界。
- `Loadout`、`Effect`、`Hit` 和 `Prayer.EffectTime` 分别表示装备组合、对象行为、伤害数据和祷告时序字段，不能混用。
