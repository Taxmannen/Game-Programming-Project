using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Data", menuName = "Custom/Loot Data", order = 51)]
public class LootDropData : ScriptableObject
{
    [SerializeField] private ItemStruct[] items = new ItemStruct[3];
    
    public void DropItem(Transform player)
    {
        Retry:
            int itemRandom = UnityEngine.Random.Range(0, items.Length);
            ItemStruct drop = items[itemRandom];
            int dropChanceRandom = UnityEngine.Random.Range(1, 101);
            if (dropChanceRandom <= drop.dropChance)
            {
                player.GetComponent<PlayerInventory>().AddItem(Resources.Load<GameObject>("Items/" + drop.item));
                return;
            }
            else goto Retry;
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