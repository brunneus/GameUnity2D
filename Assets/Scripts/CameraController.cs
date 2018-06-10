using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float endXPosition;

    private float offset;        

    void Start()
    {
        offset = transform.position.x - 0.9f;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
		if (player != null && !player.GetComponent<PlayerPlatformerController>().isLocked() && player.transform.position.x < endXPosition && player.transform.position.x > 0.14)
        transform.position = new Vector3(
			player.transform.position.x + offset, 
            transform.position.y, 
            transform.position.z);
    }
}
