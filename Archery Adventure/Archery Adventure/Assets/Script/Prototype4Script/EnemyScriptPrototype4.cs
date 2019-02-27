using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptPrototype4 : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject prefab_EnemyArrow;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShootArrow()
    {
        if (prefab_EnemyArrow != null)
        {
            GameObject temp = Instantiate(prefab_EnemyArrow);
            temp.transform.position = transform.position;
            temp.transform.parent = transform;
        }
    }
}
