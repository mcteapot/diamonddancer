using UnityEngine;
using System.Collections;

public class LevelCubeManager : MonoBehaviour {
	
	public Transform gameManager;
	
	//private GameManager gameManager;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
	
	void OnCollisionEnter (Collision collision) {
		gameManager.gameObject.GetComponent<GameManager>().endGame();
		Debug.Log("GAME FUCKING OVER");
	}
}
