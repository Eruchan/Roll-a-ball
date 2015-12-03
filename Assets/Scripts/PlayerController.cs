using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//speed of ball movement
	public float speed = 2;
	//amount of pick ups collected
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

		if (count >= 12)
		{
			winText.text = "You Win!";
		}
	}
}