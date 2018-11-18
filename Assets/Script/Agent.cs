using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {
	private Vector2 _direction;
	private NavMeshAgent agent;
	private Rigidbody _rigidbody;
	
	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		Nav();
	}

	private void Nav()
	{
		agent.SetDestination(GetTheClosestItemPosition());
	}

	private Vector3 GetTheClosestItemPosition()
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

		return target;
	}
	
	private void Move()
	{
		transform.Translate(_direction*Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Item"))
		{
			Destroy(other.gameObject);
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
			_direction = Vector2.up;
			Move();
		}
		if (Input.GetKey(KeyCode.S))
		{
			_direction = Vector2.down;
			Move();
		}
		if (Input.GetKey(KeyCode.D))
		{
			_direction = Vector2.right;
			Move();
		}
	}
}
