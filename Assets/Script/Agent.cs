using System;
using System.Collections;
using System.Linq;
using Boo.Lang;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {
	private NavMeshAgent agent;
	private Vector3 _target;

	private Vector3 prevPosition;
	private float closestEnemyXMovingDirection;
	private AgentController _agentController;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		prevPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTarget();
		if (SafeToMove())
		{
			if (!Moved())
			{
				agent.SetDestination(_target);
			}
		}
		else
		{
			if (CloseEnemyMovingTowards() && InDangerZone())
			{
				agent.SetDestination(FindEscapeAlcove());
			}
			else
			{
				agent.SetDestination(FindClosestSafeAlcove());
			}
		}
	}

	private bool InDangerZone()
	{
		var z = transform.position.z;
		
		return (z > 0.42 && z < 4.31) || (z>-4.51 && z<-0.31);
	}

	private Vector3 FindEscapeAlcove()
	{
		var alcoves = AlcovesController.GetAlcoves();
		int myAlcoveIndex = -1;
		float minDist = 100;
		
		//find my alcove first, or the closest one
		for (int i=0; i< alcoves.Count; i++)
		{
			Vector3 alcovePos = alcoves[i].transform.position;
			float dist = Vector3.Distance( alcovePos, transform.position);
			if (dist < minDist && ((closestEnemyXMovingDirection < 0) ? alcovePos.x < transform.position.x: alcovePos.x >= transform.position.x))
			{
				minDist = dist;
				myAlcoveIndex = i;
			}
		}

		if (myAlcoveIndex == -1)
		{
			return transform.position;
		}
		
		Vector3 closestAlcove = alcoves[myAlcoveIndex].transform.position;
		
		return (InAlcove(closestAlcove, _target, 0.6)) ? _target: closestAlcove;
	}
	
	private bool CloseEnemyMovingTowards()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (var enemyGameObject in enemies)
		{
			Vector2 enemyMovingDirection = enemyGameObject.GetComponent<Enemy>().GetDirection();
			float diffInXAxis = transform.position.x - enemyGameObject.transform.position.x; //if > 0 then enemy is left else right
			if (SameSign(enemyMovingDirection.x, diffInXAxis) && Vector3.Distance(transform.position, enemyGameObject.transform.position) < 4)
			{
				closestEnemyXMovingDirection = enemyMovingDirection.x;
				return true;
			}
		}
		
		return false;
	}

	private bool SameSign(float num1, float num2)
	{
		return num1 >= 0 && num2 >= 0 || num1 < 0 && num2 < 0;
	}

	private bool SafeToMove()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		float minDist = 100;

		foreach (var enemy in enemies)
		{
			float dist = Vector3.Distance(enemy.transform.position, transform.position);
			minDist = Math.Min(dist, minDist);
		}
		
		//Distance between agent and enemy is large enough
		if (minDist > 4)
		{
			return true;
		}
		
		return false;
	}

	private bool Moved()
	{
		bool moved = Vector3.Distance(prevPosition, transform.position) < 0.02 ;
		prevPosition = transform.position;
		return moved;
	}

	private Vector3 FindClosestSafeAlcove()
	{
		var alcoves = AlcovesController.GetAlcoves();
		int myAlcoveIndex = -1;
		float minDist = 100;
		
		//find my alcove first, or the closest one
		for (int i=0; i< alcoves.Count; i++)
		{
			Vector3 alcovePos = alcoves[i].transform.position;
			float dist = Vector3.Distance( alcovePos, transform.position);
			if (dist < minDist && HasNoEnemyInFront(alcovePos))
			{
				minDist = dist;
				myAlcoveIndex = i;
			}
		}

		if (myAlcoveIndex == -1)
		{
			return transform.position;
		}
		
		Vector3 closestAlcove = alcoves[myAlcoveIndex].transform.position;
		
		float distToTarget = Vector3.Distance(_target, transform.position);
		float distToClosestAlcove = Vector3.Distance(closestAlcove, transform.position);

		return (distToTarget < distToClosestAlcove || InAlcove(closestAlcove, _target, 0.6)) ? _target: closestAlcove;
	}

	private bool HasNoEnemyInFront(Vector3 alcove)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float closestEnemyDistance = 100;

		foreach (var enemy in enemies)
		{
			float dist = Vector3.Distance(enemy.transform.position, alcove);
			closestEnemyDistance = Math.Min(dist, closestEnemyDistance);
		}
		return closestEnemyDistance > 4 || InAlcove(alcove, transform.position, (transform.position.z < 0) ? 0.397 : 0.359);
	}

	private bool InAlcove(Vector3 alcove, Vector3 targ, double tol)
	{
		return targ.x < alcove.x+tol && targ.x > alcove.x-tol && targ.z < alcove.z+tol && targ.z > alcove.z - tol;
	}

	private void UpdateTarget()
	{
		GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
		float minDist = 100;
		
		for (int i = 0; i < items.Length; i++)
		{
			Vector3 curItemPosition = items[i].transform.position;
			float dist = Vector3.Distance(curItemPosition, transform.position);
			if (dist < minDist)
			{
				minDist = dist;
				_target = curItemPosition;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Item"))
		{
			Destroy(other.gameObject);
			_agentController.IncrementAgentScore();
		}	
		
		if (other.CompareTag("FOV"))
		{
			Destroy(gameObject);
		}
		
	}

	public void SetAgentController(AgentController agentController)
	{
		_agentController = agentController;
	}
}
