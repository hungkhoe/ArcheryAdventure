using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    GameObject winningPanel, losingPanel;
    [SerializeField]
    Button winningButton, losingButton,fireButton;

    static UIController instance;
    bool isWinning, isLosing;
    public static UIController Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;

    }

    public void StartGame_OnClick(){	
	
		transform.GetChild (0).gameObject.SetActive (false);
        GameManager.Instance.SpawnEnemy();
        GameManager.Instance.SpawnEnemyZombie();
        InitUIController();
    }

    public void Update()
    {
        if(isLosing)
        {
            losingButton.gameObject.SetActive(true);
        }
        else if(isWinning)
        {
            winningButton.gameObject.SetActive(true);
        }
    }

    public void SetWinning(bool _isWinning)
    {
        PlayerController.Instance.SetWinning();
        isWinning = _isWinning;
    }

    public void SetLosing(bool _isLosing)
    {        
        isLosing = _isLosing;
    }

    void WinningButtonFunction()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ArcheryAdventureScene2");
    }

    void LosingButtonFunction()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ArcheryAdventureScene2");
    }

    void InitUIController()
    {
        PlayerController.Instance.SetGameStart();
        if (losingButton != null)
        {
            losingButton.onClick.AddListener(LosingButtonFunction);
        }
        if (winningButton != null)
        {
            winningButton.onClick.AddListener(WinningButtonFunction);
        }
        if(fireButton != null)
        {
            fireButton.onClick.AddListener(FireButtonFunction);
        }
        GameManager.Instance.SetPlayerCanRun();
    }

    void FireButtonFunction()
    {
        PlayerController.Instance.FireBall();
    }
}
