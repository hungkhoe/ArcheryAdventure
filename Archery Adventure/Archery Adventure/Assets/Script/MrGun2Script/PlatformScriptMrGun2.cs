using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScriptMrGun2 : MonoBehaviour {

    // Use this for initialization
    int random, totalEnemy;
    float heightPlatform;
    [SerializeField]
    GameObject prefab_Enemy,prefab_Boss,prefab_MediumEnemy;
    [SerializeField]
    int level;

    private void Awake()
    {
        //InitEnemy();
        InitEnemyVersion2();
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void InitEnemy()
    {
        heightPlatform = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y;
        for (int i = 0; i < 4; i++)
        {
            random = Random.Range(0, 11);
            if (random > 4)
            {
                totalEnemy++;
                GameObject Ene = Instantiate(prefab_Enemy, transform.GetChild(i).transform);
                Ene.transform.position
                    = new Vector3(transform.GetChild(i).transform.position.x, transform.GetChild(i).transform.position.y + heightPlatform, transform.GetChild(i).transform.position.z);
            }
        }
        if (totalEnemy == 0)
        {
            totalEnemy++;
            GameObject Ene = Instantiate(prefab_Enemy, transform.GetChild(2).transform);
            Ene.transform.position
                = new Vector3(transform.GetChild(2).transform.position.x, transform.GetChild(2).transform.position.y + heightPlatform, transform.GetChild(2).transform.position.z);
        }
    }

    public int GetTotalEnemy()
    {
        return totalEnemy;
    }

    public void InitEnemyVersion2()
    {
        heightPlatform = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y;
        if (level == 0)
        {            
            totalEnemy = 2;
            GameObject Ene = Instantiate(prefab_Enemy, transform.GetChild(3).transform);
            Ene.transform.position
                = new Vector3(transform.GetChild(3).transform.position.x, transform.GetChild(3).transform.position.y + heightPlatform, transform.GetChild(3).transform.position.z);

            GameObject Ene1 = Instantiate(prefab_Enemy, transform.GetChild(0).transform);
            Ene1.transform.position
                = new Vector3(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y + heightPlatform, transform.GetChild(0).transform.position.z);
        }
        else if (level == 1)
        {
            totalEnemy = 2;
            GameObject Ene = Instantiate(prefab_Enemy, transform.GetChild(2).transform);
            Ene.transform.position
                = new Vector3(transform.GetChild(2).transform.position.x, transform.GetChild(2).transform.position.y + heightPlatform, transform.GetChild(2).transform.position.z);

            GameObject Ene1 = Instantiate(prefab_Enemy, transform.GetChild(1).transform);
            Ene1.transform.position
                = new Vector3(transform.GetChild(1).transform.position.x, transform.GetChild(1).transform.position.y + heightPlatform, transform.GetChild(1).transform.position.z);
        }
        else if (level == 2)
        {
            totalEnemy = 3;
            GameObject Ene = Instantiate(prefab_Enemy, transform.GetChild(3).transform);
            Ene.transform.position
                = new Vector3(transform.GetChild(3).transform.position.x, transform.GetChild(3).transform.position.y + heightPlatform, transform.GetChild(3).transform.position.z);

            GameObject Ene1 = Instantiate(prefab_Enemy, transform.GetChild(2).transform);
            Ene1.transform.position
                = new Vector3(transform.GetChild(2).transform.position.x, transform.GetChild(2).transform.position.y + heightPlatform, transform.GetChild(2).transform.position.z);

            GameObject Ene2 = Instantiate(prefab_MediumEnemy, transform.GetChild(0).transform);
            Ene2.transform.position
                = new Vector3(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y + heightPlatform, transform.GetChild(0).transform.position.z);
        }
        else if (level == 3)
        {
            totalEnemy = 2;         

            GameObject Ene1 = Instantiate(prefab_MediumEnemy, transform.GetChild(1).transform);
            Ene1.transform.position
                = new Vector3(transform.GetChild(1).transform.position.x, transform.GetChild(1).transform.position.y + heightPlatform, transform.GetChild(1).transform.position.z);

            GameObject Ene2 = Instantiate(prefab_MediumEnemy, transform.GetChild(2).transform);
            Ene2.transform.position
                = new Vector3(transform.GetChild(2).transform.position.x, transform.GetChild(2).transform.position.y + heightPlatform, transform.GetChild(2).transform.position.z);
        }
        else if (level == 4)
        {
            int position = Random.Range(0,4);
            totalEnemy = 1;
            heightPlatform = transform.GetChild(position).GetComponent<SpriteRenderer>().bounds.size.y;
            GameObject Ene = Instantiate(prefab_Boss, transform.GetChild(position).transform);
            Ene.transform.position
                = new Vector3(transform.GetChild(position).transform.position.x, transform.GetChild(position).transform.position.y + heightPlatform - 0.1f, transform.GetChild(position).transform.position.z);           
        }
    }
}
