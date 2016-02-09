using UnityEngine;
using System.Collections;

public class JumpPlatformScript : MonoBehaviour {


	public float JumpStrength;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			// Get the rigidbody of the player
			Rigidbody rb = other.GetComponent<Rigidbody>();

			Vector3 LaunchVector = new Vector3();
			LaunchVector.y = JumpStrength;
			rb.AddForce(LaunchVector);
		}
	}
}
