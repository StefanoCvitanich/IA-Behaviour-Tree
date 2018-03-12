using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	public Transform lookAtTransform;
	public Transform cameraTransform;

	public Vector3 dir;
	public Quaternion rot;

	private Camera mainCam;

	private float distance = 2.0f;
	public float currentX = 0.0f;
	private float currentY = 0.0f;
	private float sensivityX = 1.0f;
	private float sensivityY = 1.0f;

	private const float minYAgle = -60.0f;
	private const float maxYAngle = 25.0f;

	void Start () {

		mainCam = Camera.main;
		cameraTransform = transform;
        Cursor.visible = false;
	}

    void Update() {

        if (!Cursor.visible)
        {
            currentX += Input.GetAxis("Mouse X") * sensivityX;

            currentY += Input.GetAxis("Mouse Y") * sensivityY;

            currentY = Mathf.Clamp(currentY, minYAgle, maxYAngle);
        }
    }

	void LateUpdate () {

		dir = new Vector3 (0, 0, -distance);

		rot = Quaternion.Euler (-currentY, currentX, 0);

        cameraTransform.position = (lookAtTransform.position + rot * dir);

		cameraTransform.LookAt (lookAtTransform.position);
	}
}
