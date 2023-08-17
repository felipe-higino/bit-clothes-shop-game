using UnityEngine;
namespace Game.Scripts.Controller
{
    public static class DomainConversion
    {
        const string itemIcons = "Sets/{0}";

        public static Sprite SpriteFromItemName(this string itemName)
        {
            Sprite sprite = Resources.Load<Sprite>(string.Format(itemIcons, itemName));
            return sprite;
        }
    }
}