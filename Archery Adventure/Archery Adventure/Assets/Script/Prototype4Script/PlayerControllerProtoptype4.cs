using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerProtoptype4 : MonoBehaviour {

    // Use this for initialization
   
    [SerializeField]
    GameObject [] dotArray;
    LineRenderer lineCheck;
    [SerializeField]
    GameObject dottest,prefab_Bullet;
    [SerializeField]
    float angleMeasurement,bulletAngleRotation;    
    Vector2 direction;
    Rigidbody2D rb;
    static PlayerControllerProtoptype4 instance;
    int score = 0;

    bool isShooting, goUp, goDown,isRunning,isStarGame;

    public static PlayerControllerProtoptype4 Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start () {
        InitPlayer();   
        ResetAdjustDirection();
        isShooting = true;
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (isStarGame)
        {
            if (isShooting)
            {
                AdjustDirection();
                if (Input.GetMouseButtonDown(0))
                {
                    FireGun();
                }
            }
            else
            {
                if (isRunning)
                    rb.velocity = Vector2.right * 2.5f;
            }
        }
#endif
#if UNITY_ANDROID
        if (isStarGame)
        {
            if (isShooting)
            {
                AdjustDirection();
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        FireGun();
                    }
                }
            }
            else
            {
                if (isRunning)
                    rb.velocity = Vector2.right * 2.5f;
            }
        }
#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Destination")
        {
            lineCheck.enabled = true;
            isShooting = true;
            GameControllerPrototype4.Instance.SetPlayerRuning(false);
            Destroy(collision.gameObject);
            ResetAdjustDirection();
            GameControllerPrototype4.Instance.DestroyPreviousPlatForm();
            rb.velocity = Vector2.zero;
            isRunning = false;
        }
    }
   
    void AdjustDirection()
    {      
        Vector2 target = dottest.transform.position - this.transform.position;
        angleMeasurement = AngleBetweenVector2(transform.right, target);
        if (goUp)
        {
            dottest.transform.RotateAround(transform.position, Vector3.forward, 40 * Time.deltaTime);
            lineCheck.SetPosition(1, new Vector3(dottest.transform.position.x, dottest.transform.position.y, -1));           
            if (angleMeasurement >= 70)
            {
                goUp = false;
                goDown = true;
            }
        }
        else if (goDown)
        {         
            dottest.transform.RotateAround(transform.position, Vector3.back, 40 * Time.deltaTime);
            lineCheck.SetPosition(1, new Vector3(dottest.transform.position.x, dottest.transform.position.y, -1));
            if (angleMeasurement <= 0)
            {
                goUp = true;
                goDown = false;
            }
        }                      
    }

    void InitPlayer()
    {
        lineCheck = gameObject.AddComponent<LineRenderer>();
        lineCheck.enabled = false;
        lineCheck.SetWidth(0.03f, 0.03f);
        lineCheck.SetVertexCount(2);
        goUp = true;
        dottest.SetActive(false);
    }

    void ResetAdjustDirection()
    {        
        //dotArray[0].transform.position = this.transform.position;
        lineCheck.SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, -1));
        lineCheck.SetPosition(1, new Vector3(this.transform.position.x + 0.7f, this.transform.position.y, -1));
        dottest.transform.position = new Vector3(this.transform.position.x + 0.7f, this.transform.position.y, -1);     
    }

    private float AngleBetweenVector2(Vector2 source, Vector2 target)
    {
        float angle = Mathf.DeltaAngle(Mathf.Atan2(source.y, source.x) * Mathf.Rad2Deg,
                            Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
        return angle;
    }

    public void SetIsShooting(bool _isShooting)
    {
        isShooting = _isShooting;
    }

    public void SetIsRunning(bool _isRunning)
    {
        isRunning = _isRunning;
    }

    public void FireGun()
    {
        lineCheck.enabled = false;
        isShooting = false;
        direction = dottest.transform.position - transform.position;
        bulletAngleRotation = 
            Quaternion.FromToRotation(transform.right, dottest.transform.position - transform.position).eulerAngles.z;
        GameObject bullet = Instantiate(prefab_Bullet, this.transform);
        bullet.transform.position = transform.position;
        bullet.GetComponent<PlayerBulletPrototyp4>().SetDirection(direction);
        bullet.GetComponent<PlayerBulletPrototyp4>().SetRotationAngle(bulletAngleRotation);   
    }

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore()
    {
        score++;
    }

    public void SetStarGame()
    {
        isStarGame = true;
        lineCheck.enabled = true;
    }
}
