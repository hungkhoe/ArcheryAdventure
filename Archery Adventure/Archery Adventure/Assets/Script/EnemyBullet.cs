using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    // Use this for initialization
    GameObject player;
    GameObject enemySelf;
    Player playerScript;
    Vector3 direction;
    Rigidbody2D rb;
    int damage;
    private void Awake()
    {   
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
    }
    void Start()
    {
        Vector3 pos1 = player.transform.position;
        Vector3 pos2 = transform.position;
        direction = Vector3.Normalize(pos1 - pos2);
        float angleMeasurement =
               Quaternion.FromToRotation(transform.right, player.transform.position - transform.position).eulerAngles.z + 100;
        this.transform.rotation = Quaternion.AngleAxis(angleMeasurement, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * 9.5f;
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerScript.TakeDamage(1);
            Destroy(this.gameObject);
        }
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
}
