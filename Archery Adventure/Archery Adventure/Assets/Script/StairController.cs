using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StairController : MonoBehaviour
{

	[SerializeField]
	int defaultNumStair;
	bool isLeft;
	List<Stair> listStair;
	List<Vector2> listTrackPoint;
	static StairController instance;
	public const float HEIGHT_STAIR = 4f;
	// Use this for initialization
	void Start ()
	{

	}

	void Awake ()
	{

		instance = this;
	}

	public static StairController Instance {

		get { 
		
			return instance;
		}
	}

	public void InitStairs (int sumEnemy = 20)
	{

		isLeft = false;
		listStair = new List<Stair> ();
		listTrackPoint = new List<Vector2> ();

		for (int i = 0; i < 20; i++) {

			AddNewStair ();
		}

		UpdateLightStair ();
		listTrackPoint.Add (listStair [0].transform.GetChild (2).position);
		for (int i = 0; i < listStair.Count; i++) {
		
			listTrackPoint.Add (listStair [i].transform.GetChild (3).position);
		}
	}

	void AddNewStair ()
	{

		Stair stair = null;
		if (isLeft) {

			stair = Resources.Load<Stair> ("Prefabs/Stair/LeftStair/" + 4);
			//stair = Pooling.Instance.SpawnObject (4.ToString(), "Stair/LeftStair/").GetComponent<Stair>();
			isLeft = false;
		} else {
			stair = Resources.Load<Stair> ("Prefabs/Stair/RightStair/" + 4);
			//stair = Pooling.Instance.SpawnObject (4.ToString(), "Stair/RightStair/").GetComponent<Stair>();
			isLeft = true;

		}

		if (stair != null) {

			stair = Instantiate (stair.gameObject, transform).GetComponent<Stair> ();
			stair.idStair = listStair.Count;
			//stair.callBackHiddent = UpdateNewStair;
			int numStep = stair.GetComponent<Stair> ().numStair;
			float height = stair.GetComponent<SpriteRenderer> ().size.y;

			if (listStair.Count == 0) {

				stair.transform.position = new Vector3 (0, Static.MinY + height / 2, 0);
				listStair.Add (stair);
			} else {

				Stair lastStair = listStair [listStair.Count - 1];
				float hLastStair = lastStair.GetComponent<SpriteRenderer> ().size.y;
				int lastNumStep = lastStair.numStair;

				//float posY = (lastStair.transform.position.y + hLastStair / 2) + height / 2 - (height - stair.numStair * 0.4f);
				float posY = lastStair.transform.position.y + stair.numStair * 0.4f;
				stair.transform.position = new Vector3 (0, posY, 0);
				stair.GetComponent<SpriteRenderer> ().sortingOrder = lastStair.GetComponent<SpriteRenderer> ().sortingOrder - 1;
				//34
				listStair.Add (stair);
			}
		}
	}

	void UpdateNewStair ()
	{
		
		AddNewStair ();
		listTrackPoint.Add (listStair [listStair.Count - 1].transform.GetChild (3).position);
		//update color stair
		//255-89
	}

	void UpdateLightStair ()
	{
		
		//look fist stair visible
		int idLightestStair = 0;
		for (int i = 0; i < listStair.Count; i++) {

			Vector2 posStair = listStair [i].transform.position;
			if (posStair.y + HEIGHT_STAIR / 2 > Static.MinY) {
				idLightestStair = i;
				break;
			}
		}
			
		float disTopBottom = 0;
		float maxLight = 1f;
		float minLight = 0.34f;
		float subLight = 0;
		for (int i = idLightestStair; i < listStair.Count; i++) {

			Stair stair = listStair [i];

			disTopBottom = Static.MaxY - Static.MinY;
			if (stair.transform.position.y > Static.MinY) {
				float disPercent = (stair.transform.position.y - Static.MinY) / disTopBottom;
				subLight = (maxLight - minLight) * disPercent;
			} else
				subLight = 0;

			SpriteRenderer render = stair.GetComponent<SpriteRenderer> ();
			render.color = new Color (1 - subLight, 1 - subLight, 1 - subLight);

		}

	}

	public void MoveDownStair ()
	{
	
		float disBelowZeroPos = 1f;
		Vector2 curPlayer = PlayerController.Instance.gameObject.transform.position;
		float disMove = 0;
		if (curPlayer.y > 0) {
		
			disMove = curPlayer.y + disBelowZeroPos;
		} else if (curPlayer.y > -disBelowZeroPos) {
		
			disMove = disBelowZeroPos + curPlayer.y;
		}
			
		transform.DOMoveY (transform.position.y - disMove, 1f, false);
		UpdateLightStair ();

		listTrackPoint [0] = new Vector2 (listTrackPoint[0].x, listTrackPoint[0].y-disMove);
		for (int i = 0; i < listStair.Count; i++) {

			listTrackPoint [i] = new Vector2 (listTrackPoint [i].x,listTrackPoint[i].y-disMove );
		}
	}

	public List<Stair> ListStair {

		get { 
		
			return listStair;
		}
	}

	public List<Vector2> ListTrackPoint {

		get { 
		
			return listTrackPoint;
		}
	}
}
