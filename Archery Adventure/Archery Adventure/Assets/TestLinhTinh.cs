using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLinhTinh : MonoBehaviour {

    int health;
    // Use this for initialization
    bool move = false;
    bool create = true;
    bool create2 = true;
    GameObject player;
	void Start () {
        health = 0;
        player = GameObject.Find("Player");

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(TestCouroutineFunction());
        }

        else if(Input.GetKey(KeyCode.Space))
        {
            if(move)
            player.transform.Translate(0, 0.5f, 0);
        }
        
	}

    IEnumerator TestCouroutineFunction()
    {       
        while(create)
        {        
            if (create2)
            {
                create2 = false;
                Debug.Log("Create");               
            }
            yield return new WaitForSeconds(5);
            create2 = true;
        }     
        move = true;
        print("MyCoroutine is now finished.");
    }

    IEnumerator DoSomething(int loops)
    {
        for (int idx = 0; idx < 20; idx++)
        {
            print("DoSomething Loop");
            // Yield execution of this coroutine and return to the main loop until next frame
            yield return new WaitForSeconds(2);
        }
    }
}
