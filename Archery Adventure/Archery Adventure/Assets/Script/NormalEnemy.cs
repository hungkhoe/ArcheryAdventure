using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy {

    // Use this for initialization
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Start () {
        health = 1;
        damage = 1;
	}
	
	// Update is called once per frame
	
}
