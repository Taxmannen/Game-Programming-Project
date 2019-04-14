﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Players { singleplayer, multiplayer }

    [Header("State")]
    public Players players;

    [Header("Debug")]
    public bool runThis;

    [Header("Setup")]
    public GameObject player1;
    public GameObject player2;
    public GameObject singleplayerCamera;
    public GameObject multiplayerCamera;

    private void Start()
    {
        if (runThis)
        {
            if (players == Players.singleplayer)
            {
                player1.transform.position = new Vector3(0, -2.2f, 0);
                player1.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                player2.SetActive(false);
                singleplayerCamera.SetActive(true);
                multiplayerCamera.SetActive(false);
            }
            else
            {
                player1.transform.position = new Vector3(-4, -2.2f, 0);
                player2.transform.position = new Vector3(4, -2.2f, 0);
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
}