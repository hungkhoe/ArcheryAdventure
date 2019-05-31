using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPrototyp4 : MonoBehaviour {

    // Use this for initialization
    Rigidbody2D rb;
    Vector2 direction;
    int health;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        health = 1;
    }

    // Update is called once per frame
    void Update() {
        if (rb != null)
        {
            Vector2 v = rb.velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void SetDirection(Vector2 _direction)
    {
        direction = _direction;
        rb.velocity = direction * 15;
    }

    protected virtual void LateUpdate()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.x > 1 || screenPoint.y < 0)
        {
            Destroy(this.gameObject);
            GameControllerPrototype4.Instance.EnemyShootArrow();
        }
    }

    public void SetRotationAngle(float _angle)
    {
        this.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            GameControllerPrototype4.Instance.SetPlayerRuning(true);
            GameControllerPrototype4.Instance.SpawnPlatform();
            PlayerControllerProtoptype4.Instance.SetIsRunning(true);
            PlayerControllerProtoptype4.Instance.IncreaseScore();
        }
    }   
}
