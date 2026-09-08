# Spec 0002 — T7 待定 API 归属评估与落地计划

> 状态：draft（T7 / issue #9 产物，本地 spec，gitignore 不进 git）。
> 关联：Spec 0001（`0001-migrate-to-newbieeltonlibs.md`）、issue #2（6 项候选清单）、issue #9（T7 落地）。

## Problem Statement

Spec 0001 迁移后，6 项候选 API 的归属（并入 `Blasphemous.NewbieEltonLibs` 或留在本仓库）仍未定案，且各 API 的使用现状未经核查。T7 需要：逐项评估通用性与耦合度、给出"入 lib / 留本仓库 / 删除"结论，并制定落地流程，使本仓库与 lib 的边界最终确定。

## Solution

基于代码事实逐项评估 6 项 API，形成归属结论；对"入 lib"项，在 lib 仓库实现并发版（0.3.x），本仓库随后升引用版本并移除本地实现；对"留本仓库"项保持现状；对死代码直接删除。评估结果与落地流程记录为本 spec，供后续执行 agent 与 lib 仓库实施参照。

## 评估结论

| # | API | 结论 | 依据（代码事实） |
|---|---|---|---|
| 1 | `SerializableVector3` | **入 lib** | 1 处活跃使用（念珠效果 `minionOffsetToPenitent`）；仅依赖 UnityEngine，无 mod 耦合，通用序列化需求 |
| 2 | `ItemCollection<T>` | **删除** | 全仓库 0 处使用（internal 死代码），无保留价值 |
| 3 | `EntityOrientationToDirectionalVector` | **入 lib（GameLibs 命名空间）** | 1 处活跃使用（PR07 祷告 patch）；依赖游戏类型 `EntityOrientation`，符合 lib GameLibs 扩展定位 |
| 4 | `EquipmentLoadout` + loadout 存取 | **留本仓库** | 无活跃调用（仅 `PatchItemStats` 注释代码引用）；属于本 mod 特有的"安全替换装备"流程，且 Q5 已决策保留 |
| 5 | `UnityEngineIgnoreConverter` | **入 lib** | DEBUG 导出命令使用；通用序列化需求（忽略 UnityEngine 引用类型），低耦合 |
| 6 | JsonSerializerSettings 组装工厂 | **入 lib** | 本仓库 2 处重复组装（TypeNameHandling.Objects + StringEnumConverter + ReferenceLoopHandling.Ignore）；通用配置，lib 应提供统一工厂/常量 |

## User Stories

1. 作为 mod 维护者，我希望 `SerializableVector3` 来自 lib，以便其他 mod 直接复用同一序列化包装。
2. 作为 mod 维护者，我希望 `EntityOrientationToDirectionalVector` 进入 lib GameLibs 命名空间，以便其他涉及朝向的 patch 复用。
3. 作为 mod 维护者，我希望 `UnityEngineIgnoreConverter` 进入 lib，以便序列化导出场景统一忽略 UnityEngine 类型。
4. 作为 mod 维护者，我希望 lib 提供 JsonSerializerSettings 组装工厂，以便消除本仓库 2 处重复配置。
5. 作为 mod 维护者，我希望 `ItemCollection<T>` 被删除，以便清理零使用死代码。
6. 作为 mod 维护者，我希望 loadout 相关 API 保持在本仓库，以便不把 mod 特有的装备替换流程泛化进 lib。
7. 作为 mod 维护者，我希望"入 lib"项的替换在 lib 发版后执行，以便不破坏现有引用（先有包、后移除本地实现）。
8. 作为 mod 维护者，我希望每项落地后构建与导出功能实测正常，以便迁移无回归。

## Implementation Decisions

- **入 lib 项**（SerializableVector3、EntityOrientationToDirectionalVector、UnityEngineIgnoreConverter、JsonSettings 工厂）：由 lib 仓库（`Blasphemous.NewbieEltonLibs`）实现并发布新版本（0.3.x）。实现归属建议：`SerializableVector3` / `UnityEngineIgnoreConverter` / JsonSettings 工厂 → lib `Extensions.ModdingAPI` 或独立 `Serialization` 命名空间；`EntityOrientationToDirectionalVector` → lib `Extensions.GameLibs`（随游戏类型扩展）。
- **本仓库落地顺序**（blocked by lib 发版）：
  1. 升引用 `Blasphemous.NewbieEltonLibs` 到 0.3.x；
  2. 删除本仓库 `SerializableVector3` / `EntityOrientationToDirectionalVector`（Main.cs 内）/ `UnityEngineIgnoreConverter` 本地实现，调用点改用 lib；
  3. 2 处 JsonSerializerSettings 组装改用 lib 工厂；
  4. 删除 `ItemCollection<T>`（0 使用）。
- **留本仓库项**：`EquipmentLoadout` + `SaveCurrentEquipmentAsLoadout` / `EquipItemsInLoadout` 保持现状（当前无活跃调用，若未来启用"安全替换装备"流程再评估）。
- **不新增**：本 spec 不引入新的第三方依赖；lib 新增 API 遵循其 net35 + Nullable 约定与 XML 文档要求。

## Testing Decisions

- 沿用 Spec 0001 的双 seam：构建 seam（`dotnet build Blasphemous.Framework.Stats.sln -c Debug` 0 errors，CS1591 忽略）+ 游戏内 seam（`statspatch` / `penitentstats` 命令与 patch 应用实测）。
- 每项落地（含 lib 发版后替换）以构建通过 + 受影响功能实测为完成标准；无自动化测试项目（仓库既有约定）。
- 好测试的标准：只验证外部行为（命令输出、patch 生效、导出 JSON 内容），不验证实现细节。

## Out of Scope

- lib 仓库侧的实现细节（新 API 的代码、发版流程、CI）——由 lib 仓库 agent/维护者完成。
- 评估过程中对 lib 现有 API 的改动。
- 本 spec 不生成 GitHub issue（按用户要求仅本地输出至 `docs/specs/`）；若需追踪可在 lib 仓库另行开票。

## Further Notes

- 事实核查时间点：`SerializableVector3` 1 处使用、`ItemCollection<T>` 0 使用、loadout 仅注释引用、`UnityEngineIgnoreConverter` 仅 DEBUG 命令使用、`EntityOrientationToDirectionalVector` 1 处使用（PR07）。
- lib 当前版本 0.2.1（HEAD 一致）；入 lib 项需 lib 发版后才能替换。
- 落地后更新本仓库 `CONTEXT.md`（已内嵌 AGENTS.md 领域词汇节）与 ADR：若 `ItemCollection`/`EntityOrientationToDirectionalVector` 移出，需修订词汇引用。
