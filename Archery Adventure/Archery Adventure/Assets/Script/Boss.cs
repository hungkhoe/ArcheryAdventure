using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
    // Use this for initialization
    GameManager gameManager;
    private void Awake()
    {
        GameObject temp = GameObject.Find("GameManger");
        gameManager = temp.GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }
    void Start () {
        damage = 3;
        health = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if (isShooting == false)
        {
            ShootArrow();
        }
    }

    protected override void LateUpdate()
    {
        if (health == 0)
        {
            gameManager.SetWinning(true);
            Destroy(this.gameObject);
        }
    }


}
