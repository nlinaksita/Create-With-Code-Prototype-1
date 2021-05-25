using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float xBounds = 5f;
    private float zBound = 160f;
    private Vector3 vehicleRotation = new Vector3(0, 180, 0);
    public GameObject[] vehiclePrefabs = new GameObject[4];
    private int waveNumber = 1;
    private int enemyCount = 0;
    private float vehicleSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnVehicleWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0)
        {
            IncreaseDifficulty();
            SpawnVehicleWave(waveNumber);
        }
    }

    void SpawnVehicleWave(int waveNumber)
    {
        // Spawn a wave of incoming vehicles
        for (int i = 0; i < waveNumber; i++)
        {
            GameObject vehicle = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length - 1)];
            vehicle.GetComponent<IncomingCar>().vehicleSpeed = vehicleSpeed;
            Instantiate(vehicle, GenerateSpawnPosition(), vehicle.transform.rotation);
            enemyCount++;
        }
    }

    // Increase the wave number if all the vehicles in the wave have been destroyed
    private void IncreaseDifficulty()
    {
        waveNumber++;
        vehicleSpeed += 10f;
    }

    // Generate a spawn position
    private Vector3 GenerateSpawnPosition()
    {
        return new Vector3(Random.Range(-xBounds, xBounds), 0, zBound);
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;
    }
}
