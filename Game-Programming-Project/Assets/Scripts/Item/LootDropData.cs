using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Data", menuName = "Custom/Loot Data", order = 51)]
public class LootDropData : ScriptableObject
{
    [SerializeField] private ItemStruct[] items = new ItemStruct[3];
    
    public void DropItem(Vector2 position, Transform player, Transform otherPlayer)
    {
        foreach (ItemStruct drop in items)
        {
            int randomDrop = UnityEngine.Random.Range(1, 101);
            if (randomDrop > drop.dropChance)
            {
                GameObject currentItem = Instantiate(Resources.Load<GameObject>("Items/" + drop.item));
                currentItem.GetComponent<Item>().SetPlayers(player, otherPlayer);
                currentItem.transform.position = position;
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