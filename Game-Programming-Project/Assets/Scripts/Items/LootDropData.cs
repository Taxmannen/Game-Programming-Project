using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Data", menuName = "Custom/Loot Data", order = 51)]
public class LootDropData : ScriptableObject
{
    [SerializeField] private Item[] items = new Item[3];
    
    public void DropItem(Vector2 position)
    {
        foreach (var drop in items)
        {
            int randomDrop = UnityEngine.Random.Range(1, 101);
            if (randomDrop > drop.dropChance)
            {
                GameObject currentItem = Instantiate(Resources.Load<GameObject>("Items/" + drop.item));
                currentItem.transform.position = position;
                return;
            }
        }
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