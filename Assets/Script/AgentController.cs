using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentController : MonoBehaviour
{
	public GameObject AgentPrefab;
	public GameObject PlayerPrefab;
	public Text ScoreText;
	public Text WinningText;
	private static int _agentScore;
	private static int _playerScore;
	private bool _agentDead;
	private bool _playerDead;
	public int _agentTeleportTrapRemaining;
	public int _playerTeleportTrapRemaining;
	private GameObject _agent;
	private GameObject _player;

	// Use this for initialization
	void Start () {
		var alcoves = AlcovesController.GetAlcoves();
		System.Random rnd = new System.Random();
		var index = rnd.Next(alcoves.Count);
		while (index == 0 || index == 6)
		{
			index = rnd.Next(alcoves.Count);
		}
		_agent = Instantiate(AgentPrefab, alcoves[index].transform.position,Quaternion.identity);
		_agent.GetComponent<Agent>().SetAgentController(this);
		var playerIndex = rnd.Next(alcoves.Count);
		
		while (playerIndex == 0 || playerIndex == 6 || playerIndex == index)
		{
			playerIndex = rnd.Next(alcoves.Count);
		}
		
		_player = Instantiate(PlayerPrefab, alcoves[playerIndex].transform.position,Quaternion.identity);
		_player.GetComponent<Player>().SetAgentController(this);
		_agentScore = 0;
		_playerScore = 0;
		ScoreText.text = "Agent Score: " + _agentScore + " Player Score: " + _playerScore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncrementAgentScore()
	{
		_agentScore++;
		ScoreText.text = "Agent Score: " + _agentScore + " Player Score: " + _playerScore;
		if (_agentScore + _playerScore == 10)
		{
			ShowWinningText();
		}
	}

	public void IncrementPlayerScore()
	{
		_playerScore++;
		ScoreText.text = "Agent Score: " + _agentScore + " Player Score: " + _playerScore;
		if (_agentScore + _playerScore == 10)
		{
			ShowWinningText();
		}
	}

	private void ShowWinningText()
	{
		if (_playerScore == _agentScore)
		{
			WinningText.text = "Tie!";
		}
		else
		{
			WinningText.text = ((_playerScore > _agentScore) ? "Player": "Agent") + " Won!";
		}
	}

	public void AgentDead()
	{
		_agentDead = true;
		if (_playerDead)
		{
			ShowWinningText();
		}
	}

	public void PlayerDead()
	{
		_playerDead = true;
		if (_agentDead)
		{
			ShowWinningText();
		}
	}

	public void TeleportPlayer()
	{
		Destroy(_player);
		var alcoves = AlcovesController.GetAlcoves();
		System.Random rnd = new System.Random();
		var index = rnd.Next(alcoves.Count);
		
		while (index == 0 || index == 6)
		{
			index = rnd.Next(alcoves.Count);
		}
		
		_player = Instantiate(PlayerPrefab, alcoves[index].transform.position,Quaternion.identity);
		_player.GetComponent<Player>().SetAgentController(this);
		DecrementAgentTeleportTrapRemaining();
	}

	public void TeleportAgent()
	{
		Destroy(_agent);
		var alcoves = AlcovesController.GetAlcoves();
		System.Random rnd = new System.Random();
		var index = rnd.Next(alcoves.Count);
		
		while (index == 0 || index == 6)
		{
			index = rnd.Next(alcoves.Count);
		}
		
		_agent = Instantiate(AgentPrefab, alcoves[index].transform.position,Quaternion.identity);
		_agent.GetComponent<Agent>().SetAgentController(this);
		DecrementPlayerTeleportTrapRemaining();
	}

	public void DecrementPlayerTeleportTrapRemaining()
	{
		_playerTeleportTrapRemaining--;
		WinningText.text = "Player used teleported trap " + _playerTeleportTrapRemaining + " remaining.";
	}

	public void DecrementAgentTeleportTrapRemaining()
	{
		_agentTeleportTrapRemaining--;
		WinningText.text = "Agent used teleported trap " + _agentTeleportTrapRemaining + " remaining.";
	}
}
