using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMrGun2 : MonoBehaviour {

    Rigidbody2D rigid;
    Collider2D col2D;
    float widthPlayer;
    SpriteRenderer spriteRenderPlayer;
    bool hit;

    void Start()
    {
        InitArrow();
    }

    // Update is called once per frame
    void Update()
    {
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
        arch = Random.Range(0.2f, 0.5f);
        Mathf.Clamp(arch, 0, 1);
        var origin = rigidbody2D.position;
        float x = target.x - origin.x;
        float y = target.y - origin.y;
        x = Random.Range(x - widthPlayer, x + widthPlayer);
        float gravity = -Physics2D.gravity.y;
        float b = force * force - y * gravity;
        float discriminant = b * b - gravity * gravity * (x * x + y * y);
        if (discriminant < 0)
        {

        }
        else
        {
            float discriminantSquareRoot = Mathf.Sqrt(discriminant);
            float minTime = Mathf.Sqrt((b - discriminantSquareRoot) * 2) / Mathf.Abs(gravity);
            float maxTime = Mathf.Sqrt((b + discriminantSquareRoot) * 2) / Mathf.Abs(gravity);
            float time = (maxTime - minTime) * arch + minTime;
            float vx = x / time;
            float vy = y / time + time * gravity / 2;
            var trajectory = new Vector2(vx, vy);
            rigidbody2D.AddForce(trajectory, ForceMode2D.Impulse);
        }

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!hit)
            {
                ArrowStick(collision);           
                hit = true;
                PlayerControllerMrGun2.Instance.TakeDamage();
            }
        }
    }

    protected void ArrowStick(Collider2D col)
    {
        transform.parent = col.transform;
        Destroy(rigid);
        Destroy(col2D);
    }

    void InitArrow()
    {
        rigid = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        GameObject player = GameObject.Find("new_player");
        spriteRenderPlayer = player.GetComponentInChildren<SpriteRenderer>();
        widthPlayer = spriteRenderPlayer.bounds.size.x;
        SetTrajectory(GetComponent<Rigidbody2D>(), PlayerControllerMrGun2.Instance.gameObject.transform.position, 10);
    }
}
