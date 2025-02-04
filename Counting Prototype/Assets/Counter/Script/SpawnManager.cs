using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    private float yPos = 27f;
    private float xRange = 21f;

    private float startDelay = 3f;
    private float rateDelay;

    void Start()
    {
        rateDelay = Random.Range(2f, 4f);
        InvokeRepeating("SpawnBall", startDelay, rateDelay);
    }
    private void SpawnBall()
    {
        float horizontalRange = Random.Range(-xRange, xRange);
        Vector3 spawnPos = new Vector3(horizontalRange, yPos, 0);
        Instantiate(ball, spawnPos, ball.transform.rotation);
    }
    private void FixedUpdate()
    {
        rateDelay = Random.Range(2f, 8f);
    }
}
