using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingCar : MonoBehaviour
{
    public float vehicleSpeed = 50f;
    private float zBound = -12f;
    public GameObject spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager");
        // Change the rotation of the vehicle to face the player
        transform.Rotate(new Vector3(0, 180, 0));
    }

    // Update is called once per frame
    void Update()
    {
        // Move the car in the vertical direction
        transform.Translate(Vector3.forward * Time.deltaTime * vehicleSpeed);

        // Destroy the vehicle if it is past the designated zBound
        if (transform.position.z <= zBound)
        {
            spawnManager.GetComponent<SpawnManager>().DecreaseEnemyCount();
            Destroy(gameObject);
            vehicleSpeed += 10f;
            
        }
    }
}
