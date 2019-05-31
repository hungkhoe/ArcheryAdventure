﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptMrGun2 : MonoBehaviour {

    // Use this for initialization
    protected int health, damage;
    protected float timer;
    protected float coolDownAttack;
    [SerializeField]
    GameObject prefab_EnemyArrow;
	void Start () {
        coolDownAttack = 5;
        timer = Random.Range(0, 5);
        health = 1;
	}

    // Update is called once per frame
    public virtual void Update () {
        timer += Time.deltaTime;
        if(timer >= coolDownAttack)
        {
            timer = 0;
            ShootArrow();
        }
	}

    public void ShootArrow()
    {
        if (prefab_EnemyArrow != null)
        {
            GameObject temp = Instantiate(prefab_EnemyArrow);
            temp.transform.position = transform.position;
            temp.transform.parent = transform;
        }
    }
    public virtual void EnemyTakeDamage()
    {
        health--;
        if (health == 0)
        {
            Destroy(this.gameObject);
            PlayerControllerMrGun2.Instance.TOTALENEMYDESTROYED++;
        }
    }
}