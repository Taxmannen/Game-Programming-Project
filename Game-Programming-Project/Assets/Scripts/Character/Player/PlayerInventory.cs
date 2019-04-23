using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Image image;
    [SerializeField] private Sprite empty;
    [SerializeField] private GameObject testItem;

    private GameObject item;

    private void Start()
    {
        if (testItem != null) AddItem(testItem);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Use Item" + " " + gameObject.name)) UseItem();
    }

    public void AddItem(GameObject newItem)
    {
        if (item == null)
        {
            item = newItem;
            image.sprite = item.GetComponent<Item>().sprite;
        }
    }

    private void UseItem()
    {
        if (item != null)
        {
            GameObject newItem = Instantiate(item, transform.position, Quaternion.identity);
            newItem.GetComponent<Item>().SetPlayers(transform, GetComponent<PlayerStats>().OtherPlayer);
            image.sprite = empty;
            item = null;
        }
    }
}
