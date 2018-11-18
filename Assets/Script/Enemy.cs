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

	private void Move()
	{
		transform.Translate(_direction*Time.deltaTime);
	}

	public void SetDirection(Vector2 direction)
	{
		_direction = direction;
	}

	public void SelfDestroy()
	{
		Destroy(gameObject);
		Doorway.DecrementEnemyCount();
	}
}
