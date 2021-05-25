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
    private float vehicleSpawnDelay = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        //SpawnVehicleWave(waveNumber);
        StartCoroutine(SpawnVehicle());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0)
        {
            IncreaseDifficulty();
            //StartCoroutine(SpawnVehicleWave(waveNumber));
            StartCoroutine(SpawnVehicle());
        }
    }

    private void SpawnVehicleWave(int waveNumber)
    {
        // Spawn a wave of incoming vehicles
        for (int i = 0; i < waveNumber; i++)
        {
            // Allow for vehicleSpawnDelay seconds of delay in between each vehicle spawn
            //StartCoroutine(SpawnVehicle());
            Invoke("SpawnVehicle", vehicleSpawnDelay);
            enemyCount++;
            //yield return new WaitForSeconds(vehicleSpawnDelay);
        }
    }

    // Spawns the actual vehicle
    private IEnumerator SpawnVehicle()
    {
        GameObject vehicle = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length - 1)];
        vehicle.GetComponent<IncomingCar>().vehicleSpeed = vehicleSpeed;
        Instantiate(vehicle, GenerateSpawnPosition(), vehicle.transform.rotation);
        enemyCount++;
        yield return new WaitForSeconds(vehicleSpawnDelay);
    }

    // Increase the wave number if all the vehicles in the wave have been destroyed
    private void IncreaseDifficulty()
    {
        //waveNumber++;
        vehicleSpeed += 5f;
        vehicleSpawnDelay *= 0.9f;
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
