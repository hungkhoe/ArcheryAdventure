using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerMrGun2 : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject loseButton,winButton,resetLevelButton;
    [SerializeField]
    UnityEngine.UI.Text scoreText;
    static UIControllerMrGun2 instance;

    public static UIControllerMrGun2 Instance
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
    void Start()
    {
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame_OnClick()
    {        
        transform.GetChild(0).gameObject.SetActive(false);
        GameControllerMrGun2.Instance.StartGame();
        PlayerControllerMrGun2.Instance.SetStarGame();
    }

    void InitUI()
    {
        if (winButton != null)
        {
            winButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(WinButtonFunction);
        }    
        
        if(resetLevelButton != null)
        {
            resetLevelButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ResetLevel);
        }
        if (loseButton != null)
        {
            loseButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(LoseButtonFunction);
        }
    }

    void LoseButtonFunction()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }

    void WinButtonFunction()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MrGun2Scene");
    }

    public void SetLose()
    {
        if (loseButton != null)
        {
            loseButton.SetActive(true);
        }
    }

    public void SetWin()
    {
        if (winButton != null)
        {
            winButton.SetActive(true);
        }
    }

    public void ResetLevel()
    {
        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MrGun2Scene");
    }
}
 