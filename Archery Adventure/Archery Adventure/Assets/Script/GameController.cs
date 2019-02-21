using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		InitGame ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitGame(){
	
		//set first position player
		StairController.Instance.InitStairs();
		MovingController.Instance.InitMoving ();

		//StartGame ();
	}

	public void StartGame(){
	
		MovingController.Instance.Move ();
	}
}
