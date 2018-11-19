﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
	public GameObject AgentPrefab;
	public GameObject PlayerPrefab;

	// Use this for initialization
	void Start () {
		var alcoves = AlcovesController.GetAlcoves();
		System.Random rnd = new System.Random();
		var index = rnd.Next(alcoves.Count);
		while (index == 0 || index == 6)
		{
			index = rnd.Next(alcoves.Count);
		}
		Instantiate(AgentPrefab, alcoves[index].transform.position,Quaternion.identity);
		var playerIndex = rnd.Next(alcoves.Count);
		
		while (playerIndex == 0 || playerIndex == 6 || playerIndex == index)
		{
			playerIndex = rnd.Next(alcoves.Count);
		}
		
		Instantiate(PlayerPrefab, alcoves[playerIndex].transform.position,Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InstantiateAgent()
	{
	}
}
