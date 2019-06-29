using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MirrorRotation : MonoBehaviour, IDragHandler
{
    public float sensivity = 3f;
    public Camera FiveCamera;
    private Ray ray;
    private RaycastHit hit;
    public GameObject Steklo;
    private Quaternion startQ;
    public float stekloX;

    // Start is called before the first frame update
    void Start()
    {
        Steklo = GameObject.Find("CAP_OPEN_CLOSED");
        startQ = Steklo.transform.localRotation;
        stekloX = startQ.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //int layerMaskMR = 1 << 8;
       
        ////if (Input.GetMouseButton(0) & Camera.main.gameObject.GetComponent<CameraUI>().butt == -5)
        ////{

        ////    ray = FiveCamera.ScreenPointToRay(Input.mousePosition);


        ////    if (Physics.Raycast(ray, out hit, Vector3.Distance(FiveCamera.transform.position, this.transform.position), layerMaskMR))
        ////    {
        ////        string ObjectName = hit.collider.name;
        ////        if (ObjectName != "") {
        ////            if (ObjectName == this.name)
        ////                this.transform.Rotate(0, 0, -Input.GetAxis("Mouse Y") * 1.5f);


        ////        }
        ////    }

        ////}
    }
   void OnGUI()
    {
        if (Camera.main.gameObject.GetComponent<CameraUI>().butt == -5)
        {
            stekloX = GUI.VerticalSlider(new Rect(Screen.width - 50, Screen.height - 150, 100, Screen.height/10), stekloX, 2f, 0);
            Steklo.transform.localRotation = new Quaternion(stekloX, startQ.y, startQ.z, startQ.w);
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
                this.transform.Rotate(0, 0, -delta.y);
    }
}