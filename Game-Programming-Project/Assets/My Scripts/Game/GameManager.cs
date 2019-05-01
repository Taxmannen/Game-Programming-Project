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
    [SerializeField] private GameObject playerMarker;

    private void Start()
    {
        if (runThis)
        {
            player1.position = new Vector3(-5, 0.8f, 0);
            player2.position = new Vector3(5, 0.8f, 0);
            player2.localScale = new Vector3(-1, 1, 1);
            singleplayerCamera.SetActive(false);

            //Camera Setup
            CameraManager cm = Instantiate(multiplayerCamera).GetComponent<CameraManager>();
            cm.SetTargetTransform(player1, player2);

            //Marker Setup
            GameObject marker1 = Instantiate(playerMarker);
            GameObject marker2 = Instantiate(playerMarker);
            marker1.layer = LayerMask.NameToLayer("Player 1 Camera");
            marker2.layer = LayerMask.NameToLayer("Player 2 Camera");
            marker1.GetComponent<Marker>().SetCameraAndPlayer(cm.GetCamera(1), player2);
            marker2.GetComponent<Marker>().SetCameraAndPlayer(cm.GetCamera(2), player1);

            //Player Setup
            SpriteRenderer sr1 = player1.GetComponent<SpriteRenderer>();
            SpriteRenderer sr2 = player2.GetComponent<SpriteRenderer>();
            sr1.material.SetColor("_MainColor", new Color32(255, 165, 165, 255));
            sr2.material.SetColor("_MainColor", new Color32(165, 255, 165, 255));
            sr1.sortingOrder = 20;
            sr2.sortingOrder = 25;  
        }
    }
}