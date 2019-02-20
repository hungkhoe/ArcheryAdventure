using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    int health;
    Text health_Text;
    int damage;

    private void Awake()
    {
        damage = 1;
    }
    void Start () {
        GameObject temp = GameObject.Find("health_text");
        health_Text = temp.GetComponent<Text>();       
	}
	
	// Update is called once per frame
	void Update () {
        health_Text.text = health.ToString();
	}

    public void TakeDamage(int _damage)
    {
        health -= _damage;
    }
    public int GetDamage()
    {
        return damage;
    }
}
