using System;
using UnityEngine;
#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor.AssetImporters;
#endif

namespace Game.Scripts.Model
{
    public class SO_Animations : ScriptableObject
    {
        public Animations[] animations;
    }

    [Serializable]
    public class Animations
    {
        public string animationControllerName;
        public string setItemName;
        public string weaponItemName;
    }

#if UNITY_EDITOR
    [ScriptedImporter(2, "animationsCSV")]
    public class AnimationsImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            string text = File.ReadAllText(ctx.assetPath);
            string[] lines = text.Split('\n');

            List<Animations> animationsList = new();
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] data = line.Split(',');
                Animations row = new()
                {
                    animationControllerName = data[0],
                    setItemName = data[1],
                    weaponItemName = data[2]
                };
                animationsList.Add(row);
            }

            SO_Animations scriptableObject = ScriptableObject.CreateInstance<SO_Animations>();
            scriptableObject.animations = animationsList.ToArray();
            ctx.AddObjectToAsset("mainasset", scriptableObject);
            ctx.SetMainObject(scriptableObject);
        }
    }
#endif
}