using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ControlKrutilka : MonoBehaviour, IDragHandler
{
    GameObject vizir;
    float stepKr, stepVz;

    void Start()
    {
        vizir = GameObject.Find("Uglomer_des_Rotation");
        stepVz = 6f;
        stepKr = 3.6f;
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        
        Vector2 delta = eventData.delta;

        if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
            if (Time.timeScale != 0 & CameraUI.butt == -2)
                if (delta.y < -1)
                {


                transform.Rotate(stepKr, 0f, 0f);
                vizir.transform.Rotate(0, 0.01f * -stepVz, 0);
                    Camera.main.GetComponent<Control>().fx += 1;

            }
            else if (delta.y > 1)
              {
                transform.Rotate(-stepKr, 0f, 0f);
                vizir.transform.Rotate(0, 0.01f * stepVz, 0);
                    Camera.main.GetComponent<Control>().fx -= 1;

            }



    }


}
