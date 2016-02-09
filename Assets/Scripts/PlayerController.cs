using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	//speed of ball movement
	public float speed = 2;

	// Pivot of the camera, used to define the direction of the applied force
	public Transform CameraPivot;
	private Rigidbody rb;
	private LevelManager Manager;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();

		// Get the level manager for this level
		Manager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
	}
	
	void FixedUpdate ()
	{
		if (Manager.IsRunningTime ()) 
		{
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
		
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			// As we can only rotate the camera horizontally we can use the rotation of the pivot directly
			movement = CameraPivot.rotation * movement;
		
			rb.AddForce (movement * speed);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pokemon")) 
		{
			other.gameObject.SetActive (false);
			Manager.PokemonCaught ();
		}
	}
}