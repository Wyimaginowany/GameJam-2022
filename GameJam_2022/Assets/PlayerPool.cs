using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPool : MonoBehaviour
{

    [SerializeField] GameObject[] players;

    int currentPlayer = 0;

    private void Start()
    {
        SpawnNextPlayer();
    }

    public void SpawnNextPlayer()
    {
        if (currentPlayer <= players.Length - 1)
        {
            players[currentPlayer].SetActive(true);
        }
        else
        {
            Debug.Log("Show end screen");
        }

        currentPlayer++;
    }

}
