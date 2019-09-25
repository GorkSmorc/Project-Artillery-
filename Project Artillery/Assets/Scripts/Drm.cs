using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drm : MonoBehaviour
{
    private DateTime dateNow, dateDrm;
    private int DRM, DRMDisable;
    private string Pass = "Введите пароль";
    public GUIStyle DRMstyle, font;
    private string PassOk;
    // Start is called before the first frame update
    void Awake()
    {
        PassOk = "Б00250М2";
        dateDrm = new DateTime(2019, 10, 20);
        if (DRM != 0 & DRM != 1 & DRMDisable != 1)
        {
            PlayerPrefs.SetInt("DRM", 0);
            PlayerPrefs.SetInt("DRMDisable", 0);

        }
        DRM = PlayerPrefs.GetInt("DRM");
        DRMDisable = PlayerPrefs.GetInt("DRMDisable");
        if (DRM == 0)
            dateNow = DateTime.Now;
        if (dateNow >= dateDrm & DRMDisable != 1)
        {
            DRM = 1;
            PlayerPrefs.SetInt("DRM", 1);
        }
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (DRM == 1) {
            Time.timeScale = 0;
            GUI.Box(new Rect(new Vector2(0, 0), new Vector2(Screen.width, Screen.height)), "");
            Pass = GUI.TextField(new Rect(new Vector2(Screen.width / 2 - 100, Screen.height / 2), new Vector2(320, 30)), Pass, 20);
            GUI.Label(new Rect(new Vector2(Screen.width / 2-150, Screen.height / 2 - 50), new Vector2(300, 100)), "Введите пароль для разблокировки приложения: ", font);
            if (GUI.Button(new Rect(new Vector2(Screen.width / 2, Screen.height / 2 + 50), new Vector2(100, 30)), "OK"))
            {
                if(Pass == PassOk)
                {
                    DRMDisable = 1;
                    DRM = 0;
                    Time.timeScale = 1;
                    PlayerPrefs.SetInt("DRM", 0);
                    PlayerPrefs.SetInt("DRMDisable", 1);
                }
            }

        }
 
           

    }

}
