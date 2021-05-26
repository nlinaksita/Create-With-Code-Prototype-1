using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float xBounds = 5f;
    private float zBound = 150f;
    private Vector3 vehicleRotation = new Vector3(0, 180, 0);
    public GameObject[] vehiclePrefabs = new GameObject[4];
    public GameObject crateStack;

    private int waveNumber = 1;
    private int enemyCount = 0;
    private float vehicleSpeed = 10f;
    private float vehicleSpawnDelay = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnVehicle", 2.0f, vehicleSpawnDelay);
        Instantiate(crateStack, crateStack.transform.position, crateStack.transform.rotation);
        //Debug.Log("Wavenumber " + waveNumber);
        //StartCoroutine(SpawnVehicleWave(1));
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    private IEnumerator SpawnVehicleWave(int waveNumber)
    {
        int i = 0;
        // Spawn a wave of incoming vehicles
        while (i < waveNumber)
        {
            // Allow for vehicleSpawnDelay seconds of delay in between each vehicle spawn
            //Invoke("SpawnVehicle", vehicleSpawnDelay);
            yield return new WaitForSeconds(vehicleSpawnDelay);
            SpawnVehicle();
            i++;
        }
        
        IncreaseDifficulty();
    }

    // Spawns the actual vehicle
    private void SpawnVehicle()
    {
        GameObject vehicle = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length - 1)];
        vehicle.GetComponent<IncomingCar>().vehicleSpeed = vehicleSpeed;
        Instantiate(vehicle, GenerateSpawnPosition(), vehicle.transform.rotation);
        //enemyCount++;
    }

    // Increase the wave number if all the vehicles in the wave have been destroyed
    private void IncreaseDifficulty()
    {
        waveNumber++;
        vehicleSpeed += 2f;
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

    public void LevelUp()
    {
        Cleanup();
        IncreaseDifficulty();
        InvokeRepeating("SpawnVehicle", 2.0f, vehicleSpawnDelay);
    }

    public void ResetGame()
    {
        // Cleanup objects
        Cleanup();
        
        // Reset variables
        waveNumber = 1;
        vehicleSpeed = 10.0f;
        vehicleSpawnDelay = 3.0f;

        // Restart the invoke
        InvokeRepeating("SpawnVehicle", 2.0f, vehicleSpawnDelay);
    }

    public void Cleanup()
    {
        // Stop the current invoke
        CancelInvoke();

        // Reset crates
        GameObject crates = GameObject.FindGameObjectWithTag("Obstacle");
        Destroy(crates);
        Instantiate(crateStack, crateStack.transform.position, crateStack.transform.rotation);

        // Remove all spawned incoming vehicles
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
