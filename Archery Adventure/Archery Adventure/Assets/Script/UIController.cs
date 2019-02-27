using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    GameObject winningPanel, losingPanel;
    [SerializeField]
    Button winningButton, losingButton,fireButton,spearButton,bowButton;

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
        GameManager.Instance.SetPlayerCanRun();
        PlayerController.Instance.SetCanShoot(true);
        PlayerController.Instance.SetGameStart();
    }

    private void Start()
    {
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
        if (spearButton != null)
        {
            spearButton.onClick.AddListener(SpearButtonFunction);
        }
        if (bowButton != null)
        {
            bowButton.onClick.AddListener(BowButtonFunction);
        }      
    }

    void FireButtonFunction()
    {
        PlayerController.Instance.FireBall();
    }

    void SpearButtonFunction()
    {
        PlayerController.Instance.UseSpearWeapon();
    }

    void BowButtonFunction()
    {
        PlayerController.Instance.UseBowWeapon();
    }
}
