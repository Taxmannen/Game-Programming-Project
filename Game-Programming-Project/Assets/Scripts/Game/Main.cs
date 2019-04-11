using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public LootDropData lootDropData;

    private void Update()
    {
        if (Input.GetButtonDown("Restart")) SceneManager.LoadScene("Main");

        if (Input.GetKeyDown(KeyCode.K))
        {
            LootDropData data;
            data = Resources.Load<LootDropData>("Loot Drop Data/Low Drop Chance");
            data.DropItem();
        }
    }
}
