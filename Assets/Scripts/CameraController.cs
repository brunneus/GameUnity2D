using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float endXPosition;

    private float offset;
	private float originalCameraY;

    void Start()
    {
        offset = transform.position.x - 0.9f;
		originalCameraY = transform.position.y;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
		var cameraY = player.transform.position.y > 8.5
			? originalCameraY + (player.transform.position.y - 8.5f)
			: originalCameraY;
		
		if (player != null && !player.GetComponent<PlayerPlatformerController>().isLocked() && player.transform.position.x < endXPosition && player.transform.position.x > 0.14)
        transform.position = new Vector3(
			player.transform.position.x + offset, 
			cameraY, 
            transform.position.z);
		
		Debug.Log (Time.deltaTime);
    }
}
