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
			double chance = rnd.NextDouble();
			if (chance < 0.33)
			{
				_direction = Vector2.left;
			}
			else if (chance < 0.66)
			{
				_direction = Vector2.right;
			}
			else
			{
				SelfDestroy();
			}
		}

		if (other.CompareTag("Doorway"))
		{
			SelfDestroy();
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

	private void SelfDestroy()
	{
		Destroy(gameObject);
		Doorway.DecrementEnemyCount();
	}
}
