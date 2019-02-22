using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

	protected override void LateUpdate()
	{
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
		if(screenPoint.x > 1 || screenPoint.y < 0)
		{
			Destroy(this.gameObject);		
			GameController.Instance.EnemyAttack ();
		}     
	}

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!hit)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                ArrowStick(collision);
                hit = true;
            }
        }
    }
}
