using UnityEngine;
using System.Collections;

public class LevelCubeManager : MonoBehaviour {
	
	public Transform gameManager;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
	
	void OnCollisionEnter (Collision collision) {
		Debug.Log("GAME FUCKING OVER");
	}
}
