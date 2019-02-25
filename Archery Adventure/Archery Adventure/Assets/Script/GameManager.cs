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
    GameObject [] platForm;
    [SerializeField]
    GameObject prefab_Enemy,mainPlatform;
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
    }
    void Start()
    {
        curEnemy = -1;      
        InitGame();
        SpawnEnemy();
    }    
    // Update is called once per frame
    void Update()
    {
        if (isPlayCanRun)
        {
           // PlayerController.Instance.PlayerRunToDestination();           
            mainPlatform.transform.Translate(-0.07f, 0, 0);
        }            
    }
    void InitGame()
    {
        if (platForm == null)
        {
            platForm = new GameObject[sumEnemy];
        }
        float min = 0.7f;
        float max = 0.1f;
          
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
        if(curEnemy >= sumEnemy)
        {
            UIController.Instance.SetWinning(true);
        }
        else
        {
            GameObject Ene = Instantiate(prefab_Enemy);
            float platformHeight = platForm[curEnemy].GetComponent<SpriteRenderer>().size.y;
            Ene.transform.position = platForm[curEnemy].transform.position;
            Ene.transform.Translate(0, platformHeight, 0);
            PlayerController.Instance.SetCannotSpawn(false);
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
