using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet {

    // Use this for initialization
   
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (!hit)
			{
				collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
				ArrowStick(collision);
				hit = true;
				PlayerController.Instance.CanShooting = true;
			}
		}
	}


	protected override void LateUpdate()
	{
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
		if(screenPoint.x > 1 || screenPoint.y < 0)
		{
			Destroy(this.gameObject);

			PlayerController.Instance.CanShooting = true;
		}     
	}

}
