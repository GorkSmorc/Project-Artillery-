using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class CameraUI : MonoBehaviour
{
    public AudioSource voice;
    public AudioClip uglomer, collimator, navodchik;
    public Button Strt, Ok, Rand, Exit, ok2, cancel, Accept, Back;
    public Text time, znach, znachV;
    public InputField des, sot;
    public RawImage error, good;
    public Sprite errorOff, errorAct, goodOff, goodAct;
    public Texture2D mainTex, background;
    private float timeout = 0, timer = 0;
    private string x1s = "00", x2s = "00";
    private bool play = false, ready = false;
    private int x1t, x2t;
    public int butt, x, y;
    private GameObject temp,ex;
    // Start is called before the first frame update
    void Start()
    {
        voice = this.GetComponent<AudioSource>();
        Time.timeScale = 0;
        timer = 0;
        butt = 0;
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
        des.text = "";
        sot.text = "";
        znach.gameObject.SetActive(false);
        znachV.gameObject.SetActive(false);
        Accept.gameObject.SetActive(false);
        Back.gameObject.SetActive(false);
       // scope.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        time.text = timer.ToString("F2");
        if (Time.timeScale == 1)
            timer += Time.fixedDeltaTime;
        if (timeout > 0)
            timeout -= Time.fixedDeltaTime;
        if (timeout < 0)
            timeout = 0;

        if (butt == 0)
        {

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
            temp.SetActive(true);
            if (des.text != "")
                x1s = des.text;
            if (sot.text != "")
                x2s = sot.text;
            Time.timeScale = 0;
        }
        if (butt == -1)
        {
            znach.gameObject.SetActive(true);
            znachV.gameObject.SetActive(true);
            znachV.text = x1s + "." + x2s;
            if (timeout == 0 & play == true)
            {
                voice.clip = collimator;
                voice.Play();
                timeout = voice.clip.length;
                play = false;
            }
           // if (timeout == 0 & play == false)
                //voice.Stop();

            Accept.gameObject.SetActive(true);
            Back.gameObject.SetActive(true);
        }

        if (x1s != "")

            if (int.Parse(x1s) >= 60)
            {
                x = int.Parse(x1s);
                x = x - 60;
                x1s = x.ToString();
                des.text = x1s;

            }
        if (butt == -4)
        {
            error.gameObject.SetActive(false);
            good.gameObject.SetActive(false);
        }

    }



    void StrtButton()
    {
        voice.clip = uglomer;
        voice.Play();
        timeout = voice.clip.length + 2;
        play = true;
        Strt.gameObject.SetActive(false);

    }

    void OkButton()
    {
        if (x1s != "" & x2s != "")
        {

            x = int.Parse(x1s);
            y = int.Parse(x2s);
            ready = false;
            butt = -1;
            timeout = 2;
            play = true;
            temp.SetActive(false);
            Time.timeScale = 1;
            if (int.Parse(x1s) < 10 & int.Parse(x1s) > 0)
                x1s = "0" + x1s;
            if (int.Parse(x2s) < 10)
                x2s = "0" + x2s;
            error.gameObject.SetActive(true);
            good.gameObject.SetActive(true);

        }
        
    }
    void RandButton()
    {
        x1t = Random.Range(0, 59);
        x2t = Random.Range(0, 99);
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
        if (butt == -1)
            butt = -2;
        else if (butt == -2)
            butt = -3;
            
    }
    void BackButton()
    {
        this.GetComponent<Control>().progress = 0;
        this.GetComponent<Control>().CamPosition = this.GetComponent<Control>().MainCamera.transform.position;
        if (butt == -2)
            butt = -1;
        else if (butt == -3)
            butt = -2;
        
    }
    void OnGUI()
    {
        if (butt == -4)
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
