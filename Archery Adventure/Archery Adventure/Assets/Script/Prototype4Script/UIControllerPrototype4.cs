using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerPrototype4 : MonoBehaviour
{
    // Use this for initialization
    [SerializeField]
    GameObject loseButton;
    [SerializeField]
    UnityEngine.UI.Text scoreText;
    static UIControllerPrototype4 instance;

    public static UIControllerPrototype4 Instance
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
        if(scoreText !=null)
        {
            scoreText.text = PlayerControllerProtoptype4.Instance.GetScore().ToString();
        }
    }

    public void StartGame_OnClick()
    {
        Debug.Log("Click");
        transform.GetChild(0).gameObject.SetActive(false);
        GameControllerPrototype4.Instance.StartGame();
        PlayerControllerProtoptype4.Instance.SetStarGame();
    }

    void InitUI()
    {
        if (loseButton != null)
        {
            loseButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(LoseButtonFunction);
        }
        GameObject temp = GameObject.Find("score_text");
        if(temp)
        {
            scoreText = temp.GetComponent<UnityEngine.UI.Text>();
        }
    }

    void LoseButtonFunction()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("NewMrGunScene");
    }

    public void SetLose()
    {
        if(loseButton != null)
        {
            loseButton.SetActive(true);
        }
    }
}
