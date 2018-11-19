using System;
using System.Collections;
using Boo.Lang;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {
	private Vector3 _direction;
	private NavMeshAgent agent;
	private Vector3 _target;
	public GameObject alcovesController;

	private Vector3 prevPosition;
	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		prevPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		UpdateTarget();
		if (!Moved())
		{
			Nav();
		}
	}

	private bool Moved()
	{
		bool moved = !prevPosition.Equals(transform.position);
		prevPosition = transform.position;
		return moved;
	}

	private void Nav()
	{
		agent.SetDestination(FindNextAlcovePosition());
	}

	private Vector3 FindNextAlcovePosition()
	{
		var alcoves = alcovesController.GetComponent<AlcovesController>().alcoves;
		int myAlcoveIndex = 0;
		float minDist = Vector3.Distance(alcoves[0].transform.position, transform.position);
		
		//find my alcove first, or the closest one
		for (int i=1; i< alcoves.Count; i++)
		{
			float dist = Vector3.Distance( alcoves[i].transform.position, transform.position);
			if (dist < minDist)
			{
				minDist = dist;
				myAlcoveIndex = i;
			}
		}

		Vector3 prevPos = alcoves[(myAlcoveIndex + 9) % 10].transform.position;
		Vector3 nextPos = alcoves[(myAlcoveIndex + 1) % 10].transform.position;
		float tarDistToPrev = Vector3.Distance(prevPos, _target);
		float tarDistToNext = Vector3.Distance(nextPos, _target);

		Vector3 closestAlcove = (tarDistToPrev < tarDistToNext) ? prevPos : nextPos;

		float distToTarget = Vector3.Distance(_target, transform.position);
		float distToClosestAlcove = Vector3.Distance(closestAlcove, transform.position);
		
		return (distToTarget < distToClosestAlcove) ? _target: closestAlcove;
	}

	private void UpdateTarget()
	{
		GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
		Vector3 agentPosition = transform.position;
		Vector3 target = (items.Length > 0) ? items[0].transform.position : agentPosition;
		float minDist = (items.Length > 0) ? Vector3.Distance(items[0].transform.position, agentPosition) : 0;
		
		for (int i = 1; i < items.Length; i++)
		{
			Vector3 curItemPosition = items[i].transform.position;
			float dist = Vector3.Distance(curItemPosition, agentPosition);
			if (dist < minDist)
			{
				minDist = dist;
				target = curItemPosition;
			}
		}

		_target = target;
	}
	
	private void Move()
	{
		transform.Translate(_direction*Time.deltaTime*3);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Item"))
		{
			Destroy(other.gameObject);
		}	
		
		if (other.CompareTag("FOV"))
		{
			Destroy(gameObject);
		}
		
	}
	
	private void GetInput()
	{
		if (Input.GetKey(KeyCode.A))
		{
			_direction = Vector2.left;
			Move();
		}
		if (Input.GetKey(KeyCode.W))
		{
			_direction = Vector3.forward;
			Move();
		}
		if (Input.GetKey(KeyCode.S))
		{
			_direction = Vector3.back;
			Move();
		}
		if (Input.GetKey(KeyCode.D))
		{
			_direction = Vector2.right;
			Move();
		}
	}
}
