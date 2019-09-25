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
    private GameObject Steklo;
    public Quaternion startQ;
    private float stekloX;

    // Start is called before the first frame update
    void Start()
    {
        Steklo = GameObject.Find("CAP_OPEN_CLOSED");
        startQ = Steklo.transform.localRotation;
        stekloX = startQ.x;
    }

   void OnGUI()
    {
        if (CameraUI.butt == -5)
        {
            
            stekloX = GUI.VerticalSlider(new Rect(Screen.width - 50, Screen.height - 150, FiveCamera.scaledPixelWidth, Screen.height/10), stekloX, 0, 2f);
            Steklo.transform.localRotation = new Quaternion(stekloX, startQ.y, startQ.z, startQ.w);
            
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                this.transform.Rotate(0, 0, -delta.x);
    }
}