using UnityEngine;
using System.Collections;

public class WaveMotion : MonoBehaviour {
	
	public float speed = 0.2f;
	public float waveOffset = 0.0f;
	
	public float yRangeMax = 1.0f;
	public float yRangeMin = 1.0f;
	
	public float xRangeMax = 1.0f;
	public float xRangeMin = 1.0f;
	
	private float yStartPos;
	private float yPosMax;
	private float yPosMin;
	
	private float xStartPos;
	private float xPosMax;
	private float xPosMin;
	
	//private float xStartPos;
	

	// Use this for initialization
	void Start () {
		yStartPos = transform.localPosition.y;
		xStartPos = transform.localPosition.x;
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void FixedUpdate () {
	
		yPosMax = yStartPos + yRangeMax;
		yPosMin = yStartPos - yRangeMin;
		
		xPosMax = xStartPos + xRangeMax;
		xPosMin = xStartPos - xRangeMin;
		
		
		double weightY = Mathf.Cos((Time.time + waveOffset) * speed * 2 * Mathf.PI) + 0.5 + 0.5;
		double weightX = Mathf.Sin((Time.time + waveOffset) * speed * 2 * Mathf.PI) + 0.5 + 0.5;

		float yPos = (float)(yPosMax * weightY + yPosMin * (1 - weightY));
		float xPos = (float)(xPosMax * weightX + xPosMin * (1 - weightX));
		
		transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
	}
}
