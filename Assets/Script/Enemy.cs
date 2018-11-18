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
		if (direction.x > 0)
		{
			Vector3 tmp = transform.GetChild(0).transform.position;
			tmp.x = transform.position.x + 0.8f;
			transform.GetChild(0).transform.position = tmp;
		}
		else if (direction.x < 0)
		{
			Vector3 tmp = transform.GetChild(0).transform.position;
			tmp.x = transform.position.x - 0.8f;
			transform.GetChild(0).transform.position = tmp;
		}
	}

	public void SelfDestroy()
	{
		Destroy(gameObject);
		Doorway.DecrementEnemyCount();
	}
}
