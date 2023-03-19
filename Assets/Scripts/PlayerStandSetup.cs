using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandSetup : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform[] playerStands;

    private void OnEnable()
    {
        GameManager.startGameEvent += SpawnPlayer;
    }

    private void OnDisable()
    {
        GameManager.startGameEvent -= SpawnPlayer;
    }

    public void SpawnPlayer()
    {
        Transform tempPlayer = Instantiate(player, GetRandPlayerStand().position + (Vector3.up * 0.6f), player.rotation);
    }

    public Transform GetRandPlayerStand()
    {
        return playerStands[Random.Range(0, playerStands.Length)];
    }

    public Transform GetPlayerStand(int index)
    {
        return playerStands[index];
    }
}
