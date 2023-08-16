using System;
using UnityEngine;
#if UNITY_EDITOR
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using UnityEditor.AssetImporters;
#endif
namespace Game.Scripts.Model
{
    public class SO_Store : ScriptableObject
    {
        public Store[] storeItems;
    }

    [Serializable]
    public class Store
    {
        public string itemName;
        public int price;
        public ItemType itemType;
    }

    public enum ItemType
    {
        SET,
        WEAPON
    }

#if UNITY_EDITOR
    [ScriptedImporter(2, "storeCSV")]
    public class StoreImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            string text = File.ReadAllText(ctx.assetPath);
            string[] lines = text.Split('\n');

            List<Store> storeList = new();
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] data = line.Split(',');
                Store row = new()
                {
                    itemName = data[0],
                    price = int.Parse(data[1], CultureInfo.InvariantCulture),
                    itemType = Enum.Parse<ItemType>(data[2], true)
                };
                storeList.Add(row);
            }

            SO_Store scriptableObject = ScriptableObject.CreateInstance<SO_Store>();
            scriptableObject.storeItems = storeList.ToArray();
            ctx.AddObjectToAsset("mainasset", scriptableObject);
            ctx.SetMainObject(scriptableObject);
        }
    }
#endif
}