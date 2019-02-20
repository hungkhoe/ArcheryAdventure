using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
    // Use this for initialization
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Start () {
        damage = 3;
        health = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
