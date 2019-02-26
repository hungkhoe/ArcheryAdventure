using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyZombie : MonoBehaviour {

    // Use this for initialization
    int damage;

	void Start () {
        damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
      transform.Translate(-0.03f, 0, 0);
	}

    private void LateUpdate()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.y < 0 || screenPoint.x < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        PlayerController.Instance.TakeDamage(damage);
    }
}
