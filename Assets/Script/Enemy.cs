using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Vector2 _direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("SideObstacle"))
		{
			System.Random rnd = new System.Random();
			_direction = (rnd.NextDouble()<0.5)?Vector2.left:Vector2.right;
		}

		if (other.CompareTag("Doorway"))
		{
			Destroy(gameObject);
		}
	}

	private void Move()
	{
		transform.Translate(_direction*Time.deltaTime);
	}

	public void SetDirection(Vector2 direction)
	{
		_direction = direction;
	}
}
