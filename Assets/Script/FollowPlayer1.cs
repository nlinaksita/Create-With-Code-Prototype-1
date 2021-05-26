using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer1 : MonoBehaviour
{
    public GameObject player;
    public Camera player1Camera;

    private Rect soloRect = new Rect(0, 0, 1, 1);
    private Rect multiRect = new Rect(0, 0, 0.5f, 1);

    private Vector3 behindOffset = new Vector3(0, 5, -7);
    private Vector3 driverOffset = new Vector3(0, 3, 2);
    private Vector3 offset;
    private bool toggleCam = false; // false = 3rd person view, true = driver view
    // Start is called before the first frame update
    void Start()
    {
        offset = behindOffset;
    }

    private void Update()
    {
        // Toggle between 3rd person and driver view
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Flip the toggle state
            toggleCam = !toggleCam;

            // Change the offset value
            // if true, change to driver view
            if (toggleCam)
            {
                offset = driverOffset;
                transform.Rotate(0, 0, 0);
            } else
            {
                offset = behindOffset;
                transform.Rotate(25, 0, 0);
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

    public void SetSolo()
    {
        player1Camera.rect = soloRect;
    }

    public void SetMulti()
    {
        player1Camera.rect = multiRect;
    }
}
