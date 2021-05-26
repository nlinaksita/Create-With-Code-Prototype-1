using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer2 : MonoBehaviour
{
    public GameObject player;
    private Vector3 behindOffset = new Vector3(0, 5, -7);
    private Vector3 driverOffset = new Vector3(0, 3, 2);
    private Vector3 offset;

    private Vector3 behindRotate = new Vector3(25, 0, 0);
    private Vector3 driverRotate = Vector3.zero;
    private bool toggleCam = false; // false = 3rd person view, true = driver view
    // Start is called before the first frame update
    void Start()
    {
        offset = behindOffset;
    }

    private void Update()
    {
        // Toggle between 3rd person and driver view
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            // Flip the toggle state
            toggleCam = !toggleCam;

            // Change the offset value
            // if true, change to driver view
            if (toggleCam)
            {
                offset = driverOffset;
                transform.eulerAngles = driverRotate;
            } else
            {
                offset = behindOffset;
                transform.eulerAngles = behindRotate;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        // if driver view enabled, follow player rotation
        if (toggleCam)
        {
            transform.rotation = player.transform.rotation;
        }
    }
}
