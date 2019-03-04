using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMrGun2 : MonoBehaviour {
  
    LineRenderer lineCheck;
    [SerializeField]
    GameObject dottest, prefab_Bullet;
    [SerializeField]
    float angleMeasurement, bulletAngleRotation;
    Vector2 direction;
    Rigidbody2D rb;
    static PlayerControllerMrGun2 instance;
    int score = 0;
    int totalEnemyDestroyedThisPlatform = 0;

    bool isShooting, goUp, goDown, isRunning, isStarGame;

    [SerializeField]
    GameObject[] dot_testArray;
    [SerializeField]
    GameObject prefab_DotTest;
    bool isHolding,isDestroyingDotTest;
    float powerMeasurement, storePower;
    Vector3 testingHeading;

    public static PlayerControllerMrGun2 Instance
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

    void Start()
    {
        InitPlayer();
        ResetAdjustDirection();     
        rb = GetComponent<Rigidbody2D>();
        CreateCircleDistance();
        SetStarGame();
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        //if (isStarGame)
        //{
        //    if (isShooting)
        //    {
        //        AdjustDirection();
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            FireGun();
        //        }
        //    }
        //    else
        //    {
        //        if (isRunning)
        //            rb.velocity = Vector2.right * 2.5f;
        //    }
        //}

        if(isStarGame)
        {
            if(isShooting)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    startPoint.z = 15;
                    dot_testArray[0].transform.position = startPoint;
                    dot_testArray[0].SetActive(true);
                    dot_testArray[1].SetActive(true);
                }

                if (Input.GetMouseButton(0))
                {
                    isHolding = true;
                    isDestroyingDotTest = true;
                    dot_testArray[0].SetActive(true);
                    dot_testArray[1].SetActive(true);
                    AdjustFirePower();
                }
                else
                {
                    if (isHolding == true)
                    {
                        isHolding = false;
                        if (isDestroyingDotTest)
                        {
                            ClearDotTest();
                        }
                    }
                }
            }
            else
            {
                if(isRunning)
                {
                    rb.velocity = Vector2.right * 2.5f;
                }
            }
        }
#endif
#if UNITY_ANDROID
        //if (isStarGame)
        //{
        //    if(isShooting)
        //    {
        //        if (Input.touchCount > 0)
        //        {
        //            Touch touch = Input.GetTouch(0);
        //            if (touch.phase == TouchPhase.Began)
        //            {
        //                Vector3 startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //                startPoint.z = 15;
        //                dot_testArray[0].transform.position = startPoint;
        //                dot_testArray[0].SetActive(true);
        //                dot_testArray[1].SetActive(true);
        //            }
        //            if (touch.phase == TouchPhase.Moved)
        //            {
        //                isHolding = true;
        //                isDestroyingDotTest = true;
        //                dot_testArray[0].SetActive(true);
        //                dot_testArray[1].SetActive(true);
        //                AdjustFirePower();
        //            }
        //        }
        //        else
        //        {
        //            if (isHolding == true)
        //            {
        //                isHolding = false;
        //                if (isDestroyingDotTest)
        //                {
        //                    ClearDotTest();
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (isRunning)
        //        {
        //            rb.velocity = Vector2.right * 2.5f;
        //        }
        //    }
        //}
#endif
        //#if UNITY_ANDROID
        //        if (isStarGame)
        //        {
        //            if (isShooting)
        //            {
        //                AdjustDirection();
        //                if (Input.touchCount == 1)
        //                {
        //                    Touch touch = Input.GetTouch(0);

        //                    if (touch.phase == TouchPhase.Began)
        //                    {
        //                        FireGun();
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (isRunning)
        //                    rb.velocity = Vector2.right * 2.5f;
        //            }
        //        }
        //#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Destination")
        {
            lineCheck.enabled = true;
            isShooting = true;
            GameControllerMrGun2.Instance.SetPlayerRuning(false);
            Destroy(collision.gameObject);
            ResetAdjustDirection();
            GameControllerMrGun2.Instance.DestroyPreviousPlatForm();
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
            if (angleMeasurement >= 80)
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
        if(isRunning)
        {
            lineCheck.enabled = false;
        }
    }

    public void FireGun()
    {
        // TODO : uncoment
        //lineCheck.enabled = false;
        //isShooting = false;
        direction = dottest.transform.position - transform.position;
        bulletAngleRotation =
            Quaternion.FromToRotation(transform.right, dottest.transform.position - transform.position).eulerAngles.z;
        GameObject bullet = Instantiate(prefab_Bullet, this.transform);
        bullet.transform.position = transform.position;
        bullet.GetComponent<PlayerBulletMrGun2>().SetDirection(direction);
        bullet.GetComponent<PlayerBulletMrGun2>().SetRotationAngle(bulletAngleRotation);
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
        isShooting = true;
    }

    public int TOTALENEMYDESTROYED
    {
        get
        {
            return totalEnemyDestroyedThisPlatform;
        }
        set
        {
            totalEnemyDestroyedThisPlatform = value;
        }
    }

    void PlayerControllerPullRelease()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
            dot_testArray[0].transform.position = startPoint;
            dot_testArray[0].SetActive(true);
            dot_testArray[1].SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            isHolding = true;
            isDestroyingDotTest = true;
            dot_testArray[0].SetActive(true);
            dot_testArray[1].SetActive(true);
            AdjustFirePower();
        }
        else
        {
            if (isHolding == true)
            {
                isHolding = false;
                if (isDestroyingDotTest)
                {
                    ClearDotTest();
                }
            }
        }
    }

    void CreateCircleDistance()
    {
        dot_testArray = new GameObject[2];
        for (int i = 0; i < 2; i++)
        {
            dot_testArray[i] = Instantiate(prefab_DotTest);
            Vector3 convertPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            convertPositon.z = 15;
            dot_testArray[i].transform.position = convertPositon;
            dot_testArray[i].SetActive(false);
        }        
    }

    void ClearDotTest()
    {
        for (int i = 0; i < dot_testArray.Length; i++)
        {
            dot_testArray[i].SetActive(false);
        }

        Vector3 pos1 = dot_testArray[0].transform.position;
        Vector3 pos2 = dot_testArray[1].transform.position;
        testingHeading = Vector3.Normalize(pos1 - pos2);

        storePower = powerMeasurement;

        if (Vector3.Distance(dot_testArray[0].transform.position, dot_testArray[1].transform.position) > 1)
        {
            GameObject temp = Instantiate(prefab_Bullet,this.transform);
            PlayerBulletMrGun2 prefabBullet = temp.GetComponent<PlayerBulletMrGun2>();            
            prefabBullet.SetRotationAngle(angleMeasurement);
            prefabBullet.SetPowerDirection(powerMeasurement, testingHeading);           
        }

        lineCheck.enabled = false;
        powerMeasurement = 0;
        angleMeasurement = 0;
    }

    void AdjustFirePower()
    {
        if (dot_testArray[1] != null)
        {
            lineCheck.enabled = true;
            Vector3 convertPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                convertPositon = Camera.main.ScreenToWorldPoint(touch.position);
            }
#endif
            convertPositon.z = 15;
            dot_testArray[1].transform.position = convertPositon;

            lineCheck.SetPosition(0, new Vector3(dot_testArray[0].transform.position.x, dot_testArray[0].transform.position.y, -1));
            lineCheck.SetPosition(1, new Vector3(dot_testArray[1].transform.position.x, dot_testArray[1].transform.position.y, -1));

            powerMeasurement = (Vector3.Distance(dot_testArray[0].transform.position, dot_testArray[1].transform.position)) * 35;
            Vector3 targerDirection = dot_testArray[1].transform.position - dot_testArray[0].transform.position;

            Vector3 targetDir = dot_testArray[1].transform.position - dot_testArray[0].transform.position;

            angleMeasurement =
                Quaternion.FromToRotation(dot_testArray[0].transform.right, dot_testArray[1].transform.position - dot_testArray[0].transform.position).eulerAngles.z - 180;

            if (powerMeasurement >= 100)
            {
                powerMeasurement = 100;
            }

            float angle = 0;
            //if(GetComponent<MovingController>().dir>0)
            angle = Mathf.Atan2(dot_testArray[1].transform.position.y - dot_testArray[0].transform.position.y, dot_testArray[1].transform.position.x - dot_testArray[0].transform.position.x) * 180 / Mathf.PI - 180;
            //else angle = Mathf.Atan2(dot_testArray[1].transform.position.y-dot_testArray[0].transform.position.y, dot_testArray[1].transform.position.x-dot_testArray[0].transform.position.x)*180 / Mathf.PI;           
        }
    }
}
