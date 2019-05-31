using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformationMrGun2 : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    int level;

    static LevelInformationMrGun2 instance;

    public static LevelInformationMrGun2 Instance
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
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void SaveLevel()
    {
        level++;
        PlayerPrefs.SetInt("LevelProgress", level);
    }

    public int GetCurrentLevel()
    {
        return level;
    }


}
