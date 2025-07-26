using Blasphemous.CheatConsole;
using Gameplay.UI.Widgets;
using System.Linq;
using System.Text;

namespace Blasphemous.Framework.Stats.Extensions;

internal static class ModCommandExtensions
{
    internal static ConsoleWidget GetConsoleWidget(this ModCommand command)
    {
        return Main.GetValue<ConsoleWidget>(command, "console", Main.TraverseAccessType.Field);
    }

    internal static bool ValidateParameterList(this ModCommand modCommand, string[] parameters, params int[] validParameterLengths)
    {
        if (!validParameterLengths.Contains(parameters.Length))
        {
            StringBuilder sb = new();
            sb.Append($"This command takes ");
            for (int i = 0; i < validParameterLengths.Length; i++)
            {
                sb.Append($"{validParameterLengths[i]} ");
                if (i != validParameterLengths.Length - 1)
                    sb.Append("or ");
            }
            sb.Append($"parameters.  You passed {parameters.Length}");
            modCommand.GetConsoleWidget().Write(sb.ToString());

            return false;
        }

        return true;
    }
}
