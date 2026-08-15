using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blasphemous.Framework.Stats.Components;

internal class EquipmentLoadout
{
    public List<string> beads;
    public string swordHeart;
    public string prayer;
    public List<string> relics;

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine("Equipment Loadout:");
        sb.AppendLine($"Beads: {string.Join(", ", (beads ?? new List<string>()).ToArray())}");
        sb.AppendLine($"Sword Heart: {swordHeart ?? "None"}");
        sb.AppendLine($"Prayer: {prayer ?? "None"}");
        sb.AppendLine($"Relics: {string.Join(", ", (relics ?? new List<string>()).ToArray())}");
        return sb.ToString();
    }
}
