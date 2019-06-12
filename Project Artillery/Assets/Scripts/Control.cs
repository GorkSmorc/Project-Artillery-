using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject krutilka, vizir;
    string ObjectName;
    public Vector3 CamPosition;
    public Quaternion CamRotate;
    Ray ray ;
    RaycastHit hit;
    public float sensitivity,speed;
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
        CamRotate = MainCamera.transform.rotation;
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                ObjectName = hit.collider.name;
                if (ObjectName != "")
                    if (krutilka.name == ObjectName & Input.GetMouseButton(0) & Time.timeScale != 0)
                    {
                        krutilka.transform.Rotate(-sensitivity * Input.GetAxis("Mouse Y"), 0, 0);
                        vizir.transform.Rotate(0, 0.05f * Input.GetAxis("Mouse Y"), 0);
                    }
            }

          
        }
        if (this.GetComponent<CameraUI>().butt == -1) ;

          //  MainCamera.transform.position = new Vector3(Mathf.Lerp(0,8.895f,speed), Mathf.Lerp(0,8.022f, speed), Mathf.Lerp(0, 2.539f, speed));

           // MainCamera.transform.rotation = new Quaternion(10.691f, 202.74f, 4.446f, 0);


    }
}
