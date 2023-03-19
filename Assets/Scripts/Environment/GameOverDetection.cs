using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }
    }
}
