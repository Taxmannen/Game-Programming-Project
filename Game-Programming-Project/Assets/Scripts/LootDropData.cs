using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Data", menuName = "Custom/Loot Data", order = 51)]
public class LootDropData : ScriptableObject
{
    [SerializeField] private Item[] items = new Item[3];

    public void DropItem()
    {
        Debug.Log("Drop Item");
    }

    public Item[] Items
    {
        get { return items; }
    }

    [Serializable]
    public struct Item
    {
        public string item;
        public int dropChance;
    }
}
