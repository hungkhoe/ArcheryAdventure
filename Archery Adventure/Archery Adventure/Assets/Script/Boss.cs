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

    protected override void LateUpdate()
    {
        if (health == 0)
        {           
            Destroy(this.gameObject);
        }
    }

    public override void TakeDamage(int _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
            UIController.Instance.SetWinning(true);
        }
        if (health > 0)
        {
            GetComponent<MovingController>().Move();
            GameController.Instance.NextStep();
            GameController.Instance.SetIsNotDead(true);
        }
        else
        {


        }

        

        //GameController.Instance.NextStep ();
    }

}
