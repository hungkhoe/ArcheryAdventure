using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyArrow : MonoBehaviour {

    // Use this for initialization
    Rigidbody2D rigid;
    Collider2D col2D;
    float widthPlayer;
    SpriteRenderer spriteRenderPlayer;
    bool hit;
    int damage = 1;
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        GameObject player = GameObject.Find("Player");
        spriteRenderPlayer = player.GetComponentInChildren<SpriteRenderer>();
        widthPlayer = spriteRenderPlayer.size.x;
        SetTrajectory(GetComponent<Rigidbody2D>(), PlayerController.Instance.gameObject.transform.position, 15);      
    }  

    // Update is called once per frame
    void Update () {
        if (rigid != null)
        {
            Vector2 v = rigid.velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    protected void LateUpdate()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.x > 1 || screenPoint.y < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTrajectory(Rigidbody2D rigidbody2D, Vector2 target, float force, float arch = 0.8f)
    {
        arch = Random.Range(0.1f, 0.6f);
        Mathf.Clamp(arch, 0, 1);
        var origin = rigidbody2D.position;            
        target.x = Random.Range(-widthPlayer / 3f + target.x, widthPlayer/ 3f + target.x);
        float x = target.x - origin.x;
        float y = target.y - origin.y;           
        float gravity = -Physics2D.gravity.y;
        float b = force * force - y * gravity;
        float discriminant = b * b - gravity * gravity * (x * x + y * y);       
        float discriminantSquareRoot = Mathf.Sqrt(discriminant);
        float minTime = Mathf.Sqrt((b - discriminantSquareRoot) * 2) / Mathf.Abs(gravity);
        float maxTime = Mathf.Sqrt((b + discriminantSquareRoot) * 2) / Mathf.Abs(gravity);
        float time = (maxTime - minTime) * arch + minTime;
        float vx = x / time;
        float vy = y / time + time * gravity / 2;
        var trajectory = new Vector2(vx, vy);
        rigidbody2D.AddForce(trajectory, ForceMode2D.Impulse);        
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!hit)
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                ArrowStick(collision);
                hit = true;
            }
        }
    }

    protected void ArrowStick(Collider2D col)
    {
        transform.parent = col.transform;
        Destroy(rigid);
        Destroy(col2D);
    }
}
