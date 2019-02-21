using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingController : MonoBehaviour {

	[SerializeField]
	float speedMoving;

	[SerializeField]
	float heightStair = 0.4f;

	Rigidbody2D rb;
	Vector2 forceMov;
	int dir;
	int curIdTrack;

	static MovingController instance;
	// Use this for initialization
	void Start () {

	
	}

	void Awake(){

		instance = this;
		rb = GetComponent<Rigidbody2D> ();
	}

	public static MovingController Instance{

		get{ 
		
			return instance;
		}
	}
		
	public void InitMoving(){
	
		transform.position = StairController.Instance.ListTrackPoint [0];
		if (StairController.Instance.ListTrackPoint [0].x > 0)
			dir = -1;
		else
			dir = 1;
		
		curIdTrack = 0;
	}

	public void Move(){

		forceMov = transform.right * dir * speedMoving;
		rb.velocity = forceMov;

	}

	IEnumerator Jump(int step, Vector2 endJump){

		float scalGra = rb.gravityScale;
		bool nextJump = true;
		rb.gravityScale = 0;

		for (int i = 0; i < step; i++) {

			yield return new WaitUntil (()=>nextJump);
			nextJump = false;

			if (i == step - 1) {
			
				transform.DOJump (new Vector3 (endJump.x, transform.position.y + heightStair, 0), 0.5f, 1, 0.2f, false).OnComplete(()=>{

					rb.gravityScale = scalGra;
					//turn around direction
					dir *= -1;

					StairController.Instance.MoveDownStair ();
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
//	IEnumerator TestMove(){
//
//		yield return new WaitForSeconds (2f);
//		Move ();
//	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.transform.tag.Equals ("PosToJump")) {

			int step = col.transform.parent.GetComponent<Stair> ().numStair;
			Vector2 endJump = StairController.Instance.ListTrackPoint[++curIdTrack];
			StartCoroutine (Jump(step, endJump));
		}
	}
}
