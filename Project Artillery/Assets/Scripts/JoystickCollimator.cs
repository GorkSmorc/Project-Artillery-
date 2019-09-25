using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JoystickCollimator : MonoBehaviour
{
    public Button Accept;
    public Camera cam,thirdCam;
    public float speed = 1, sensivity = 10f, rast;
    
    public FixedJoystick fixedJoystick;
    Vector3 quant, quantcam;
    GameObject go, Viz, main;
    RaycastHit hit;
    private float error = 0; 
    int layer;
    private void Awake()
    {
        quantcam = cam.transform.localRotation.eulerAngles;
        go = GameObject.Find("Msh");
        Viz = GameObject.Find("Viz");
        rast = Vector3.Distance(go.transform.position, cam.transform.position);
        layer = 1 << 12;
        Accept.onClick.AddListener(AcceptButton);
        //Back.onClick.AddListener(BackButton);
        main = Camera.main.gameObject;

    }

    void Update()
    {

        //if (CameraUI.butt == -5)
          //  transform.LookAt(thirdCam.transform.position);
        if (CameraUI.butt == -7) 
        {
            quant = transform.localRotation.eulerAngles;
            
            if (fixedJoystick.Vertical > 0 | fixedJoystick.Vertical < 0)
            {
                
                transform.localRotation = Quaternion.Euler(quant.x - fixedJoystick.Vertical * sensivity * Time.deltaTime, quant.y, quant.z);
                if (transform.localRotation.eulerAngles.x > 340 & transform.localRotation.eulerAngles.x < 350)
                    transform.localRotation = Quaternion.Euler(350, quant.y, quant.z);
                if(transform.localRotation.eulerAngles.x > 10 & transform.localRotation.eulerAngles.x < 340)
                    transform.localRotation = Quaternion.Euler(10, quant.y, quant.z);
                quant = transform.localRotation.eulerAngles;
            }
            if (fixedJoystick.Horizontal > 0 | fixedJoystick.Horizontal < 0)
            {
                transform.localRotation = Quaternion.Euler(quant.x, quant.y + fixedJoystick.Horizontal * sensivity * Time.deltaTime, quant.z);
                if (transform.localRotation.eulerAngles.y > 330 & transform.localRotation.eulerAngles.y < 340)
                    transform.localRotation = Quaternion.Euler(quant.x, 340, quant.z);
                if (transform.localRotation.eulerAngles.y > 20 & transform.localRotation.eulerAngles.y < 330)
                    transform.localRotation = Quaternion.Euler(quant.x,20, quant.z);
                quant = transform.localRotation.eulerAngles;
            }
            cam.transform.LookAt(go.transform.position * speed);
            
            
        }

    }
    void AcceptButton()
    {
        if (CameraUI.butt == -7)
        {
            Ray ray = new Ray(go.transform.position, go.transform.TransformDirection(new Vector3(0, 0, Vector3.Distance(go.transform.position, Viz.transform.position))));

            if (Physics.Raycast(ray, out hit, Vector3.Distance(go.transform.position, Viz.transform.position), layer))
            {
                if (hit.collider.name == Viz.name)
                {
                    main.GetComponent<CameraUI>().good.texture = main.GetComponent<CameraUI>().goodAct.texture;
                    main.GetComponent<CameraUI>().voice.clip = main.GetComponent<Control>().goodS;
                    main.GetComponent<CameraUI>().voice.Stop();
                    main.GetComponent<CameraUI>().voice.Play();
                    Control.timer = 1.5f;
                    CameraUI.butt = -5;
                }
            }
            else
            {
                error += 1;
                main.GetComponent<CameraUI>().error.texture = main.GetComponent<CameraUI>().errorAct.texture;
                main.GetComponent<CameraUI>().voice.clip = main.GetComponent<Control>().errorS;
                main.GetComponent<CameraUI>().voice.Stop();
                main.GetComponent<CameraUI>().voice.Play();
                Control.timer = 1.5f;
                if (error == 3)
                {
                    Control.neud = true;
                    CameraUI.butt = -6;
                }

            }

        }
    }

}
