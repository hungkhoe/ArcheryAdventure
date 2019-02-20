using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected GameObject prefab_enemyArrow;
    [SerializeField]
    protected bool isShooting;   
    private void Awake()
    {
        isShooting = false;
    }
    void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {
       
    }   

    protected void ShootArrow()
    {
        GameObject temp = Instantiate(prefab_enemyArrow);
        temp.transform.position = this.transform.position;
        temp.GetComponent<EnemyBullet>().SetDamage(damage);
        //transform.rotation = Quaternion.AngleAxis(_angle, Vector3.back);       
        isShooting = true;
    }

    public void TakeDamage(int _damage)
    {
        health -= damage;
    }

    private void LateUpdate()
    {
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
