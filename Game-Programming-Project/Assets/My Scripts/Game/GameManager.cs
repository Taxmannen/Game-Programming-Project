using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool runThis;

    [Header("Setup")]
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private GameObject singleplayerCamera;
    [SerializeField] private GameObject multiplayerCamera;

    private void Start()
    {
        if (runThis)
        {
            player1.position = new Vector3(-5, 0.8f, 0);
            player2.position = new Vector3(5, 0.8f, 0);
            player2.localScale = new Vector3(-1, 1, 1);

            singleplayerCamera.SetActive(false);

            Instantiate(multiplayerCamera).GetComponent<Camera>().SetTargetTransform(player1, player2);
            

            SpriteRenderer sr1 = player1.GetComponent<SpriteRenderer>();
            SpriteRenderer sr2 = player2.GetComponent<SpriteRenderer>();
            sr1.color = new Color32(255, 165, 165, 255);
            sr2.color = new Color32(165, 255, 165, 255);
            sr1.sortingOrder = 20;
            sr2.sortingOrder = 25;  
        }
    }
}