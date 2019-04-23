using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Data", menuName = "Custom/Loot Data", order = 51)]
public class LootDropData : ScriptableObject
{
    [SerializeField] private ItemStruct[] items = new ItemStruct[3];
    
    public void DropItem(Transform player)
    {
        foreach (ItemStruct drop in items)
        {
            int randomDrop = UnityEngine.Random.Range(1, 101);
            if (randomDrop > drop.dropChance)
            {
                player.GetComponent<PlayerInventory>().AddItem(Resources.Load<GameObject>("Items/" + drop.item));
                return;
            }
        }
    }

    public ItemStruct[] Items
    {
        get { return items; }
    }

    [Serializable]
    public struct ItemStruct
    {
        public string item;
        public int dropChance;
    }
}