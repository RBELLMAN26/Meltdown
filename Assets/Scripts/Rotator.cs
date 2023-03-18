using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    bool startCountdown;
    float countdown = 3;
    [SerializeField] float rotationSpeed;
    [SerializeField] int rotationDirection = 1; //Set as 1 or -1;
    float changeTime = 3, maxChangeTime = 3;

    private void OnEnable()
    {
        GameManager.startGameEvent += StartGame;
    }

    private void OnDisable()
    {
        GameManager.startGameEvent -= StartGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCountdown)
        {
            if(countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                rotationSpeed += Time.deltaTime;
                if(changeTime > 0)
                {
                    changeTime -= Time.deltaTime;
                }
                else
                {
                    if(Random.Range(0,2) == 0)
                    {
                        rotationDirection *= -1;
                    }
                    changeTime = maxChangeTime;
                }
                Rotate();
            }
        }
    }

    void StartGame()
    {
        startCountdown = true;
        GetComponent<AudioSource>().Play();
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up, (rotationSpeed * rotationDirection) * Time.deltaTime);
    }
}
