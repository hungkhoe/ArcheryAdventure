using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyScript : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject prefab_EnemyArrow;    

    float timer = 2;
    float timeToShoot = 2;

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= timeToShoot)
        {
            timer = 0;
            ShootArrow();
        }
	}

    void ShootArrow()
    {
        if(prefab_EnemyArrow != null)
        {
            GameObject temp = Instantiate(prefab_EnemyArrow);            
            temp.transform.position = transform.position;
            temp.transform.parent = transform;
        }       
    }  

    protected void LateUpdate()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.y < 0 || screenPoint.x < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
