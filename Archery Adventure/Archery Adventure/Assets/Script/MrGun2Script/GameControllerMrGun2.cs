using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerMrGun2 : MonoBehaviour {

    // Use this for initialization
    static GameControllerMrGun2 instance;

    [SerializeField]
    GameObject prefab_platform, enviroment,previous_platform, current_platform;
    [SerializeField]
    float platformWitdh, enemyPlatformHeight;
    bool isPlayerRunning, isLosing;
    bool firstTime, startGame;
    Rigidbody2D cameraRB;
    int totalEnemyNeedToDestroy;
    void Awake()
    {
        instance = this;
        Physics2D.IgnoreLayerCollision(9, 11);
    }

    public static GameControllerMrGun2 Instance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {
        InitGame();
        StartGame();
    }

    void Update()
    {
        if (startGame)
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
        GameObject temp = Instantiate(prefab_platform, enviroment.transform);
        temp.transform.position = new Vector3(current_platform.transform.position.x + platformWitdh - 0.5f, current_platform.transform.position.y, 0);
        previous_platform = current_platform;
        current_platform = temp;
        totalEnemyNeedToDestroy = current_platform.GetComponent<PlatformScriptMrGun2>().GetTotalEnemy();
        PlayerControllerMrGun2.Instance.TOTALENEMYDESTROYED = 0;
        PlayerControllerMrGun2.Instance.SetIsRunning(true);
        PlayerControllerMrGun2.Instance.SetIsShooting(false);
        isPlayerRunning = true;
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
        
    }

    public void SetGameLose()
    {
        isLosing = true;
        UIControllerPrototype4.Instance.SetLose();
    }

    public void InitGame()
    {
        platformWitdh = current_platform.GetComponent<SpriteRenderer>().bounds.size.x;
        previous_platform = current_platform;   
        firstTime = true;
        cameraRB = Camera.main.GetComponent<Rigidbody2D>();
        isPlayerRunning = false;
        totalEnemyNeedToDestroy = current_platform.GetComponent<PlatformScriptMrGun2>().GetTotalEnemy();
    }

    public void StartGame()
    {        
        startGame = true;
    }

    public int GetTotalEnemyNeedToDestroyed()
    {
        return totalEnemyNeedToDestroy;
    }
}
