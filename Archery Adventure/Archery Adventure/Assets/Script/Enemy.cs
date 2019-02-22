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
    protected GameObject prefab_enemyArrow;

    [SerializeField]
    protected bool isShooting; 

	[SerializeField]
	GameObject bow;

	protected GameObject player;
    private void Awake()
    {
      
    }
    void Start () {
        isShooting = false;
    }
	
	// Update is called once per frame
	void Update () {
       
    }   

	public void ShootArrow()
    {
        GameObject temp = Instantiate(prefab_enemyArrow);
        temp.transform.position = this.transform.position;
        temp.GetComponent<EnemyBullet>().SetDamage(damage);        
        isShooting = true;
    }

	public void Attack(){

		Vector3 target = PlayerController.Instance.transform.position;

		target = new Vector3 (target.x, target.y + Static.HEIGHT_PLAYER/2 + Random.Range(Static.ENEMY_MIN_HEIGHT_AIM, Static.ENEMY_MAX_HEIGHT_AIM), target.z);
		//float randX = Random.Range ();

		float angle = 0;
		if(GetComponent<MovingController>().dir>0)
			angle = Mathf.Atan2(bow.transform.position.y - target.y,bow.transform.position.x - target.x)*180 / Mathf.PI - 180;
		else angle = Mathf.Atan2(bow.transform.position.y - target.y,bow.transform.position.x - target.x)*180 / Mathf.PI;

		bow.transform.rotation = Quaternion.Euler (bow.transform.rotation.x, bow.transform.rotation.y, bow.transform.rotation.z + angle);

		GameObject arrow = Instantiate(prefab_enemyArrow);
		EnemyBullet prefabBullet = arrow.GetComponent<EnemyBullet>();
		prefabBullet.SetDamage(damage);
		prefabBullet.SetRotationAngle(angle);

		float power = 0;

		if (PlayerController.Instance.DirectPlayer < 0) {
		
			power = (PlayerController.Instance.transform.position.x / Static.MaxX) * Static.ENEMY_MAX_POWER;
		} else
			power = (PlayerController.Instance.transform.position.x / Static.MinX) * Static.ENEMY_MAX_POWER;

		prefabBullet.SetPowerDirection(power, (target - bow.transform.position).normalized);
		arrow.transform.position = bow.transform.position;
	}

    public void TakeDamage(int _damage)
    {
        health -= _damage;
		if (health > 0) {

			GetComponent<MovingController>().Move();
			GameController.Instance.NextStep ();
		} else {
		
			
		}

		//GameController.Instance.NextStep ();
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
