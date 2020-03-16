using UnityEngine;
using System.Collections;


public class CameraControllerLevel1 : MonoBehaviour
{
    public GameObject player;
    private Vector3 _offset;

    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        _offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + _offset;
    }
}