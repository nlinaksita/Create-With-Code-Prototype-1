﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    private float speed = 20f;
    private float turnSpeed = 45f;

    private float horizontalInput;
    private float forwardInput;

    private GameObject spawnManager;
    private Vector3 startPosition = new Vector3(-3, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal_P1");
        forwardInput = Input.GetAxis("Vertical_P1");

        // Move the vehicle forward based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // Move the vehicle forward based on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

        // If the player falls through the floor, reset position
        if (transform.position.y < -1)
        {
            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == "Finish")
        {
            // Reset player position
            transform.position = Vector3.zero;

            // Reset spawnManager objects
            spawnManager.GetComponent<SpawnManager>().LevelUp();
        }
    }
}
