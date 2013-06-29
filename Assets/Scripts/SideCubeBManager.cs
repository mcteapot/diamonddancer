using UnityEngine;
using System.Collections;

public class SideCubeBManager : MonoBehaviour {
	
	public AudioClip bip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter (Collision collision) {
		//Debug.Log("WORKING COLIDE");
		float velo = collision.rigidbody.velocity.magnitude;
		//float newVelo = co
		float newVector = Random.Range(-1.0F, 1.0F);
		collision.rigidbody.velocity = new Vector3(newVector, 0, -velo);
		
		audio.PlayOneShot(bip);
	}
}
