using CommandTerminal;
using Game.Scripts.Model;
using UniRx;
namespace Game.Scripts.Controller
{
    public class Cheats
    {
        [RegisterCommand(Name = "cs2000", Help = "Gives 2000 cash", MinArgCount = 0, MaxArgCount = 0)]
        static void GiveCash(CommandArg[] args)
        {
            Service<DatabusInventory>.Get().cash += 2000;
        }

        [RegisterCommand(Name = "cs100", Help = "Removes 4100 cash", MinArgCount = 0, MaxArgCount = 0)]
        static void RemoveCash(CommandArg[] args)
        {
            Service<DatabusInventory>.Get().cash -= 100;
        }

        [RegisterCommand(Name = "clrinv", Help = "Clears inventory", MinArgCount = 0, MaxArgCount = 0)]
        static void ClearInventory(CommandArg[] args)
        {
            ReactiveCollection<InventoryItem> inventoryItems = Service<DatabusInventory>.Get().inventoryItems;
            inventoryItems.ClearCollection();
        }
    }
}