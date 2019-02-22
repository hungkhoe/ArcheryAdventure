using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Use this for initialization
    [SerializeField]
    bool isHolding, isDestroyingDotTest;

    [SerializeField]
    GameObject prefab_DotTest,prefab_bulletTest;

    //[SerializeField]
    GameObject[] dot_testArray;

    [SerializeField]
    float powerMeasurement, angleMeasurement;

	[SerializeField]
	GameObject bow;

	[SerializeField]
	int health;

    [SerializeField]
    bool isShooting = false;

	Text health_Text;
	int damage;
	LineRenderer lineCheck;
	Text powerText,angleText;
	bool isTestMovement;
	Vector3 testingHeading; 
	float storePower;
    [SerializeField]
	bool canShooting;
	static PlayerController instance;
	public static PlayerController Instance{

		get{

			return instance;
		}
	}
	void Awake(){

		instance = this;
	}

    void Start()
    {
		canShooting = true;
		CreateCircleDistance ();
        SetUpPlayerControl();
		//lineCheck.SetPosition ();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
//        UI_Update();        
    }


	public void TakeDamage(int _damage)
	{
		health -= _damage;
	}
	public int GetDamage()
	{
		return damage;
	}

	public int GetHealth()
	{
		return health;
	}

    void PlayerControl()
    {
		if (canShooting) {

			if (Input.GetMouseButtonDown (0)) {
			

				Vector3 startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				startPoint.z = 15;
				dot_testArray [0].transform.position = startPoint;
			}

			if (Input.GetMouseButton (0)) {
				isHolding = true;
				isDestroyingDotTest = true;
				AdjustFirePower ();

			} else {
				if (isHolding == true) {
					isHolding = false;
					if (isDestroyingDotTest) {
						ClearDotTest ();
					}
				}
			}
				
		}
    }

	public bool CanShooting{

		set{ 
		
			canShooting = value;
		}
	}

    void CreateCircleDistance()
    {
		if (dot_testArray == null) {
		
			dot_testArray = new GameObject[2];

			for (int i = 0; i < 2; i++) {
				dot_testArray [i] = Instantiate (prefab_DotTest);
				Vector3 convertPositon = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				convertPositon.z = 15;
				dot_testArray [i].transform.position = convertPositon;
			}
		}
   
    }

	void AdjustFirePower()
    {
        if (dot_testArray[1] != null)
        {
			dot_testArray[0].SetActive(true);
			dot_testArray[1].SetActive(true);

            lineCheck.enabled = true;
            Vector3 convertPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            convertPositon.z = 15;
            dot_testArray[1].transform.position = convertPositon;

			lineCheck.SetPosition(0, new Vector3(dot_testArray[0].transform.position.x, dot_testArray[0].transform.position.y , -1));
			lineCheck.SetPosition(1, new Vector3(dot_testArray[1].transform.position.x, dot_testArray[1].transform.position.y, -1));

            powerMeasurement = (Vector3.Distance(dot_testArray[0].transform.position, dot_testArray[1].transform.position)) * 30;
            Vector3 targerDirection = dot_testArray[1].transform.position - dot_testArray[0].transform.position;

            Vector3 targetDir = dot_testArray[1].transform.position - dot_testArray[0].transform.position;           

            angleMeasurement = 
                Quaternion.FromToRotation(dot_testArray[0].transform.right, dot_testArray[1].transform.position - dot_testArray[0].transform.position).eulerAngles.z - 180;

            if (powerMeasurement >= 100)
            {
                powerMeasurement = 100;
            }   

			float angle = 0;
			if(GetComponent<MovingController>().dir>0)
				angle = Mathf.Atan2(dot_testArray[1].transform.position.y-dot_testArray[0].transform.position.y, dot_testArray[1].transform.position.x-dot_testArray[0].transform.position.x)*180 / Mathf.PI-180;
			else angle = Mathf.Atan2(dot_testArray[1].transform.position.y-dot_testArray[0].transform.position.y, dot_testArray[1].transform.position.x-dot_testArray[0].transform.position.x)*180 / Mathf.PI;
			bow.transform.rotation = Quaternion.Euler (bow.transform.rotation.x, bow.transform.rotation.y, bow.transform.rotation.z + angle);
        }
    }    

    void ClearDotTest()
    {
        for (int i = 0; i < dot_testArray.Length; i++)
        {
			dot_testArray[i].SetActive(false);
        }

        Vector3 pos1 = dot_testArray[0].transform.position;
        Vector3 pos2 = dot_testArray[1].transform.position;
        testingHeading = Vector3.Normalize(pos1 - pos2);      

        storePower = powerMeasurement;

        if(Vector3.Distance(dot_testArray[0].transform.position, dot_testArray[1].transform.position) > 1)
        {
            GameObject temp = Instantiate(prefab_bulletTest);
			PlayerBullet prefabBullet = temp.GetComponent<PlayerBullet>();
            prefabBullet.SetDamage(damage);
            prefabBullet.SetRotationAngle(angleMeasurement);
            prefabBullet.SetPowerDirection(powerMeasurement, testingHeading);
			temp.transform.position = bow.transform.position;
			canShooting = false;
        }    

        lineCheck.enabled = false;
        powerMeasurement = 0;
        angleMeasurement = 0;
    }

    void UI_Update()
    {
        powerText.text = powerMeasurement.ToString("F0");
        angleText.text = angleMeasurement.ToString("F0") +  "°";
    }

    void SetUpPlayerControl()
    {
        damage = GetDamage();
        powerMeasurement = 0;
        angleMeasurement = 0;
        lineCheck = gameObject.AddComponent<LineRenderer>();
        lineCheck.SetWidth(0.05f, 0.05f);
        lineCheck.SetVertexCount(2);
    }

	public int DirectPlayer{

		get{ 

			return GetComponent<MovingController> ().dir;
		}
	}
}

