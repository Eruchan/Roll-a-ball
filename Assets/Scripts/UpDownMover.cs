using UnityEngine;
using System.Collections;

public class UpDownMover : MonoBehaviour {


	float YOffset;
	public float MoveHeight;
	public float MoveFrequency;

	// Use this for initialization
	void Start () {

		YOffset = transform.position.y;
	
	}
	
	// Update is called once per frame
	void Update () {


		float CurrentHeight = YOffset + MoveHeight * Mathf.Cos(MoveFrequency * Time.time + Mathf.PI) + MoveHeight;
		Vector3 CurrentPosition = transform.position;
		CurrentPosition.y = CurrentHeight;
		transform.position = CurrentPosition;
	}
}
