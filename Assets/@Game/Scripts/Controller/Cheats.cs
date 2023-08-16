using CommandTerminal;
using Game.Scripts.Model;
namespace Game.Scripts.Controller
{
    public class Cheats
    {
        [RegisterCommand(Name = "cash+200", Help = "Gives 200 cash", MinArgCount = 0, MaxArgCount = 0)]
        static void GiveCash(CommandArg[] args)
        {
            Service<DatabusInventory>.Get().cash += 200;
        }

        [RegisterCommand(Name = "cash-100", Help = "Removes 100 cash", MinArgCount = 0, MaxArgCount = 0)]
        static void RemoveCash(CommandArg[] args)
        {
            Service<DatabusInventory>.Get().cash -= 100;
        }
    }
}