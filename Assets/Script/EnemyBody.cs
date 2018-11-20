using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{

	private Enemy _enemy;

	// Use this for initialization
	void Start ()
	{
		_enemy = transform.parent.GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("SideObstacle"))
		{
			transform.parent.GetChild(0).gameObject.SetActive(false);
			System.Random rnd = new System.Random();
			double chance = rnd.NextDouble();
			if (chance < 0.33)
			{
				_enemy.SetDirection(Vector2.left);
			}
			else if (chance < 0.66)
			{
				_enemy.SetDirection(Vector2.right);
			}
			else
			{
				_enemy.SelfDestroy();
			}
		}

		if (other.CompareTag("Doorway"))
		{
			_enemy.SelfDestroy();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("SideObstacle"))
		{
			transform.parent.GetChild(0).gameObject.SetActive(true);
		}
	}
}
