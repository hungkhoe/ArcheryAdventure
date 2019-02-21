using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stair : MonoBehaviour {


	public int numStair;
	public int idStair;
	public UnityAction callBackHiddent;

	Vector2 startPoint;
	Vector2 endPoint;
	// Use this for initialization
	void Start () {

		startPoint = transform.GetChild (2).position;
		endPoint = transform.GetChild (3).position;
	}
	
	void OnBecameInvisible()
	{
		//gameObject.SetActive (false);
		if (callBackHiddent != null)
			callBackHiddent ();
	}

	public Vector2 StartPoint{
	
		get{

			return startPoint;
		}
	}

	public Vector2 EndPoint{

		get{ 
		
			return endPoint;
		}
	}

}
