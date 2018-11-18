using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
	public GameObject EnemyPrefab;
	public Vector2 EnemyOutputDirection;

	// Use this for initialization
	void Start () {
		GenerateEnemy();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void GenerateEnemy()
	{
		GameObject enemy = Instantiate(EnemyPrefab, new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.identity);
		enemy.GetComponent<Enemy>().SetDirection(EnemyOutputDirection);
	}
}
