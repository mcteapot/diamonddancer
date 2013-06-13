using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour {
	
	float gravity = -9.8F;
	
	public float initalSpeed = 1.0F;
	
	private Vector3 startPosTop = new Vector3(0, 0.5F, 5.5F);
	private Vector3 startPosLeft = new Vector3(-10.0F, 0.5F, 0);
	private Vector3 startPosRight = new Vector3(10.0F, 0.5F, 0);
	private Vector3 startPosBottom = new Vector3(0, 0.5F, -5.5F);
	
	private Vector3 startVelTop = new Vector3(0, 0, -1);
	private Vector3 startVelLeft = new Vector3(-1, 0, 0);
	private Vector3 startVelRight = new Vector3(1, 0, 0);
	private Vector3 startVelBottom = new Vector3(0, 0, 1);
	
	private Vector3 startPos = new Vector3(0, 0, 0);
	private Vector3 startVel = new Vector3(0, 0, 0);
	
	
	void Awake () {
		int randomNum = Random.Range(1, 5);
		Debug.Log("Start Pos: " + randomNum);
		if(randomNum == 1) {
			// Top
			startPos = startPosTop;
			startVel = startVelTop;
		} else if(randomNum == 2) {
			// Left
			startPos = startPosLeft;
			startVel = startVelLeft;
		} else if(randomNum == 3) {
			// Right
			startPos = startPosRight;
			startVel = startVelRight;
		} else if(randomNum == 4) {
			// Bottom
			startPos = startPosBottom;
			startVel = startVelBottom;
		}
		Debug.Log("Start POS " + startPos);
		Debug.Log("Start VEL " + startVel);
	}

	// Use this for initialization
	void Start () {
		//float zDir = Random.Range(-1.0F, 1.0F);
		//rigidbody.AddForce(new Vector3(-1, 0, 1) * initalSpeed);
		//rigidbody.AddForce(new Vector3(0, 0, 1) * initalSpeed);
		
		transform.position = startPos;
		rigidbody.AddForce(startVel * initalSpeed);
	
	}
	
	// Update is called once per frame
	void Update () {
		//int randomNum = Random.Range(1, 5);
		//Debug.Log(randomNum);
	
	}
	
}
