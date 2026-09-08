# Spec 0001 — 复用代码迁移到 Blasphemous.NewbieEltonLibs

> 状态：draft（grill-with-docs 敲定）。追踪 issue：见 GitHub issue（本 spec 发布时创建）。

## Problem Statement

本仓库（Blasphemous.Framework.Stats）手写了大量与作者自己的通用库 `Blasphemous.NewbieEltonLibs` 重复的 API：Harmony Traverse 反射工具（约 231 处调用点）、FileHandler / ConfigHandler / ModCommand / InventoryManager 扩展方法、CheatConsole 命令样板。这些代码与作者其他 mod 各自维护一份，修 bug 要改多处，行为也不一致。作者希望以依赖包形式统一到 lib，本仓库只保留与 mod 强耦合的部分。

## Solution

以 nuget 包依赖 `Blasphemous.NewbieEltonLibs 0.2.1`，分三批替换本仓库的复用代码：

1. **扩展层**：FileHandler / ConfigHandler / ModCommand / InventoryManager 四组扩展方法整体换用 lib 同名类（删除本地版本，迁移调用点）。
2. **命令层**：`statspatch` / `penitentstats` 两个 CheatConsole 命令改用 lib 的 `AutoModCommand` + `ModSubCommandAttribute`（attribute 声明子命令、自动生成 help、参数长度自动校验）。
3. **Traverse 工具层**：`Main.*` 工具方法全部替换为 `TraverseUtils`（约 231 处调用点），`Main` 类瘦身为纯 BepInEx 入口。

同时：修复发布流程依赖 DLL 缺失；`#if DEBUG` 中纯日志改用 `IfDebugBuild` 系列；建立 ADR 与 glossary；6 项待定 API 由独立 agent 评估是否并入 lib。

## User Stories

1. 作为 mod 维护者，我希望本仓库不再维护与 lib 重复的 Traverse 工具方法，以便修一次工具 bug 即可惠及所有 mod。
2. 作为 mod 维护者，我希望 FileHandler / ConfigHandler 扩展与 lib 保持一致，以便行为（含 AssetBundle 加载、nullable 处理）与其他 mod 完全一致。
3. 作为 mod 维护者，我希望 InventoryManager 查询扩展统一走 lib，以便获得其 `Try*` 变体与 `OfType` 变体能力。
4. 作为 mod 维护者，我希望命令定义改为 attribute 风格，以便去掉手写 `AddSubCommands()` 样板与手写 help 的重复维护。
5. 作为 mod 维护者，我希望命令的 help 输出保留现有信息量（子命令说明与 usage 占位），以便用户不受迁移影响。
6. 作为 mod 维护者，我希望 DEBUG 子命令（exportjson 等）仍只在 DEBUG 构建出现，以便 Release 不暴露调试入口。
7. 作为 mod 维护者，我希望 `#if DEBUG` 中的纯日志改用运行时 debug 检测，以便减少编译期分支散落。
8. 作为 mod 维护者，我希望 `#if DEBUG` 中的调试逻辑（序列化对比输出等）保持原样，以便调试信息不受影响。
9. 作为 mod 维护者，我希望 loadout 保存/重穿、UnityEngineIgnoreConverter、JsonSerializerSettings 组装保留在本仓库，以便这些与 mod 强耦合的代码不被误泛化。
10. 作为 mod 维护者，我希望 `EnsureDirectoryExists`、`SetValueIfValidated`（无调用）被删除，以便清理死代码。
11. 作为 mod 维护者，我希望发布 zip 与开发部署包含 `NewbieEltonLibs.dll`，以便游戏内不因缺依赖而加载失败。
12. 作为 mod 维护者，我希望迁移分批提交、每批独立构建验证，以便出错可定位回滚。
13. 作为 mod 维护者，我希望 6 项候选 API（SerializableVector3、ItemCollection、EntityOrientationToDirectionalVector、loadout 存取、UnityEngineIgnoreConverter、JsonSettings 工厂）的"是否并入 lib"决策被文档化并交给独立 agent 评估，以便决策可追踪。
14. 作为未来的读者，我希望迁移决策记录为 ADR，以便理解"为何本仓库依赖这个包"。
15. 作为未来的读者，我希望领域术语（stats patch、traverse utils、loadout、AutoModCommand）记录在 CONTEXT.md，以便统一语言。

## Implementation Decisions

- **依赖方式**：nuget 包 `Blasphemous.NewbieEltonLibs 0.2.1`（不采用 ProjectReference）。lib 本地 HEAD 与 0.2.1 一致。
- **分批替换顺序**：扩展层（FileHandler / ConfigHandler / ModCommand / InventoryManager）→ 命令层（AutoModCommand 重构）→ Traverse 工具层（Main.* → TraverseUtils）。每批独立 `dotnet build` 验证 + 独立 commit（commit 前征求用户批准，message 全英文 conventional commits）。
- **扩展层硬约束**：lib 与本仓库存在同名扩展类，两个命名空间并存会 CS0104 歧义，故替换 = 删除本仓库版本 + 迁移全部调用点（调用点集中在两个命令类、StatsFramework 主类、InventoryItemData）。
- **命令层**：`ModSubCommandAttribute` 的 `Usage` / `Description` 保留原 help 信息；DEBUG-only 子命令以 `#if DEBUG` 包裹 attribute 声明；参数校验由 `ValidLengths` 自动包装。
- **Traverse 层**：`Main.GetValue/SetValue/SetValueIfNotNull/Validate` → `TraverseUtils` 对应方法（注意 lib 返回 `TValue?`、`GetValue/SetValue` 默认 `accessType=Field`）；`Main.TraverseAccessType` 枚举引用同步指向 `TraverseUtils.TraverseAccessType`；`Main` 工具方法全部删除。
- **日志策略**：`#if DEBUG` 中**纯日志**调用换 `ModLogExtensions.*IfDebugBuild`；其余 `#if DEBUG` 调试逻辑保留原样。
- **保留本仓库**：`SaveCurrentEquipmentAsLoadout` / `EquipItemsInLoadout`、`UnityEngineIgnoreConverter`、JsonSerializerSettings 组装。
- **删除**：`EnsureDirectoryExists`（唯一调用方随 ConfigHandlerExtensions 迁移而消失）、`Main.SetValueIfValidated`（0 调用）。
- **部署修复**：csproj 的 Development target（复制到 Steam Modding/plugins 与 publish zip）补复制 `NewbieEltonLibs.dll` 依赖。
- **领域文档**：`docs/adr/0001-*.md` 记录"复用代码迁移到 NewbieEltonLibs 包"；`CONTEXT.md` 建立 glossary（stats patch、traverse utils、loadout、AutoModCommand 等）。
- **待定项交办**：SerializableVector3、ItemCollection、EntityOrientationToDirectionalVector、loadout 存取、UnityEngineIgnoreConverter、JsonSettings 工厂 —— 以 GitHub issue（本仓库，`ready-for-agent`）列出清单，独立 agent 评估通用性/耦合度并贴建议，用户勾选"入 lib / 留本仓库"，入 lib 项在 lib 仓库实现。

## Testing Decisions

- 本仓库**无自动化测试项目**（AGENTS.md 既有约定），不新增测试 seam。
- 验证 seam 为两层：**构建 seam**（`dotnet build Blasphemous.Framework.Stats.sln -c Debug` 必须 0 errors；CS1591 警告属预期）+ **游戏内 seam**（CheatConsole 命令 `statspatch list/activate/deactivate`、`penitentstats exportjson/importjson` + ModLog 输出）。
- 每个批次的完成标准：构建通过 + 对应命令/数据流游戏内实测正常（扩展层影响 patch 应用、命令层影响控制台交互、Traverse 层影响全部数据读写路径）。
- 好测试的标准：只验证外部行为（命令输出、patch 生效、日志），不验证实现细节。

## Out of Scope

- lib（Blasphemous.NewbieEltonLibs）自身的功能开发——除 6 项待定 API 的评估与实现（由独立 agent 在 lib 仓库处理）。
- ModdingAPI / GameLibs 等依赖升级。
- 祷告/物品 patch 的业务逻辑改动（PrayerHitPatches、InventoryItemData 的 MonoBehaviour 增删逻辑仅做调用点迁移，不改语义）。
- `AttackSpeedUncapPatches`（WIP，保持注释状态）。
- ModdingAPI 上游与 CheatConsole 上游修改。

## Further Notes

- 包 `Blasphemous.NewbieEltonLibs 0.2.1` 在 nuget.org 与 nuget.bepinex.dev 均可还原，当前构建通过。
- git 规则：commit / push / PR 前必须先征求用户批准；commit message 全英文 conventional commits。
- 领域文档（ADR / CONTEXT.md）按 `docs/agents/domain.md` 的 single-context 布局；发布 issue 按 `docs/agents/issue-tracker.md`（gh CLI）与 `docs/agents/triage-labels.md`（默认五 labels）。
