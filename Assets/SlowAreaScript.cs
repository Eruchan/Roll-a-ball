using UnityEngine;
using System.Collections;

public class SlowAreaScript : MonoBehaviour {

	// Drag applied to to player when in the slow area, the higher the amount the slower will the player be
	public float AreaDrag;

	float CacheDrag;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Rigidbody rb = other.GetComponent<Rigidbody>();

			//Save the drag of the player
			CacheDrag = rb.drag;

			// Slow the player
			rb.drag = AreaDrag;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			Rigidbody rb = other.GetComponent<Rigidbody>();
			
			// Reset drag of the player
			rb.drag = CacheDrag;
		}
	}
}
