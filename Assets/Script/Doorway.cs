using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
	public GameObject EnemyPrefab;
	public Vector2 EnemyOutputDirection;
	private static List<Doorway> _doorways = new List<Doorway>();
	private static int _enemyCount = 0;
	private static int _lastSpawnIndex;

	private static readonly int MaxEnemyAllowed = 2;

	// Use this for initialization
	void Start () {
		_doorways.Add(this);
		if (_doorways.Count == 4)
		{
			RandomSpawnEnemy();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void GenerateEnemy()
	{
		GameObject enemy = Instantiate(EnemyPrefab, new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.identity);
		enemy.GetComponent<Enemy>().SetDirection(EnemyOutputDirection);
		_enemyCount++;
	}

	/*
	 * Random generate enemy to the maximum amount.
	 */
	private static void RandomSpawnEnemy()
	{
		System.Random rnd = new System.Random();
		while (_enemyCount < MaxEnemyAllowed)
		{
			int i = rnd.Next(_doorways.Count);
			if (i != _lastSpawnIndex)
			{
				_doorways[i].GenerateEnemy();
				_lastSpawnIndex = i;
			}
		}
	}

	public static void DecrementEnemyCount()
	{
		_enemyCount--;
		RandomSpawnEnemy();
	}
}
