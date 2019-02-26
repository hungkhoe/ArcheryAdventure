using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFireBall : MonoBehaviour {

    // Use this for initialization
    Rigidbody2D rb;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = Vector2.right * 2;
	}
    protected void LateUpdate()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.x > 1 || screenPoint.y < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
