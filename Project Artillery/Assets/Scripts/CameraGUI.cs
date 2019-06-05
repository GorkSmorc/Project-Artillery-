using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class CameraGUI : MonoBehaviour
{

    public AudioSource voice;
    public AudioClip uglomer, collimator;
    public float timeout = 0, timer = 0, x, y;
    public GUIStyle customStyleText, customStyleButton;
    private string x1s = "00", x2s = "00";
    private bool error,play = false, ready = false;
    private int butt, x1t, x2t;
    void Start()
    {

        voice = this.GetComponent<AudioSource>();
        Time.timeScale = 0;
        timer = 0;
        butt = 0;

    }

    void OnGUI()
    {


        GUI.Label(new Rect(new Vector2(1, 1), new Vector2(200, 40)), "Таймер = " + timer.ToString("F2"), customStyleText);
        if (butt == 0)
        {
            if (GUI.Button(new Rect(new Vector2(Screen.width / 2 - 100, Screen.height - 60), new Vector2(200, 40)), "Начать", customStyleButton))
            {
                voice.clip = uglomer;
                voice.Play();
                timeout = voice.clip.length + 2;
                play = true;


            }

            if (timeout == 0 & play == true)
            {
                Time.timeScale = 1;
                ready = true;
                butt = 1;
                play = false;
                voice.Stop();
            }

        }
        if (ready)
        {
            GUI.Label(new Rect(new Vector2(Screen.width / 2 - 200, 1), new Vector2(200, 40)), "Внимание батарея! Единый угломер: ", customStyleText);
            x1s = GUI.TextField(new Rect(new Vector2(Screen.width / 2 + 150, 1), new Vector2(50, 30)), checkString(x1s), 2);
            x2s = GUI.TextField(new Rect(new Vector2(Screen.width / 2 + 200, 1), new Vector2(50, 30)), checkString(x2s), 2);
            Time.timeScale = 0;

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
                        timeout = 2;
                        play = true;
                        Time.timeScale = 1;
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
            if (timeout == 0 & play == true)
            {
                voice.clip = collimator;
                voice.Play();
                timeout = voice.clip.length;
                play = false;
            }
            if (timeout == 0 & play == false)
                voice.Stop();
        }
    }
    void Update()
    {
        if (Time.timeScale == 1)
            timer += Time.fixedDeltaTime;
        if(timeout > 0)
            timeout -= Time.fixedDeltaTime;
        if (timeout < 0)
            timeout = 0;
        

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
            if(x1s != "")
            if (int.Parse(x1s) >= 60)
            {
                x = int.Parse(x1s);
                x = 60 - x;
                x1s = x.ToString();

            }
            return message;
        }


    }
}
