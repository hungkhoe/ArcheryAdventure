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
      
    }
    void Start () {
        isShooting = false;
    }
	
	// Update is called once per frame
	void Update () {
       
    }   

    protected void ShootArrow()
    {
        GameObject temp = Instantiate(prefab_enemyArrow);
        temp.transform.position = this.transform.position;
        temp.GetComponent<EnemyBullet>().SetDamage(damage);        
        isShooting = true;
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
		if (health > 0) {

			//GetComponent<MovingController> ().Move ();
			//GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0);
			GetComponent<MovingController>().Move();
		} else {
		
			
		}

		GameController.Instance.NextStep ();
		//Player.Instance.GetComponent<MovingController> ().Move ();
    }

    protected virtual void LateUpdate()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetShootingBool(bool _isShooting)
    {
        isShooting = _isShooting;
    }
}
