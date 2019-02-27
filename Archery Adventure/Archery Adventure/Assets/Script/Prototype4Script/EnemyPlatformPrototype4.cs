using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatformPrototype4 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InitEnemyPlatform();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitEnemyPlatform()
    {      
        float min = 0.75f;
        float max = 0.2f;
      
         Vector2 final = Camera.main.ViewportToWorldPoint(new Vector2(1, Random.Range(min, max)));
         final.x = this.transform.position.x;
         transform.position = final;        
    }
}
