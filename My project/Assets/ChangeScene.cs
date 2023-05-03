using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (String.IsNullOrEmpty(PlayerPrefs.GetInt("COINS").ToString()))
        {
            PlayerPrefs.SetInt("COINS", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
