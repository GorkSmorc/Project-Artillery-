using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ControlVizir : MonoBehaviour, IDragHandler
{
    float stepVz;
    GameObject vizir;
    // Start is called before the first frame update
    void Start()
    {
        stepVz = 6f;
        vizir = GameObject.Find("Uglomer_des_Rotation");
    }
    public void OnDrag(PointerEventData eventData)
    {

        Vector2 delta = eventData.delta;
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            if (Time.timeScale != 0 & CameraUI.butt == -1)
                if (delta.x > 1)
                {

                    vizir.transform.Rotate(0, -stepVz, 0);
                    Camera.main.GetComponent<Control>().fy += 1;
                }
                else if (delta.x < -1)
                {
                    vizir.transform.Rotate(0, stepVz, 0);
                    Camera.main.GetComponent<Control>().fy -= 1;
                }

    }
}

