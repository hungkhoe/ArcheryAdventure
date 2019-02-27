using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerPrototype4 : MonoBehaviour {

    // Use this for initialization
    static GameControllerPrototype4 instance;

    [SerializeField]
    GameObject prefab_platform,prefab_Enemy,enviroment,first_platform, previous_platform,current_platform,enemyPlatform,currentEnemy;
    [SerializeField]
    float platformWitdh,enemyPlatformHeight;
    bool isPlayerRunning,isLosing;
    bool firstTime,startGame;
    Rigidbody2D cameraRB;

    void Awake()
    {
        instance = this;
        Physics2D.IgnoreLayerCollision(9, 11);
    }

    public static GameControllerPrototype4 Instance
    {
        get
        {
            return instance;
        }
    }

    void Start () {
        InitGame();
	}	

	void Update () {
        if(startGame)
        {
            if (isLosing)
            {
               
            }
            else
            {
                if (isPlayerRunning)
                {
                    cameraRB.velocity = Vector2.right * 2.5f;
                }
                else
                {
                    cameraRB.velocity = Vector2.zero;
                }
            }
        }               
    }

    public void SpawnPlatform()
    {
        if(firstTime)
        {
            GameObject temp = Instantiate(prefab_platform, enviroment.transform);
            temp.transform.position = new Vector3(first_platform.transform.position.x + platformWitdh - 1, first_platform.transform.position.y, 0);
            previous_platform = first_platform;
            current_platform = temp;
            firstTime = false;
            platformWitdh = current_platform.GetComponent<SpriteRenderer>().bounds.size.x;
            currentEnemy = current_platform.transform.GetChild(0).GetChild(0).gameObject;
        }
        else
        {
            GameObject temp = Instantiate(prefab_platform, enviroment.transform);
            temp.transform.position = new Vector3(current_platform.transform.position.x + platformWitdh, current_platform.transform.position.y, 0);
            previous_platform = current_platform;
            current_platform = temp;
            currentEnemy = current_platform.transform.GetChild(0).GetChild(0).gameObject;
        }
    }

    public void SetPlayerRuning(bool _isPlayerRunning)
    {
        isPlayerRunning = _isPlayerRunning;
    }

    public void DestroyPreviousPlatForm()
    {
        Destroy(previous_platform.gameObject);
    }

    public void EnemyShootArrow()
    {
        currentEnemy.GetComponent<EnemyScriptPrototype4>().ShootArrow();
    }

    public void SetGameLose()
    {
        isLosing = true;
        UIControllerPrototype4.Instance.SetLose();
    }

    public void InitGame()
    {
        platformWitdh = first_platform.GetComponent<SpriteRenderer>().bounds.size.x;
        previous_platform = first_platform;
        current_platform = first_platform;
        firstTime = true;
        cameraRB = Camera.main.GetComponent<Rigidbody2D>();
        isPlayerRunning = false;
        currentEnemy.SetActive(false);
        enemyPlatform.SetActive(false);
    }   

    public void StartGame()
    {
        currentEnemy.SetActive(true);
        enemyPlatform.SetActive(true);
        startGame = true;
    }
}
