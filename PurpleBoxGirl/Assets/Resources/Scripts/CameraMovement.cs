using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Vector3 CameraPosition;
    public float MovementSpeed;
    public float MovementIncrease;
    public float MaxSpeed;
    public bool Move;

	public int cameraShakeTime;
	public int cameraShakeIntensity;
	public Vector3 pointToShakeAround;
    public float deltaTime = 0f;

    private void Start()
    {
		cameraShakeTime = -1;
        StartMove();
    }

    public void printMoveSpeed()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Time.fixedTime % 1 == 0)
        {
            //Debug.Log("MovementSpeed: " + MovementSpeed);
        }

    }

    void Update () {
        printMoveSpeed();
        if (Move)
        {
			CameraPosition = gameObject.transform.position;
			CameraPosition.x += MovementSpeed;
			gameObject.transform.position = CameraPosition;

            if (MovementSpeed < MaxSpeed)
            {
                MovementSpeed = MovementSpeed + (MovementIncrease * Time.deltaTime);
            }

        }

		if (cameraShakeTime >= 0) {
			executeCameraShake();
		}
	}

    public void StartMove ()
    {
        MaxSpeed = 0.075f;
        MovementSpeed = 0.03f;
        MovementIncrease = 0.00025f;
        Move = true;
    }

	/**
	 *  Executes the current camera shake.
	 */
	private void executeCameraShake() {
		cameraShakeTime--;

		/*
		 *  Just sets the camera's position to the pointToShakeAround
		 *  if the camera shake is complete.
		 */
		if (cameraShakeTime <= 0) {
			gameObject.transform.position = pointToShakeAround;
			return;
		}

		Vector3 newCameraPosition = pointToShakeAround;

		// intensity is divided by 100, otherwise the camera shakes way too violently when using ints.
		newCameraPosition.x += UnityEngine.Random.Range(-cameraShakeIntensity, cameraShakeIntensity)/100f;
		newCameraPosition.y += UnityEngine.Random.Range(-cameraShakeIntensity, cameraShakeIntensity)/100f;

		gameObject.transform.position = newCameraPosition;
	}

	/**
	 *  Sets the camera to shake at a given intensity around its current location
	 * 	for a given amount of time (Number of updates).
	 * 
	 *  Currently the camera can't move while it is shaking :(
	 */
	public void shakeCamera(int time, int intensity){
		cameraShakeTime = time;
		cameraShakeIntensity = intensity;
		pointToShakeAround = gameObject.transform.position;
	}

}
