using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private float xBounds = 5f;
    private float zBound = 150f;
    public GameObject[] vehiclePrefabs = new GameObject[4];
    public GameObject crateStack;

    private int levelNumber = 1;
    private float vehicleSpeed = 10f;
    private float vehicleSpawnDelay = 3.0f;

    public Text levelNumberText;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnVehicle", 0.1f, vehicleSpawnDelay);
        Instantiate(crateStack, crateStack.transform.position, crateStack.transform.rotation);
        //Debug.Log("Wavenumber " + waveNumber);
        //StartCoroutine(SpawnVehicleWave(1));
    }

    // Update is called once per frame
    void Update()
    {
                
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
        levelNumber++;
        levelNumberText.text = "Level: " + levelNumber;
        vehicleSpeed += 5f;
        vehicleSpawnDelay *= 0.8f;
    }

    // Generate a spawn position
    private Vector3 GenerateSpawnPosition()
    {
        return new Vector3(Random.Range(-xBounds, xBounds), 0, zBound);
    }

    public void LevelUp()
    {
        Cleanup();
        IncreaseDifficulty();
        InvokeRepeating("SpawnVehicle", 0.1f, vehicleSpawnDelay);
    }

    public void ResetGame()
    {
        // Cleanup objects
        Cleanup();
        
        // Reset variables
        levelNumber = 1;
        levelNumberText.text = "Level: " + levelNumber;
        vehicleSpeed = 10.0f;
        vehicleSpawnDelay = 3.0f;

        // Restart the invoke
        InvokeRepeating("SpawnVehicle", 0.1f, vehicleSpawnDelay);
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
