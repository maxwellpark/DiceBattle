using UnityEngine;

public class PlayerVelocity : MonoBehaviour
{
    private Vector3 lastPosition;
    public float velocityX;
    public float velocityZ;
    public float speedNullifierZ=1;
    public float speedNullifierX = 1;

    void Start()
    {
        // Initialize lastPosition with the player's initial position
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calculate the velocity for each axis separately
        velocityX =speedNullifierX * (transform.position.x - lastPosition.x) / Time.deltaTime;
        velocityZ = speedNullifierZ * (transform.position.z - lastPosition.z) / Time.deltaTime;

        // Update lastPosition for the next frame
        lastPosition = transform.position;

        // Optional: Debug the velocity
        // Debug.Log("Player Velocity - X: " + velocityX + ", Z: " + velocityZ);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {

            speedNullifierZ = 0;
            speedNullifierX = 1;

        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {

            speedNullifierZ = 1;
            speedNullifierX = 0;

        }


    }
}
