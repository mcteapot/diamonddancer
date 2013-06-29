using UnityEngine;
using System.Collections;

public class BorderTManager : MonoBehaviour {
	
	public float precentIncrease = 10.0F;
	public float maxVeol = 15.0F;
	
	public AudioClip bip ;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter (Collision collision) {
		//Debug.Log("WORKING COLIDE");
		float velo = collision.rigidbody.velocity.magnitude;
		float newVelo = velo + (velo * precentIncrease);
		float newVector = Random.Range(-5.0F, 5.0F);
		newVelo = Mathf.Clamp(newVelo, 1.0F, maxVeol); 
		collision.rigidbody.velocity = new Vector3(newVector, 0, -newVelo);
		
		audio.PlayOneShot(bip);
	}
}
