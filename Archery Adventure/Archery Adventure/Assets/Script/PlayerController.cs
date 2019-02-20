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
    [SerializeField]
    GameObject[] dot_testArray;
    [SerializeField]
    float powerMeasurement, angleMeasurement;
    LineRenderer lineCheck;
    Text powerText,angleText;

    bool isTestMovement;
    Vector3 testingHeading; 
    float storePower;
    [SerializeField]
    int damage;
    bool isShooting = false;

    private void Awake()
    {        
        GameObject temp = GameObject.Find("power_measure_text");
        powerText = temp.GetComponent<Text>();
        GameObject temp2 = GameObject.Find("angle_measure_text");
        angleText = temp2.GetComponent<Text>();
        
    }
    void Start()
    {
        damage = GetComponent<Player>().GetDamage();
        powerMeasurement = 0;
        angleMeasurement = 0;
        lineCheck = gameObject.AddComponent<LineRenderer>();
        lineCheck.SetWidth(0.05f, 0.05f);
        lineCheck.SetVertexCount(2);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
        UI_Update();        
    }

    void PlayerControl()
    {
        if (Input.GetMouseButton(0))
        {
            isHolding = true;
            isDestroyingDotTest = true;
            AdjustFirePower();
        }
        else
        {
            if (isHolding == true)
            {
                isHolding = false;
                if (isDestroyingDotTest)
                {
                    ClearDotTest();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            CreateCircleDistance();
            Debug.Log("test");
        }        
    }

    void CreateCircleDistance()
    {
        for (int i = 0; i < dot_testArray.Length; i++)
        {
            dot_testArray[i] = Instantiate(prefab_DotTest);
            Vector3 convertPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            convertPositon.z = 15;
            dot_testArray[i].transform.position = convertPositon;
        }
    }

    void AdjustFirePower()
    {
        if (dot_testArray[1] != null)
        {
            lineCheck.enabled = true;
            Vector3 convertPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            convertPositon.z = 15;
            dot_testArray[1].transform.position = convertPositon;

            lineCheck.SetPosition(0, dot_testArray[0].transform.position);
            lineCheck.SetPosition(1, dot_testArray[1].transform.position);

            powerMeasurement = (Vector3.Distance(dot_testArray[0].transform.position, dot_testArray[1].transform.position)) * 30;
            Vector3 targerDirection = dot_testArray[1].transform.position - dot_testArray[0].transform.position;

            Vector3 targetDir = dot_testArray[1].transform.position - dot_testArray[0].transform.position;           

            angleMeasurement = 
                Quaternion.FromToRotation(dot_testArray[0].transform.right, dot_testArray[1].transform.position - dot_testArray[0].transform.position).eulerAngles.z - 180;

            if (powerMeasurement >= 100)
            {
                powerMeasurement = 100;
            }       
        }
    }    

    void ClearDotTest()
    {
        for (int i = 0; i < dot_testArray.Length; i++)
        {
            Destroy(dot_testArray[i]);
        }

        Vector3 pos1 = dot_testArray[0].transform.position;
        Vector3 pos2 = dot_testArray[1].transform.position;
        testingHeading = Vector3.Normalize(pos1 - pos2);      

        storePower = powerMeasurement;

        if(Vector3.Distance(dot_testArray[0].transform.position, dot_testArray[1].transform.position) > 1)
        {
            GameObject temp = Instantiate(prefab_bulletTest);
            Bullet prefabBullet = temp.GetComponent<Bullet>();
            prefabBullet.SetDamage(damage);
            prefabBullet.SetRotationAngle(angleMeasurement);
            prefabBullet.SetPowerDirection(powerMeasurement, testingHeading);
            temp.transform.position = this.transform.position;
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
}

