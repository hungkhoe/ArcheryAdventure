using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScriptMrGun2 : MonoBehaviour {

    // Use this for initialization
    int random,totalEnemy;
    float heightPlatform;
    [SerializeField]
    GameObject prefab_Enemy;

    private void Awake()
    {
        InitEnemy();
    }
    void Start () {
  
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitEnemy()
    {
        heightPlatform = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y;
        for (int i = 0; i < 4; i++)
        {
            random = Random.Range(0, 11);
            if(random > 4)
            {
                totalEnemy++;
                GameObject Ene = Instantiate(prefab_Enemy, transform.GetChild(i).transform);
                Ene.transform.position
                    = new Vector3(transform.GetChild(i).transform.position.x, transform.GetChild(i).transform.position.y + heightPlatform, transform.GetChild(i).transform.position.z);
            }          
        }
        if(totalEnemy == 0)
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
}
