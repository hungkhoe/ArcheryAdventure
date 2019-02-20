using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidEnemy : Enemy {

    // Use this for initialization
    private void Awake()
    {
        player = GameObject.Find("Player");
        
    }
    void Start () {
        damage = 2;
        health = 3;
	}
	
	// Update is called once per frame
	void Update () {
        if (isShooting == false)
        {
            ShootArrow();
        }
    }
}
