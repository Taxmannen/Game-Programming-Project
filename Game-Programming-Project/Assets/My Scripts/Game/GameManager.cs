using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool runThis;

    [Header("Setup")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject singleplayerCamera;
    [SerializeField] private GameObject multiplayerCamera;

    private void Start()
    {
        if (runThis)
        {
            player1.transform.position = new Vector3(-5, 0.8f, 0);
            player2.transform.position = new Vector3(5, 0.8f, 0);
            player2.transform.localScale = new Vector3(-1, 1, 1);

            singleplayerCamera.SetActive(false);
            multiplayerCamera.SetActive(true);

            SpriteRenderer sr1 = player1.GetComponent<SpriteRenderer>();
            SpriteRenderer sr2 = player2.GetComponent<SpriteRenderer>();
            sr1.color = new Color32(255, 165, 165, 255);
            sr2.color = new Color32(165, 255, 165, 255);
            sr1.sortingOrder = 20;
            sr2.sortingOrder = 25;  
        }
    }
}