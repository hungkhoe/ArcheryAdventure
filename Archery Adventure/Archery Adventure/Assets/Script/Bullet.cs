using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    Vector2 direction = new Vector2(0, 0);
    Rigidbody2D rigid;
    float power;
    GameObject player;
    int damage;
    private void Awake()
    {
        player = GameObject.Find("Player");
        Physics2D.IgnoreCollision(player.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());        
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 v = rigid.velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetRotationAngle(float _angle)
    {
        this.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }

    public void SetPowerDirection(float _power , Vector3 _direction)
    {
        direction = _direction;
        power = _power;
        rigid.velocity = direction * power / 6.5F;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }     
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
}
