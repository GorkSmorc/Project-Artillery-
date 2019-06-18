using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public Camera MainCamera, SecondCamera;
    private GameObject krutilka, vizir, WayPoint;
    string ObjectName;
    public Vector3 CamPosition, WayPointPos;
    Ray ray ;
    RaycastHit hit;
    public float sensitivity, progress;
    private float step, stepKr, stepVz, timer;
    public int fx, fy,error;
    private bool good, navodchik = false;
    // Start is called before the first frame update
    void Start()
    {
       
        sensitivity = 5;
        MainCamera = Camera.main;
        krutilka = GameObject.FindGameObjectWithTag("Krutilka");
        ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        ObjectName = "";
        vizir = GameObject.FindGameObjectWithTag("Vizir");
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
  
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
        
        if (Input.GetMouseButton(0))
            { 
            ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                ObjectName = hit.collider.name;
                    if (ObjectName != "")
                    {
                        if (krutilka.name == ObjectName & Input.GetMouseButton(0) & Time.timeScale != 0 & this.GetComponent<CameraUI>().butt == -2)
                        {
                        if (Input.GetAxis("Mouse Y") < -0.01)
                        {


                            krutilka.transform.Rotate(stepKr, 0f, 0f);
                            vizir.transform.Rotate(0, 0.01f * -stepVz, 0);
                            fx += 1;
                        
                        }
                       else if (Input.GetAxis("Mouse Y") > 0.01)
                        {
                            krutilka.transform.Rotate(-stepKr, 0f, 0f);
                            vizir.transform.Rotate(0, 0.01f * stepVz, 0);
                           fx -= 1;
                        
                        }


                        }
                        if (vizir.name == ObjectName & Input.GetMouseButton(0) & Time.timeScale != 0 & this.GetComponent<CameraUI>().butt == -1)
                        {

                        if (Input.GetAxis("Mouse X") < -0.01)
                        {


                            vizir.transform.Rotate(0, stepVz, 0);
                            fy -= 1;

                        }
                        else if (Input.GetAxis("Mouse X") > 0.01)
                        {
   
                            vizir.transform.Rotate(0, -stepVz, 0);
                            fy += 1;

                        }

                    }
                    }
            }

          
        }
        if (this.GetComponent<CameraUI>().butt == -2)
        {

            WayPoint = GameObject.FindGameObjectWithTag("WayPoint");
            WayPointPos = WayPoint.transform.position;
            MainCamera.transform.position = Vector3.Lerp(CamPosition, WayPointPos, progress);
            if (progress < 1.1f)
                progress += step;
            SecondCamera.enabled = true;

        }
        if (this.GetComponent<CameraUI>().butt == -1)
        {
 
            WayPoint = GameObject.FindGameObjectWithTag("WayPoint2");
            WayPointPos = WayPoint.transform.position;
            MainCamera.transform.position = Vector3.Lerp(CamPosition, WayPointPos, progress);
            if (progress < 1.1f)
                progress += step;
            SecondCamera.enabled = false;
        }
       

        if (this.GetComponent<CameraUI>().butt == -3)
        {

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
                if(error != 3)
                this.GetComponent<CameraUI>().butt = -1;
                this.gameObject.GetComponent<CameraUI>().error.texture = this.gameObject.GetComponent<CameraUI>().errorAct.texture;
                timer = 1f;
                if (error == 3)
                    good = true;
            }
            else if (fy == this.gameObject.GetComponent<CameraUI>().x & fx == this.gameObject.GetComponent<CameraUI>().y & good == false)
            {
                good = true;
                this.gameObject.GetComponent<CameraUI>().good.texture = this.gameObject.GetComponent<CameraUI>().goodAct.texture;
                timer = 1f;
            }
            if (error == 3)
            {
               
                if (navodchik == false)
                {
                    this.gameObject.GetComponent<CameraUI>().voice.Stop();
                    this.gameObject.GetComponent<CameraUI>().voice.clip = this.gameObject.GetComponent<CameraUI>().navodchik;
                    this.gameObject.GetComponent<CameraUI>().voice.Play();
                    navodchik = true;
                }
                WayPoint = GameObject.FindGameObjectWithTag("WayPoint3");
                WayPointPos = WayPoint.transform.position;
                MainCamera.transform.position = Vector3.Lerp(CamPosition, WayPointPos, progress);
                if (progress < 1.1f)
                    progress += step;



                if (this.gameObject.GetComponent<CameraUI>().x > fy)
                {
                    while (fy != this.gameObject.GetComponent<CameraUI>().x)
                    {
                            fy += 1;
                            vizir.transform.Rotate(0, -stepVz, 0);
                    }
                  
                }
                else if ((this.gameObject.GetComponent<CameraUI>().x < fy)) { 
                    while (fy != this.gameObject.GetComponent<CameraUI>().x)
                    {
                 
                            fy -= 1;
                            vizir.transform.Rotate(0, stepVz, 0);
                            
                        

                    }
                    }
                if (this.gameObject.GetComponent<CameraUI>().y > fx)
                    while (fx != this.gameObject.GetComponent<CameraUI>().y)
                    {
                     
                            fx += 1;
                        krutilka.transform.Rotate(stepKr, 0f, 0f);
                        vizir.transform.Rotate(0, 0.01f * -stepVz, 0);
       
                        


                    }
                else if ((this.gameObject.GetComponent<CameraUI>().y < fx))
                    while (fx != this.gameObject.GetComponent<CameraUI>().y)
                    {
                  
                            fx -= 1;
                        krutilka.transform.Rotate(-stepKr, 0f, 0f);
                        vizir.transform.Rotate(0, 0.01f * stepVz, 0);
                       
                       
                    }



                //Debug.Log("Are you dolboeb?");
            }
           // Debug.Log("Выставленые значения:" + fy + "," + fx);
        }
        if (timer == 0)
        {
            this.gameObject.GetComponent<CameraUI>().error.texture = this.gameObject.GetComponent<CameraUI>().errorOff.texture;
            this.gameObject.GetComponent<CameraUI>().good.texture = this.gameObject.GetComponent<CameraUI>().goodOff.texture;
        }
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
            timer = 0;

    }
   void Update()
    {
       

    }
}
