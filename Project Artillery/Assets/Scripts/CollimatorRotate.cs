using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollimatorRotate : MonoBehaviour, IDragHandler
{
    public Camera MCamera, FourCam, FiveCam;
    public GameObject trenoga, martixrotate;
    public Material matrix;
    private Vector3 CamTransform, TargetPosition, range;
    public float stepMtr = 0.5f;
    private Ray ray;
    private RaycastHit hit;
    private Quaternion ThisQuaterion;
    public Texture Button1, Button2, Button3;
    public GUIStyle Style;
    public float rotationSpeed = 10, sensivity = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
      
      // Camera.main.gameObject.GetComponent<CameraUI>().butt = -4;
        CamTransform = MCamera.gameObject.transform.position;
        TargetPosition = CamTransform - this.gameObject.transform.position;
        trenoga = GameObject.Find("trenoga");
        martixrotate = GameObject.Find("MATRIX_ROTATION");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion rawRoation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetPosition), rotationSpeed * Time.deltaTime);
        transform.rotation = new Quaternion(0, rawRoation.y, 0, rawRoation.w);
        transform.LookAt(CamTransform);
        if (matrix.mainTextureOffset.x < 0.04f)
            matrix.mainTextureOffset = new Vector2(0.04f, matrix.mainTextureOffset.y);
        if (matrix.mainTextureOffset.x > 0.84f)
            matrix.mainTextureOffset = new Vector2(0.84f, matrix.mainTextureOffset.y);

    }
    void OnGUI()
    {
        if (Camera.main.gameObject.GetComponent<CameraUI>().butt == -4)
        {
            MCamera.enabled = true;
            if (GUI.RepeatButton(new Rect(new Vector2(Screen.width - 320, Screen.height - 70), new Vector2(Button1.texelSize.x + 150, Button1.texelSize.y + 50)), Button1, Style))
                trenoga.transform.localPosition = new Vector3(trenoga.transform.localPosition.x - 0.01f, trenoga.transform.localPosition.y, trenoga.transform.localPosition.z);
            if (GUI.RepeatButton(new Rect(new Vector2(Screen.width -160, Screen.height - 70), new Vector2(Button2.texelSize.x + 150, Button2.texelSize.y + 50)), Button2, Style))
                trenoga.transform.localPosition = new Vector3(trenoga.transform.localPosition.x + 0.01f, trenoga.transform.localPosition.y, trenoga.transform.localPosition.z);
            if (GUI.Button(new Rect(new Vector2(Screen.width / 2 - 50, Screen.height - 70), new Vector2(Button3.texelSize.x + 200, Button3.texelSize.y + 50)), Button3, Style))
            {
                Camera.main.gameObject.GetComponent<CameraUI>().butt = -5;
                GameObject.Find("Spot Light").GetComponent<LightRand>().start = true;
            }
        }
        if (Camera.main.gameObject.GetComponent<CameraUI>().butt == -5)
        {
            FourCam.Render();
            FiveCam.Render();
        }    
}

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        if(Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
        {
            if (delta.y > 0)
            {
                martixrotate.transform.Rotate(0, stepMtr, 0);
                matrix.mainTextureOffset = new Vector2(matrix.mainTextureOffset.x - 0.001f, matrix.mainTextureOffset.y);
            }
            else
            {
                martixrotate.transform.Rotate(0, -stepMtr, 0);
                matrix.mainTextureOffset = new Vector2(matrix.mainTextureOffset.x + 0.001f, matrix.mainTextureOffset.y);
            }
        }
    }
}
