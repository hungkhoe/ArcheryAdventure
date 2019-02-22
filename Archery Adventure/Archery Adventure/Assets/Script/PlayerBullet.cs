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
}
