using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset; // To store the offset distance between the player and camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;
        // where the hell is the transform coming from?
        // Answer: In Unity, every GameObject has a Transform component that stores its position,
        // rotation, and scale in the game world.
        // When you create a script that inherits from MonoBehaviour (like CameraController), it automatically has access to the Transform component of the GameObject
        // to which the script is attached. This is why you can use "transform" directly in your script to refer to the Transform of the CameraController GameObject.
    }

    // Update is called once per 
    void LateUpdate()
    {
        if (player != null) 
        {
            transform.position = player.transform.position + offset;
        }
    }
}
    