using UnityEngine;
using System.Collections;

public class PokeMoverScript : MonoBehaviour {

	public enum MoveModi
	{
		Directional,
		RandomInArea,
	}

	public MoveModi MoveMode;



	private Vector3 StartingPosition;
	private NavMeshAgent NavAgent;

	// Variables for directional movement

	public Vector3 MovementDirection;
	public float DistanceFromStart;
	private bool IsMovingTo = true;

	// Use this for initialization
	void Start () {

		StartingPosition = transform.position;
		NavAgent = GetComponent<NavMeshAgent>();
		NavAgent.SetDestination(GetNextMovementPoint());
	
	}
	
	// Update is called once per frame
	void Update () {

		if(NavAgent.remainingDistance < 0.5f)
		{
			NavAgent.SetDestination(GetNextMovementPoint());
		}
	}

	Vector3 GetNextMovementPoint()
	{
		switch (MoveMode)
		{
		case MoveModi.Directional:
			return GetNextMovementPointDirectional();
		case MoveModi.RandomInArea:
			return GetNextMovementPointRandomInArea();
		default:
			return Vector3.zero;
		}
	}

	Vector3 GetNextMovementPointDirectional()
	{
		Vector3 PointToMoveTo;

		if(IsMovingTo)
		{
			PointToMoveTo = StartingPosition + (MovementDirection.normalized * DistanceFromStart);
			IsMovingTo = false;
		}
		else
		{
			PointToMoveTo = StartingPosition - (MovementDirection.normalized * DistanceFromStart);
			IsMovingTo = true;
		}

		return PointToMoveTo;
	}

	Vector3 GetNextMovementPointRandomInArea()
	{
		return Vector3.zero;
	}
}
