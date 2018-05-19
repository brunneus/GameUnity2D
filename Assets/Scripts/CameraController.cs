using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float endXPosition;

    private Vector3 offset;        

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
		if (player != null && player.transform.position.x < endXPosition)
        transform.position = new Vector3(
            (player.transform.position + offset).x, 
            transform.position.y, 
            transform.position.z);
    }
}
