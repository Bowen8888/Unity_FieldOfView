using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Vector3 _direction;
	private AgentController _agentController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
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
	
	private void Move()
	{
		transform.Translate(_direction*Time.deltaTime*3);
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Item"))
		{
			Destroy(other.gameObject);
			_agentController.IncrementPlayerScore();
		}	
		
		if (other.CompareTag("FOV"))
		{
			Destroy(gameObject);
			_agentController.PlayerDead();
		}
		
	}

	public void SetAgentController(AgentController agentController)
	{
		_agentController = agentController;
	}
}
