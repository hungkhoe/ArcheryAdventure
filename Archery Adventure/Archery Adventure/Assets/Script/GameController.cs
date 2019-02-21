using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	GameObject enemy;

	[SerializeField]
	Player player;

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
	}
	void Start () {

		sumEnemy = 10;
		curStairEnemy = 0;
		listEnemy = new List<Enemy> ();

		InitGame ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitGame(){
	
		//set first position player
		StairController.Instance.InitStairs();
		player.GetComponent<MovingController> ().InitMoving ();

		InitEnemy ();


		StartGame ();
	}

	public void StartGame(){
	

		SpawnEnemy ();

		//
		//player.GetComponent<MovingController> ().Move();
	}

	public void NextStep(){
		
		player.GetComponent<MovingController> ().Move ();
	}
	void InitEnemy(){
	
		for (int i = 0; i < sumEnemy; i++) {
		
			Enemy enemy = Instantiate (Resources.Load<GameObject>("Prefabs/Enemy/Tree"), StairController.Instance.transform).GetComponent<Enemy>();
			enemy.transform.position = new Vector2 (-100, -100);
			enemy.gameObject.SetActive (false);
			listEnemy.Add (enemy);
		}
	}

	void SpawnEnemy(){

		Vector2 posSpawn = StairController.Instance.ListTrackPoint [++curStairEnemy];

		Enemy ene = listEnemy [curEnemy];
		ene.gameObject.SetActive (true);
		ene.transform.position = new Vector2 (Static.MinX - 1f, posSpawn.y + 0.5f);
		ene.transform.DOMoveX (posSpawn.x, 1f, false);
		ene.GetComponent<MovingController> ().dir *= -1;
		ene.GetComponent<MovingController> ().curIdTrack = player.GetComponent<MovingController> ().curIdTrack+1;
	}
}
