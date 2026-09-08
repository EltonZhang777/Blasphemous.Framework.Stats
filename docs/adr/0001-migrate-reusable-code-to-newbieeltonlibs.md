# 复用代码迁移到 Blasphemous.NewbieEltonLibs 包

本仓库（Stats Framework）原先手写维护了一套与作者自有库 `Blasphemous.NewbieEltonLibs` 重复的通用 API（Harmony Traverse 工具、FileHandler/ConfigHandler/ModCommand/InventoryManager 扩展、CheatConsole 命令样板）。决定：删除本地重复实现，以 nuget 包依赖 lib，mod 强耦合部分（loadout 存取、UnityEngineIgnoreConverter、JsonSerializerSettings 组装、EntityOrientationToDirectionalVector）保留在本仓库。理由：多 mod 共享一份实现，修 bug 一次到位、行为一致；代价是发布需同步携带 lib DLL（已在构建流程处理）。

Status: accepted

## Consequences

- 本仓库依赖 `Blasphemous.NewbieEltonLibs`（nuget 0.2.1），发布产物必须包含该依赖 DLL（csproj Development target 已复制）。
- 修改通用扩展方法需在 lib 仓库进行并发版，本仓库升级版本号；不再直接编辑本地副本。
- 待定项（SerializableVector3、ItemCollection、EntityOrientationToDirectionalVector、loadout 存取、UnityEngineIgnoreConverter、JsonSettings 工厂）是否并入 lib 由 issue #2 评估决定。
