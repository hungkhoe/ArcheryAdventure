using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingController : MonoBehaviour {

	[SerializeField]
	float speedMoving;

	[SerializeField]
	float heightStair = 0.4f;

	[HideInInspector]
	public int dir;

	Rigidbody2D rb;
	Vector2 forceMov;

	[HideInInspector]
	public int curIdTrack;

	bool isMoving;

	//static MovingController instance;
	// Use this for initialization
	void Start () {

		dir = 1;
	}

	void Awake(){

		//instance = this;
		rb = GetComponent<Rigidbody2D> ();
	}

//	public static MovingController Instance{
//
//		get{ 
//		
//			return instance;
//		}
//	}
		
	public void InitMoving(){
	
		transform.position = StairController.Instance.ListTrackPoint [0];
		if (StairController.Instance.ListTrackPoint [0].x > 0)
			dir = -1;
		else
			dir = 1;
		
		curIdTrack = 0;

		if (dir > 0)
			transform.localScale= new Vector3(1f, transform.localScale.y, transform.localScale.z);
		else
			transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
	}

	public void Move(){

		forceMov = transform.right * dir * speedMoving;
		rb.velocity = forceMov;

		SetFlipX ();
		isMoving = true;
	}

	public void SetFlipX(){

		if (dir > 0)
			transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
		else
			transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
	}

	IEnumerator Jump(int step, Vector2 endJump){

		bool nextJump = true;
		rb.simulated = false;

		for (int i = 0; i < step; i++) {

			yield return new WaitUntil (()=>nextJump);
			nextJump = false;

			if (i == step - 1) {
			
				transform.DOJump (new Vector3 (endJump.x, transform.position.y + heightStair, 0), 0.5f, 1, 0.2f, false).OnComplete(()=>{

					rb.simulated = true;
					rb.velocity = new Vector2(0,rb.velocity.y);
					//turn around direction
					dir *= -1;
					SetFlipX();
					isMoving = false;
				});
				
			} else {
				
				transform.DOJump (new Vector3 (transform.position.x - heightStair*(-dir), transform.position.y + heightStair, 0), 0.5f, 1, 0.2f, false).OnComplete (() => {

					nextJump = true;
				});
			}
				
		}


		//StartCoroutine (TestMove());
	}

	//
	IEnumerator TestMove(){

		yield return new WaitForSeconds (2f);
		Move ();
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.transform.tag.Equals ("PosToJump")) {

			int step = col.transform.parent.GetComponent<Stair> ().numStair;
			Vector2 endJump = StairController.Instance.ListTrackPoint[++curIdTrack];
			StartCoroutine (Jump(step, endJump));
		}
	}
		
	public bool IsMoving{

		get{ 
		
			return isMoving;
		}
	}
}
