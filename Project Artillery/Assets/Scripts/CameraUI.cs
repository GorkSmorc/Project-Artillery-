using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CameraUI : MonoBehaviour
{
    public Camera Four, Five;
    public AudioSource voice;
    public AudioClip uglomer, collimator, navodchik;
    public Button Strt, Ok, Rand, Exit, ok2, cancel, Accept, Back;
    public Text time, znach, znachV, warn2, Score, endTimer, timerL;
    public InputField des, sot;
    public RawImage endTable;
    public RawImage error, good;
    public Sprite errorOff, errorAct, goodOff, goodAct;
    public Texture2D mainTex, background;
   public float timeout = 0, timer = 0, timerM = 0;
    private string x1s = "00", x2s = "00";
    private bool play = false;
    private int x1t, x2t;
    public static int butt;
        public int x, y;
    private GameObject temp,ex;
    public GameObject JoyStick;
    public GameObject[] trenoga;
    private void Awake()
    {
        butt = 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        Four.enabled = false;
        Five.enabled = false;
        endTable.enabled = false;
        //JoyStick.SetActive(false);
        trenoga = GameObject.FindGameObjectsWithTag("trenoga");
        for (int i = 0; i < trenoga.Length; i++)
            trenoga[i].GetComponent<MeshRenderer>().enabled = false;
        voice.volume = MainMenu.Vol;
        warn2.enabled = false;
        voice = this.GetComponent<AudioSource>();
        timer = 0;
        //Strt.gameObject.SetActive(false);
        Strt.onClick.AddListener(StrtButton);
        Ok.onClick.AddListener(OkButton);
        Rand.onClick.AddListener(RandButton);
        Exit.onClick.AddListener(ExitButton);
        ok2.onClick.AddListener(Ok2Button);
        cancel.onClick.AddListener(CancelButton);
        Accept.onClick.AddListener(AcceptButton);
        Back.onClick.AddListener(BackButton);
        time.text = timer.ToString("F2");
        temp = GameObject.FindGameObjectWithTag("SetCord") as GameObject;
        ex = GameObject.FindGameObjectWithTag("ex") as GameObject;
        temp.SetActive(false);
        ex.SetActive(false);
        des.text = "00";
        sot.text = "00";
        znach.gameObject.SetActive(false);
        znachV.gameObject.SetActive(false);
        Accept.gameObject.SetActive(false);
        Back.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        // scope.gameObject.SetActive(false);
        Time.timeScale = 1;
        JoyStick.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (timerM < 10)
        {
            if (timer < 10)
                time.text = "0" + timerM.ToString() + ":" + "0" + timer.ToString("F3");
            else
                time.text = "0" + timerM.ToString() + ":" + timer.ToString("F3");
        }
        else
        {
            if (timer < 10)
                time.text = timerM.ToString() + ":" + "0" + timer.ToString("F3");
            else
                time.text = timerM.ToString() + ":" + timer.ToString("F3");
        }
        if (Time.timeScale == 1 & butt < 0)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= 60) {
                timerM += 1;
                timer = 0;
                    }
        }
        if (timeout != 0)
        {
            if (timeout > 0)
                timeout -= Time.fixedDeltaTime;
            if (timeout < 0)
                timeout = 0;
        }
        switch(butt)
        {
            case 0:
            
                if (timeout == 0 & play == true)
                {
                    //Time.timeScale = 1;
                    //ready = true;
                    butt = 1;
                    play = false;
                    voice.Stop();
                    temp.SetActive(true);
                    Time.timeScale = 1;


                }
                
                break;

            case -1:

                znach.gameObject.SetActive(true);
                znachV.gameObject.SetActive(true);
                znachV.text = x1s + "." + x2s;
                Accept.gameObject.SetActive(true);
                Back.gameObject.SetActive(true);
                if (timeout == 0 & play == true)
                {
                    voice.clip = collimator;
                    voice.Play();
                    timeout = voice.clip.length;
                    play = false;
                    
                }
                    break;
            case -4:
                
                Exit.gameObject.SetActive(false);
                error.gameObject.SetActive(false);
                good.gameObject.SetActive(false);
                
               break;
            case -7:
                JoyStick.SetActive(true);
                Exit.gameObject.SetActive(true);
                error.gameObject.SetActive(true);
                good.gameObject.SetActive(true);
                break;
            case -5:
                JoyStick.SetActive(false);
                Exit.gameObject.SetActive(false);
                error.gameObject.SetActive(false);
                good.gameObject.SetActive(false);
                break;
            case -6:
                JoyStick.SetActive(false);
                error.gameObject.SetActive(false);
                good.gameObject.SetActive(false);
                Exit.gameObject.SetActive(true);
                EndTable();
                break;
        }
        if (x1s != "")

            if (int.Parse(x1s) >= 60)
            {
                x = int.Parse(x1s);
                x = x - 60;
                x1s = x.ToString();
                des.text = x1s;

            }
        

    }



    void StrtButton()
    {
        voice.clip = uglomer;
        voice.Play();
        timeout = voice.clip.length + 1;
        play = true;
        Strt.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    void OkButton()
    {
        if (des.text != "00")
        {
            x1s = des.text;

        }
        if (sot.text != "00")
        {
            x2s = sot.text;

        }
        if (x1s != "" & x2s != "")
        {
            if (int.Parse(x1s) >= 43 & int.Parse(x1s) <= 55)
            {
                warn2.enabled = true;
            }
            else
            {
                
                x = int.Parse(x1s);
                y = int.Parse(x2s);

               // ready = false;
                butt = -1;
                timeout = 2;
                play = true;
                temp.SetActive(false);
                Time.timeScale = 1;
                if (int.Parse(x1s) < 10 & int.Parse(x1s) > 0)
                    x1s = "0" + x1s;
                if (int.Parse(x2s) < 10 & int.Parse(x2s) > 0)
                    x2s = "0" + x2s;
                error.gameObject.SetActive(true);
                good.gameObject.SetActive(true);
            }

        }
        
    }
    void RandButton()
    {
        x1t = Random.Range(0, 59);
        x2t = Random.Range(0, 99);
        if (x1t >= 43 & x1t <= 55)
        {
            RandButton();
        }
        x1s = x1t.ToString();
        x2s = x2t.ToString();
        des.text = x1s;
        sot.text = x2s;
        
    }

    void ExitButton()
        {
        ex.SetActive(true);
        }
    void Ok2Button()
    {
        CameraUI.butt = 10;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    void CancelButton()
    {
        ex.SetActive(false);
    }
    void AcceptButton()
    {
        this.GetComponent<Control>().progress = 0;
        this.GetComponent<Control>().CamPosition = this.GetComponent<Control>().MainCamera.transform.position;
        switch (butt)
        {
            case -1:
                butt = -2;
                break;
            case -2:
                butt = -3;
                break;
        }
    }
    void BackButton()
    {
        this.GetComponent<Control>().progress = 0;
        this.GetComponent<Control>().CamPosition = this.GetComponent<Control>().MainCamera.transform.position;
        switch (butt)
        {
            case -2:
            butt = -1;
                break;
            case -3:
            butt = -2;
                break;
            case -7:
                JoyStick.SetActive(false);
                butt = -4;
                break;
        }
    }
    void EndTable()
    {

        timerL.enabled = false;
        time.enabled = false;
        znach.enabled = false;
        znachV.enabled = false;
        Back.gameObject.SetActive(false);
        Accept.gameObject.SetActive(false);
        Exit.gameObject.SetActive(true);
        endTable.enabled = true;
        endTimer.text = time.text;
        if (!Control.neud)
        {
            if (timerM == 0 & timer <= 45)
                Score.text = "ОТЛ";
            else if (timerM == 0 & timer > 45 & timer <= 55)
                Score.text = "ХОР";
            else if ((timerM == 0 & timer > 55) | (timerM == 1 & timer <= 10))
                Score.text = "УДОВ";
            else
                Score.text = "НЕУД";
        }
        else
            Score.text = "НЕУД";
        Time.timeScale = 0;
    }
    void OnGUI()
    {
        if (butt == -4 || butt == -5)
        {
          
            GUI.depth = 999;
            int hor = Screen.width;
            int ver = Screen.height;
            GUI.DrawTexture(new Rect((hor - ver) / 2, 0, ver, ver), mainTex);
            GUI.DrawTexture(new Rect((hor / 2) + (ver / 2), 0, hor / 2, ver), background);
            GUI.DrawTexture(new Rect(0, 0, (hor / 2) - (ver / 2), ver), background);
        }
    }
}
