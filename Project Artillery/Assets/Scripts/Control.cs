using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Control : MonoBehaviour
{
    public AudioSource sounds;
    public Camera MainCamera, SecondCamera, ThirdCamera;
    private GameObject krutilka, vizir, WayPoint;
    public Vector3 CamPosition, WayPointPos;
    public AudioClip errorS, goodS, strikeS;
    Ray ray;
    RaycastHit hit, targ;
    public float sensitivity, progress;
    public static float timer;
    public float step, stepKr, stepVz;
    public int fx, fy, error;
    private bool dovod, strike, good, navodchik = false;
    public static bool neud = false;
    // Start is called before the first frame update

    void Awake()
    {

        sounds.volume = MainMenu.Vol;
        sensitivity = 5;
        MainCamera = Camera.main;
        krutilka = GameObject.FindGameObjectWithTag("Krutilka");
        ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        vizir = GameObject.Find("Uglomer_des_Rotation");
        CamPosition = MainCamera.transform.position;
        step = 0.02f;
        WayPoint = GameObject.FindGameObjectWithTag("WayPoint");
        WayPointPos = WayPoint.transform.position;
        fx = 0;
        fy = 0;
        stepKr = 3.6f;
        stepVz = 6f;
        error = 0;
        timer = 0;
        ThirdCamera.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fy == 60)
            fy = 0;
        else if (fy == -60)
            fy = 0;



        if (fx == 100)
        {
            fx = 0;
            fy += 1;
        }
        else if (fx == -100)
        {
            fx = 0;
            fy -= 1;
        }
        int layerMask = 1 << 13;
        layerMask = ~layerMask;

       switch(CameraUI.butt)
        {
            case -2:

                WayPoint = GameObject.FindGameObjectWithTag("WayPoint");
                WayPointPos = WayPoint.transform.position;
                MainCamera.transform.position = Vector3.Lerp(CamPosition, WayPointPos, progress);
                if (progress < 1.1f)
                    progress += step;
                SecondCamera.enabled = true;
                break;
            case -1:
                WayPoint = GameObject.FindGameObjectWithTag("WayPoint2");
                WayPointPos = WayPoint.transform.position;
                MainCamera.transform.position = Vector3.Lerp(CamPosition, WayPointPos, progress);
                if (progress < 1.1f)
                    progress += step;
                SecondCamera.enabled = false;
                break;
            case -3:
                WayPoint = GameObject.FindGameObjectWithTag("WayPoint2");
                WayPointPos = WayPoint.transform.position;
                MainCamera.transform.position = Vector3.Lerp(CamPosition, WayPointPos, progress);
                if (progress < 1.1f)
                    progress += step;
                SecondCamera.enabled = false;
                if (fx < 0)
                    fx = 100 + fx;
                if (fy < 0)
                    fy = 60 + fy;
                if (fy != this.gameObject.GetComponent<CameraUI>().x || fx != this.gameObject.GetComponent<CameraUI>().y)
                {
                    error += 1;
                    if (error < 3)
                    {
                        this.GetComponent<CameraUI>().voice.clip = errorS;
                        this.GetComponent<CameraUI>().voice.Stop();
                        this.GetComponent<CameraUI>().voice.Play();
                    }
                    timer = 1.5f;


                    if (error != 3)
                    {
                        CameraUI.butt = -1;
                        strike = true;
                    }
                    this.gameObject.GetComponent<CameraUI>().error.texture = this.gameObject.GetComponent<CameraUI>().errorAct.texture;
                    timer = 1.5f;

                }
                else if (fy == this.gameObject.GetComponent<CameraUI>().x & fx == this.gameObject.GetComponent<CameraUI>().y & good == false)
                {
                    good = true;
                    this.gameObject.GetComponent<CameraUI>().good.texture = this.gameObject.GetComponent<CameraUI>().goodAct.texture;
                    timer = 1f;
                    this.GetComponent<CameraUI>().voice.clip = goodS;
                    this.GetComponent<CameraUI>().voice.Stop();
                    this.GetComponent<CameraUI>().voice.Play();
                }
                if (error == 3)
                {
                    neud = true;
                    if (navodchik == false)
                    {
                        this.GetComponent<CameraUI>().voice.Stop();
                        this.GetComponent<CameraUI>().voice.clip = this.gameObject.GetComponent<CameraUI>().navodchik;
                        this.GetComponent<CameraUI>().voice.Play();
                        timer = 4f;
                        navodchik = true;
                    }
                    WayPoint = GameObject.FindGameObjectWithTag("WayPoint3");
                    WayPointPos = WayPoint.transform.position;
                    MainCamera.transform.position = Vector3.Lerp(CamPosition, WayPointPos, progress);

                    if (progress < 1.1f)
                        progress += step;
                    dovod = true;
                }
                    break;
            
        }
        if (timer == 0 & (CameraUI.butt == -3 | CameraUI.butt == -1 | CameraUI.butt == -7))
        {

            this.gameObject.GetComponent<CameraUI>().error.texture = this.gameObject.GetComponent<CameraUI>().errorOff.texture;
            this.gameObject.GetComponent<CameraUI>().good.texture = this.gameObject.GetComponent<CameraUI>().goodOff.texture;
            if (strike)
            {
                GetComponent<CameraShake>().Shake(0.3f, 0.3f);
                strike = !strike;
                sounds.clip = strikeS;
                sounds.Stop();
                sounds.Play();
            }
            if (dovod == false & good == true & (CameraUI.butt == -3 | CameraUI.butt == -1 | CameraUI.butt == -2))
            {
                for (int i = 0; i < this.GetComponent<CameraUI>().trenoga.Length; i++)
                    this.GetComponent<CameraUI>().trenoga[i].GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("SpotLight").GetComponent<Light>().enabled = true;
                Camera.main.enabled = false;
                SecondCamera.enabled = false;
                CameraUI.butt = -4;
            }
        }
        if (dovod)
        {
            if ((fy == this.gameObject.GetComponent<CameraUI>().x & fx == this.gameObject.GetComponent<CameraUI>().y & good == false & dovod == true))
            {

                dovod = false;
                good = true;
                timer = 1f;

            }
            if (this.gameObject.GetComponent<CameraUI>().x > fy)
            {
                if (fy != this.gameObject.GetComponent<CameraUI>().x)
                {
                    fy += 1;
                    vizir.transform.Rotate(0, -stepVz, 0);
                }
            }

            else if ((this.gameObject.GetComponent<CameraUI>().x < fy))
                if (fy != this.gameObject.GetComponent<CameraUI>().x)
                {

                    fy -= 1;
                    vizir.transform.Rotate(0, stepVz, 0);



                }
            if (this.gameObject.GetComponent<CameraUI>().y > fx) { 
            if (fx != this.gameObject.GetComponent<CameraUI>().y)
            {

                fx += 1;
                krutilka.transform.Rotate(stepKr, 0f, 0f);
                vizir.transform.Rotate(0, 0.01f * -stepVz, 0);
            }
            }   

            else if ((this.gameObject.GetComponent<CameraUI>().y < fx))
            if (fx != this.gameObject.GetComponent<CameraUI>().y)
            {

                fx -= 1;
                krutilka.transform.Rotate(-stepKr, 0f, 0f);
                vizir.transform.Rotate(0, 0.01f * stepVz, 0);


            }

            }
        
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
            timer = 0;
    }

  
}

