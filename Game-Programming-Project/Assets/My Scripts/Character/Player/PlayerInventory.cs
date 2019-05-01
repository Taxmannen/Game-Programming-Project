using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Image image;
    [SerializeField] private Sprite empty;
    [SerializeField] private GameObject testItem;

    private PlayerStats ps;
    private GameObject item;

    private void Start()
    {
        if (testItem != null) AddItem(testItem);
        ps = GetComponent<PlayerStats>();
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
            image.sprite = item.GetComponent<Powerup>().sprite;
        }
    }

    private void UseItem()
    {
        if (item != null)
        {
            //Säkrar så en spelare inte kan teleportera sig bakåt
            if (item.name.Contains("Teleport") && ps.DistanceToGoal < ps.OtherPlayersDistanceToGoal)
            {
                item = null;
                AddItem(Resources.Load<GameObject>("Items/Restore Item"));
            }
            else
            {
                GameObject newItem = Instantiate(item, transform.position, Quaternion.identity);
                newItem.GetComponent<Powerup>().SetPlayers(transform, GetComponent<PlayerStats>().OtherPlayer);
                image.sprite = empty;
                item = null;
            }
        }
    }
}
