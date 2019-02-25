using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    GameObject winningPanel, losingPanel;
    [SerializeField]
    Button winningButton, losingButton;

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
	
		GameController.Instance.StartGame ();
		transform.GetChild (0).gameObject.SetActive (false);
        InitUIController();
    }

    public void Update()
    {
        if(isLosing)
        {
            losingPanel.SetActive(true);
        }
        else if(isWinning)
        {
            losingPanel.SetActive(false);
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("HuyScene");
    }

    void LosingButtonFunction()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HuyScene");
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
            winningButton.onClick.AddListener(LosingButtonFunction);
        }
    }
}
