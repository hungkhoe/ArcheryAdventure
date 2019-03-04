using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMrGun2 : MonoBehaviour {

    Rigidbody2D rb;
    Collider2D col2D;
    Vector2 direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        rb.velocity = direction * 16;
    }

    protected virtual void LateUpdate()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.x > 1 || screenPoint.y < 0)
        {
            Destroy(this.gameObject);           
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
            PlayerControllerMrGun2.Instance.TOTALENEMYDESTROYED++;
            if (PlayerControllerMrGun2.Instance.TOTALENEMYDESTROYED == GameControllerMrGun2.Instance.GetTotalEnemyNeedToDestroyed())
            {
                GameControllerMrGun2.Instance.SpawnPlatform();
            }        
        }
        if (collision.gameObject.tag == "Ground")
        {
            ArrowStick(collision);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    void ArrowStick(Collider2D col)
    {

        transform.parent = col.transform;
        Destroy(rb);
        Destroy(col2D);

    }
   

    public void SetPowerDirection(float _power, Vector3 _direction)
    {
        direction = _direction;
        rb.velocity = direction * _power / 9f;
    }
}
