using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    Vector2 direction = new Vector2(0, 0);
    Rigidbody2D rigid;
    Collider2D col2d;
    float power;
    GameObject player;
    protected int damage;
    protected bool hit = false;
    private void Awake()
    {
        player = GameObject.Find("Player");
        Physics2D.IgnoreCollision(player.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());        
        rigid = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }
    void Start () {
        
    }

	protected virtual void LateUpdate()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if(screenPoint.x > 1 || screenPoint.y < 0)
        {
            Destroy(this.gameObject);

			//GameController.Instance.EnemyAttack ();
        }     
    }

    // Update is called once per frame
    void Update () {
        if(rigid != null)
        {
            Vector2 v = rigid.velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }      
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
  
	protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!hit)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                ArrowStick(collision);
                hit = true;
            }
        }
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    protected void ArrowStick(Collider2D col)
    {       
        transform.parent = col.transform;        
        Destroy(rigid);
        Destroy(col2d);
    }
}
