using Game.Scripts.View;
using UnityEngine;
namespace Game.Scripts.Controller
{
    public static class DomainConversion
    {
        public const string defaultName = "default";
        const string itemIcons = "Sets/{0}";
        const string animations = "Animations/{0}";

        public static Sprite SpriteFromItemName(this string itemName)
        {
            Sprite sprite = Resources.Load<Sprite>(string.Format(itemIcons, itemName));
            return sprite;
        }

        public static CharacterAnimation AnimationFromName(this string animationName)
        {
            CharacterAnimation animation = Resources.Load<CharacterAnimation>(string.Format(animations, animationName));
            return animation;
        }
    }
}