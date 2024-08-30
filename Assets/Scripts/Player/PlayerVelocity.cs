using UnityEngine;

public class PlayerVelocity : MonoBehaviour
{
    private Vector3 lastPosition;
    public float velocityX;
    public float velocityZ;

    void Start()
    {
        // Initialize lastPosition with the player's initial position
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calculate the velocity for each axis separately
        velocityX = (transform.position.x - lastPosition.x) / Time.deltaTime;
        velocityZ = (transform.position.z - lastPosition.z) / Time.deltaTime;

        // Update lastPosition for the next frame
        lastPosition = transform.position;

        // Optional: Debug the velocity
        Debug.Log("Player Velocity - X: " + velocityX + ", Z: " + velocityZ);
    }
}
