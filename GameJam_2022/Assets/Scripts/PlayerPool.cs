using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPool : MonoBehaviour
{

    [SerializeField] GameObject[] players;
    [SerializeField] TextMeshProUGUI playerLifesText;
    [SerializeField] GameObject can;
    [SerializeField] GameObject endLights;

    int currentPlayer = 0;
    int playerLifes;

    private void Start()
    {
        playerLifes = players.Length - 1;
        SpawnNextPlayer();
    }

    public void SpawnNextPlayer()
    {
        if (playerLifes == 0)
        {
            can.SetActive(false);
            endLights.SetActive(true);
        }
        else
        {
            playerLifesText.text = (playerLifes).ToString() + " x";
        }

        if (currentPlayer <= players.Length - 1)
        {
            players[currentPlayer].SetActive(true);
        }
        else
        {
            Debug.Log("Show end screen");
        }

        playerLifes--;
        currentPlayer++;
    }

}
