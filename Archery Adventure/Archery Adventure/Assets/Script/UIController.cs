using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	public void StartGame_OnClick(){
	
		GameController.Instance.StartGame ();
		transform.GetChild (0).gameObject.SetActive (false);
	}
}
