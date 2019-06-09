using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject krutilka;
    string ObjectName;
    Ray ray ;
    RaycastHit hit;
    public float sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        sensitivity = 5;
        MainCamera = Camera.main;
        krutilka = GameObject.FindGameObjectWithTag("Krutilka");
        ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        ObjectName = "";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                ObjectName = hit.collider.name; 
            }

          
        }
        if(ObjectName != "")
        if (krutilka.name == ObjectName & Input.GetMouseButton(0) & Time.timeScale != 0)
        {
            krutilka.transform.Rotate(0, -sensitivity * Input.GetAxis("Mouse Y"), 0);

        }
    }
}
