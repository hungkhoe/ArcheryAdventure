using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidEnemyScriptMrGun2 : EnemyScriptMrGun2
{

	// Use this for initialization
	void Start () {
        health = 2;
        coolDownAttack = 2.5f;
        timer = Random.Range(0, 2.5f);        
    }
		// Update is called once per frame
	
}
