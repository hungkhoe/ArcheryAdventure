using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScriptMrGun2 : EnemyScriptMrGun2 {

    // Use this for initialization
    bool isJumping;
    int randomDirection;
    [SerializeField]
    float direction = 1.5f;
    bool isGoingUp, isGoingDown;
    static BossScriptMrGun2 instance;

    void Start() {
        health = 5;
        coolDownAttack = 1.5f;
        timer = Random.Range(0, 1.5f);
    }

    public static BossScriptMrGun2 Instance
    {
        get
        {
            return instance;
        }
    }
    public void Awake()
    {
        instance = this;
    }

    public void JumpingBox()
    {
        randomDirection = Random.Range(0, 2);
        isJumping = true;
        Vector3 currentPos = Camera.main.WorldToViewportPoint(transform.position);      
        if (currentPos.y > 0.9f)
        {
            randomDirection = 0;
        }
        else if (currentPos.y < 0.3f)
        {
            randomDirection = 1;
        }
        if (randomDirection == 0)
        {
            direction = -1.5f;
            isGoingDown = true;
            isGoingUp = false;
        }
        else
        {
            isGoingDown = false;
            isGoingUp = true;
            direction = 1.5f;
        }
    }

    public override void EnemyTakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Destroy(this.gameObject);
            PlayerControllerMrGun2.Instance.TOTALENEMYDESTROYED++;
            UIControllerMrGun2.Instance.SetWin();
        }
        else
        {
            JumpingBox();
        }

      
    }

    public override void Update()
    {
        if(isJumping)
        {
            transform.Translate(0, direction * Time.deltaTime, 0);
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= coolDownAttack)
            {
                timer = 0;
                ShootArrow();
            }
        }     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(isGoingUp)
        {
            if(collision.gameObject.name =="jump_up")
            {
                isJumping = false;
            }
        }

        else if (isGoingDown)
        {
            if (collision.gameObject.name == "jump_down")
            {
                isJumping = false;               
            }
        }
    }
}
