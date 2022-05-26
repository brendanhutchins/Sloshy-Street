using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] forwardCarPrefabs;
    public GameObject[] incomingCarPrefabs;
    public GameObject bottlePrefab;

    private float spawnRangeY = 3.8f;

    private float spawnPosX = 12f;
    private float spawnPosY;

    private float bSpawnPosX = 12f;
    private float bSpawnPosY;

    private float startDelay = 3f;

    public float minWait = 0.5f;
    public float maxWait = 3f;

    private bool isSpawning;

    private void Awake()
    {
        isSpawning = true;
        StartCoroutine(StartDelay());
    }

    private void Update()
    {
        if (!isSpawning)
        {
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnRandomCar", timer);
            isSpawning = true;
        }
    }

    private void SpawnRandomCar()
    {
        // Randomly generate if car is incoming or going away and what type it is on its list
        int carType = Random.Range(0, 2);
        int carIndex = Random.Range(0, forwardCarPrefabs.Length);

        // Generate a random position on the right side for the car to spawn
        spawnPosX += Random.Range(-0.8f, 3f);

        // If going away from the player
        if (carType == 0)
        {
            spawnPosY = Random.Range(0.8f, spawnRangeY);
            Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY);
            Instantiate(forwardCarPrefabs[carIndex], spawnPos, Quaternion.identity);
        }
        // If going toward the player
        else if (carType == 1)
        {
            spawnPosY = Random.Range(-spawnRangeY, -1f);
            Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY);
            Instantiate(incomingCarPrefabs[carIndex], spawnPos, Quaternion.identity);
        }

        isSpawning = false;
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        isSpawning = false;
    }

    public void SpawnBeer()
    {
        StartCoroutine(BeerDelay());
    }

    IEnumerator BeerDelay()
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
        bSpawnPosX = 12.0f;
        bSpawnPosY = Random.Range(-spawnRangeY, spawnRangeY);

        Vector3 spawnPos = new Vector3(bSpawnPosX, bSpawnPosY);
        Instantiate(bottlePrefab, spawnPos, Quaternion.identity);

        StartCoroutine(BeerDelay());
    }
}
