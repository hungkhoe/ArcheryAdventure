using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject winningPanel;
    [SerializeField]
    Button winningButton;
    [SerializeField]
    Enemy[] monsterInScene;
    [SerializeField]
    Player playerScript;

    bool isWinning,isLosing;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(10, 11);
        Physics2D.IgnoreLayerCollision(8, 10);
        GameObject temp = GameObject.Find("Player");
        playerScript = temp.GetComponent<Player>();
    }
    void Start () {
        monsterInScene = FindObjectsOfType<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isWinning)
        {
            winningPanel.SetActive(true);
        }
        else if (playerScript.GetHealth() <= 0 )
        {
            isLosing = true;           
        }
	}    

    public void SetWinning(bool _isWinning)
    {
        isWinning = _isWinning;
    }
    public void SetLosing(bool _isLosing)
    {
        isLosing = _isLosing;
    }
}
