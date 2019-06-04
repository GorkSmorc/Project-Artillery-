using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class CameraGUI : MonoBehaviour
{

    public float timer = 0, x, y;
    public GUIStyle customStyleText, customStyleButton;
    private string x1s = "00", x2s = "00";
    private bool error, ready = false;
    private int butt, x1t, x2t;
    void Start()
    {
        Time.timeScale = 0;
        timer = 0;
        butt = 0;
    }

    void OnGUI()
    {


        GUI.Label(new Rect(new Vector2(1, 1), new Vector2(200, 40)), "Таймер = " + timer.ToString("F2"), customStyleText);
        if (butt == 0)
            if (GUI.Button(new Rect(new Vector2(Screen.width / 2 - 100, Screen.height - 60), new Vector2(200, 40)), "Начать", customStyleButton))
            {
                Time.timeScale = 1;
                ready = true;
                butt = 1;
            }
        if (ready)
        {
            GUI.Label(new Rect(new Vector2(Screen.width / 2 - 200, 1), new Vector2(200, 40)), "Внимание батарея! Единый угломер: ", customStyleText);
            x1s = GUI.TextField(new Rect(new Vector2(Screen.width / 2 + 150, 1), new Vector2(50, 30)), checkString(x1s), 2);
            x2s = GUI.TextField(new Rect(new Vector2(Screen.width / 2 + 200, 1), new Vector2(50, 30)), checkString(x2s), 2);
            if (butt == 1)
            {
                if (GUI.Button(new Rect(new Vector2(Screen.width / 2 - 200, Screen.height - 60), new Vector2(200, 40)), "ОК", customStyleButton))
                {
                    if (x1s != "" & x2s != "")
                    {
                        x = int.Parse(x1s);
                        y = int.Parse(x2s);
                        ready = false;
                        butt = -1;
                    }
                }
                if (GUI.Button(new Rect(new Vector2(Screen.width / 2, Screen.height - 60), new Vector2(200, 40)), "Случайно", customStyleButton))
                {
                    x1t = Random.Range(0, 59);
                    x2t = Random.Range(0, 99);
                    x1s = x1t.ToString();
                    x2s = x2t.ToString();

                }
            }
 
        }
        if (butt == -1)
        {
            GUI.Label(new Rect(new Vector2(1, 20), new Vector2(200, 40)), "Координаты: " + x1s + "," + x2s, customStyleText);

        }
    }
    void Update()
    {
        if (Time.timeScale == 1)
            timer += Time.fixedDeltaTime;

    }

    string checkString(string message)
    {
        {
            if (message.Length > 0)
            {
                char[] symbols = "1234567890".ToCharArray();
                char[] chars = message.ToLower().ToCharArray();
                foreach (char symbol in chars)
                {
                    if (!symbols.Contains(symbol))
                    {
                        message = message.Replace(symbol.ToString(), string.Empty);
                    }
                }
            }
            return message;
        }


    }
}
