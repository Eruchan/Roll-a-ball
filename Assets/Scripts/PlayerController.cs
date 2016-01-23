using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//speed of ball movement
	public float speed = 2;
	//actual amount of pick ups that needs to be collected to win
	public int pickUpAmount = 3;

	// Pivot of the camera, used to define the direction of the applied force
	public Transform CameraPivot;

	//amount of pick ups collected displayed for the player
	public Text countText;

	//message that is displayed when the player wins
	public Text winText;

	private Rigidbody rb;

	private int count;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody>();

		count = 0;

		SetCountText ();
		winText.text = "";
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		// As we can only rotate the camera horizontally we can use the rotation of the pivot directly
		movement = CameraPivot.rotation * movement;
		
		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();

		if (count >= pickUpAmount)
		{
			winText.text = "You Win!";
		}
	}
}