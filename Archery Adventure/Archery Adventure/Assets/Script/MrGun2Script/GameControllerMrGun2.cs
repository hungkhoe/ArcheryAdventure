using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerMrGun2 : MonoBehaviour {

    // Use this for initialization
    static GameControllerMrGun2 instance;

    [SerializeField]
    GameObject prefab_platform, enviroment, previous_platform, current_platform, first_platform;
    [SerializeField]
    GameObject[] platformArray;
    float platformWitdh, enemyPlatformHeight;
    bool isPlayerRunning, isLosing, isWinning;
    bool firstTime, startGame;
    Rigidbody2D cameraRB;
    int totalEnemyNeedToDestroy, currentPlatform;
    int loadLevel;
    [SerializeField]
    int currentLevel;
    void Awake()
    {
        instance = this;        
        Physics2D.IgnoreLayerCollision(9, 11);
        if (PlayerPrefs.HasKey("LevelProgress"))  // check if we already save It before
        {
            loadLevel = PlayerPrefs.GetInt("LevelProgress");
        }
        else
        {
            loadLevel = 1;
        }

        if (loadLevel != currentLevel)
        {
            Debug.Log("different level");
            if (loadLevel == 1)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MrGun2SceneLevel");
            }
            else if (loadLevel == 2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MrGun2SceneLevel2");
            }
            else if (loadLevel == 3)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MrGun2SceneLevel3");
            }
        }
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
        // StartGame();
       
    }

    void Update()
    {
        if (startGame)
        {
            if (isWinning)
            {

            }
            if (isLosing)
            {

            }            
        }
    }

    public void SpawnPlatform()
    {
        currentPlatform++;
        if (currentPlatform < platformArray.Length)
        {
            //GameObject temp = Instantiate(prefab_platform, enviroment.transform);
            //temp.transform.position = new Vector3(current_platform.transform.position.x + platformWitdh - 0.5f, current_platform.transform.position.y, 0);
            //previous_platform = current_platform;
            //current_platform = temp;
            platformArray[currentPlatform].SetActive(true);
            totalEnemyNeedToDestroy = platformArray[currentPlatform].GetComponent<PlatformScriptMrGun2>().GetTotalEnemy();
            PlayerControllerMrGun2.Instance.TOTALENEMYDESTROYED = 0;
            PlayerControllerMrGun2.Instance.SetIsRunning(true);
            PlayerControllerMrGun2.Instance.SetIsShooting(false);
            SetCameraRun();
            isPlayerRunning = true;
        }
        else
        {
            SetGameWin();
        }
    }

    public void SetPlayerRuning(bool _isPlayerRunning)
    {
        isPlayerRunning = _isPlayerRunning;
    }

    public void DestroyPreviousPlatForm()
    {
        //Destroy(previous_platform.gameObject);
        int temp = currentPlatform - 1;
        Destroy(platformArray[temp]);
    }

    public void EnemyShootArrow()
    {

    }

    public void SetGameLose()
    {
        isLosing = true;        
    }

    public void SetGameWin()
    {
        isWinning = true;
        UIControllerMrGun2.Instance.SetWin();
        LevelInformationMrGun2.Instance.SaveLevel();
    }

    public void InitGame()
    {
        platformWitdh = current_platform.GetComponent<SpriteRenderer>().bounds.size.x;
        previous_platform = current_platform;
        firstTime = true;
        cameraRB = Camera.main.GetComponent<Rigidbody2D>();
        isPlayerRunning = false;
        totalEnemyNeedToDestroy = current_platform.GetComponent<PlatformScriptMrGun2>().GetTotalEnemy();
        first_platform.SetActive(false);
    }

    public void StartGame()
    {
        startGame = true;
        first_platform.SetActive(true);
    }

    public int GetTotalEnemyNeedToDestroyed()
    {
        return totalEnemyNeedToDestroy;
    }

    public void SetCameraRun()
    {
        cameraRB.velocity = Vector2.right * 3.5f;
    }

    public void SetCameraStop()
    {
        cameraRB.velocity = Vector3.zero;
    }
}
