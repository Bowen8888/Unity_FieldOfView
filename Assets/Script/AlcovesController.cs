using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcovesController : MonoBehaviour {
	
	public List<GameObject> alcoves = new List<GameObject>();
	
	private static List<GameObject> staticAlcoves = new List<GameObject>();

	// Use this for initialization
	void Start () {
		staticAlcoves.AddRange(alcoves);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static List<GameObject> GetAlcoves()
	{
		return staticAlcoves;
	}
}
