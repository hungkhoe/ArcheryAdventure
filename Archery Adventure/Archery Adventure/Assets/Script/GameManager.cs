using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // Use this for initialization 
    
    static GameManager instance;
    [SerializeField]
    int sumEnemy;
    [SerializeField]
    GameObject [] platForm,enemySpawn;
    [SerializeField]
    GameObject prefab_Enemy,mainPlatform,prefab_Enemy2;
    int curEnemy;
    bool isPlayCanRun;
    Rigidbody2D mainPlatformRB;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        Physics2D.IgnoreLayerCollision(9, 11);
        Physics2D.IgnoreLayerCollision(9, 14);
        Physics2D.IgnoreLayerCollision(14, 15);
        Physics2D.IgnoreLayerCollision(10, 15);
    }

    void Start()
    {
        curEnemy = -1;      
        InitGame();        
    }    
    // Update is called once per frame
    void Update()
    {
        if (isPlayCanRun)
        {
           // PlayerController.Instance.PlayerRunToDestination();           
            mainPlatform.transform.Translate(-0.02f, 0, 0);
        }            
    }

    void InitGame()
    {
        if (platForm == null)
        {
            platForm = new GameObject[sumEnemy];
        }
        float min = 0.75f;
        float max = 0.3f;
          
        for(int i = 0; i < platForm.Length;i++)
        {
            Vector2 final = Camera.main.ViewportToWorldPoint(new Vector2(1, Random.Range(min, max)));
            final.x = platForm[i].transform.position.x;
            platForm[i].transform.position = final;
        }        
    }

    public void SpawnEnemy()
    {     
        curEnemy++;     
        if (curEnemy >= sumEnemy)
        {
            UIController.Instance.SetWinning(true);
            PlayerController.Instance.SetWinning();
            isPlayCanRun = false;
        }
        else
        {
            GameObject Ene = Instantiate(prefab_Enemy);
            float platformHeight = platForm[curEnemy].GetComponent<SpriteRenderer>().size.y;
            Ene.transform.position = platForm[curEnemy].transform.position;
            Ene.transform.Translate(0, platformHeight * 1.5f, 0);
            //PlayerController.Instance.SetCannotSpawn(false);
            Ene.transform.SetParent(platForm[curEnemy].transform);       
        }
   
    }

    public void SpawnEnemyZombie()
    {
        if(curEnemy < sumEnemy)
        {
            GameObject Ene = Instantiate(prefab_Enemy2);
            Ene.transform.position = enemySpawn[curEnemy].transform.position;
        }    
    }

    public void SetPlayerCanRun()
    {
        isPlayCanRun = true;
    }

    public void SetPlayerCanNotRun()
    {
        isPlayCanRun = false;
    }
}
