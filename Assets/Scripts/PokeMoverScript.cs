using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PokeMoverScript : MonoBehaviour {

	public enum MoveModi
	{
		Directional,
		RandomInCircle,
		MoveInCicles,
		Patrol,
	}

	public MoveModi MoveMode;



	private Vector3 StartingPosition;
	private NavMeshAgent NavAgent;

	// Variables for directional movement

	public Vector3 MovementDirection;
	public float DistanceFromStart;
	private bool IsMovingTo = true;

	// Variables for random movement in circle

	public float InCircleRadius;

	// Variables for moving in circles

	public float OnCircleRadius;
	public float OnCircleFragments;
	private float OnCircleFraction;

	// Variables for patrolling

	public List<GameObject> PatrolPoints;
	private int CurrentPoint;

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
		case MoveModi.RandomInCircle:
			return GetNextMovementPointRandomInArea();
		case MoveModi.MoveInCicles:
			return GetNextMovementPointOnCircle();
		case MoveModi.Patrol:
			return GetNextMovementPointPatrol();
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
		Vector3 PointToMoveTo = new Vector3();

		Vector2 RandomPointInCircle;
		RandomPointInCircle = (Random.insideUnitCircle * InCircleRadius);

		PointToMoveTo.x = RandomPointInCircle.x;
		PointToMoveTo.z = RandomPointInCircle.y;

		PointToMoveTo += StartingPosition;

		return PointToMoveTo;
	}

	Vector3 GetNextMovementPointOnCircle()
	{
		Vector3 PointToMoveTo = new Vector3();

		Vector2 PointOnCircle = new Vector2();
		PointOnCircle.x = Mathf.Sin(OnCircleFraction * Mathf.PI * 2) * OnCircleRadius;
		PointOnCircle.y = Mathf.Cos(OnCircleFraction * Mathf.PI * 2) * OnCircleRadius - OnCircleRadius;

		OnCircleFraction += 1 / OnCircleFragments;

		PointToMoveTo.x = PointOnCircle.x;
		PointToMoveTo.z = PointOnCircle.y;

		PointToMoveTo += StartingPosition;

		return PointToMoveTo;
	}

	Vector3 GetNextMovementPointPatrol()
	{
		Vector3 PointToMoveTo = new Vector3();

		PointToMoveTo = PatrolPoints[CurrentPoint].transform.position;

		if(CurrentPoint < PatrolPoints.Count - 1)
		{
			CurrentPoint++;
		}
		else
		{
			CurrentPoint = 0;
		}

		return PointToMoveTo;

	}
}
