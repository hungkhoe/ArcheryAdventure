using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	GameObject enemy;
    [SerializeField]
    GameObject losingPanel, winningPanel;

    bool isNotDead;
	int sumEnemy;
	int curStairEnemy;
	List<Enemy> listEnemy;
	int curEnemy;
	static GameController instance;
   
	public static GameController Instance{

		get{ 
		
			return instance;
		}
	}
	void Awake(){
	
		instance = this;
        //Physics2D.IgnoreLayerCollision(12, 11);
        //Physics2D.IgnoreLayerCollision(13, 10);
        //Physics2D.IgnoreLayerCollision(12, 8);
        //Physics2D.IgnoreLayerCollision(13, 8);
        //Physics2D.IgnoreLayerCollision(12, 9);
        //Physics2D.IgnoreLayerCollision(13, 9);
    }
	void Start () {

		sumEnemy = 3;
		curStairEnemy = 0;
		listEnemy = new List<Enemy> ();
        bool isNotDead = false;
		InitGame ();
	}
	
	// Update is called once per frame
	void Update () {      
	}

	void InitGame(){
	
		//Init Stairs
		StairController.Instance.InitStairs();
		//Set first position player
		PlayerController.Instance.GetComponent<MovingController> ().InitMoving ();

        //init Enemy
        for (int i = 0; i < sumEnemy; i++) {

            if (i == 0)
            {
                NormalEnemy enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy/Normal_Enemy"), StairController.Instance.transform).GetComponent<NormalEnemy>();
                enemy.transform.position = new Vector2(-100, -100);
                enemy.gameObject.SetActive(false);
                listEnemy.Add(enemy);
            }
            else if (i == 1)
            {
                MidEnemy enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy/Medium_Enemy"), StairController.Instance.transform).GetComponent<MidEnemy>();
                enemy.transform.position = new Vector2(-100, -100);
                enemy.gameObject.SetActive(false);
                listEnemy.Add(enemy);
            }

            else if (i == 2)
            {
                Boss enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy/Boss"), StairController.Instance.transform).GetComponent<Boss>();
                enemy.transform.position = new Vector2(-100, -100);
                enemy.gameObject.SetActive(false);
                listEnemy.Add(enemy);
            }       

        }

	}

	public void StartGame(){

        //spawn enemy for player shooted
        curEnemy = -1;
		curStairEnemy = 0;
		SpawnEnemy ();
	}

	public void NextStep(){

		StartCoroutine (StartNextStep());
	}
		
	IEnumerator StartNextStep(){

		yield return new WaitForSeconds (0.5f);
		PlayerController.Instance.GetComponent<MovingController> ().Move ();

		yield return new WaitUntil (()=>!listEnemy[curEnemy].GetComponent<MovingController>().IsMoving && !PlayerController.Instance.GetComponent<MovingController>().IsMoving);
		StairController.Instance.MoveDownStair ();
		PlayerController.Instance.CanShooting = true;

        if(isNotDead)
        {
            curStairEnemy++;
            isNotDead = false;
        }      
	}
		
	public void SpawnEnemy() {     
        Vector2 posSpawn = StairController.Instance.ListTrackPoint [++curStairEnemy];
		Enemy ene = listEnemy [++curEnemy];
		ene.gameObject.SetActive (true);

        if(posSpawn.x > 0)
        {
            ene.GetComponent<MovingController>().dir = -1;
        }
        else
        {
            ene.GetComponent<MovingController>().dir = 1;
        }		
		ene.GetComponent<MovingController> ().SetFlipX ();
		ene.GetComponent<MovingController> ().curIdTrack = PlayerController.Instance.GetComponent<MovingController> ().curIdTrack+1;

        if (ene.GetComponent<MovingController>().dir > 0)
        {
            ene.transform.position = new Vector2(Static.MinX - 1f, posSpawn.y + 0.5f);
        }
        else ene.transform.position = new Vector2(Static.MaxX + 1f, posSpawn.y + 0.5f);

        ene.transform.DOMoveX(posSpawn.x, 1.5f, false);    
    }

	public void EnemyAttack(){
	
		Enemy ene = listEnemy [curEnemy];
		
		ene.Attack ();
	}

    public void SetIsNotDead(bool _isNotDead)
    {
        isNotDead = _isNotDead;
    }  
}
