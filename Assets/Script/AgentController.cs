using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentController : MonoBehaviour
{
	public GameObject AgentPrefab;
	public GameObject PlayerPrefab;
	public Text PlayerScoreText;
	public Text AgentScoreText;
	private static int _agentScore;
	private static int _playerScore;

	// Use this for initialization
	void Start () {
		var alcoves = AlcovesController.GetAlcoves();
		System.Random rnd = new System.Random();
		var index = rnd.Next(alcoves.Count);
		while (index == 0 || index == 6)
		{
			index = rnd.Next(alcoves.Count);
		}
		GameObject agent = Instantiate(AgentPrefab, alcoves[index].transform.position,Quaternion.identity);
		agent.GetComponent<Agent>().SetAgentController(this);
		var playerIndex = rnd.Next(alcoves.Count);
		
		while (playerIndex == 0 || playerIndex == 6 || playerIndex == index)
		{
			playerIndex = rnd.Next(alcoves.Count);
		}
		
		GameObject player = Instantiate(PlayerPrefab, alcoves[playerIndex].transform.position,Quaternion.identity);
		player.GetComponent<Player>().SetAgentController(this);
		_agentScore = 0;
		_playerScore = 0;
		AgentScoreText.text = "Agent Score: " + _agentScore;
		PlayerScoreText.text = "Player Score: " + _playerScore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncrementAgentScore()
	{
		_agentScore++;
		AgentScoreText.text = "Agent Score: " + _agentScore;
	}

	public void IncrementPlayerScore()
	{
		_playerScore++;
		PlayerScoreText.text = "Player Score: " + _playerScore;
	}
}
