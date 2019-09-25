using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollimatorRotate : MonoBehaviour //IDragHandler
{

    public Camera FourCam, FiveCam, ScopeCam, ThirdCam;
    public GameObject trenoga, martixrotate;
    public Material matrix;
    private Vector3 CamTransform, TargetPosition, range;
    private Ray ray;
    private RaycastHit hit;
    private Quaternion ThisQuaterion;
    public Texture Button1, Button2, Button3, down, up;
    public GUIStyle Style, Style2;
    public float rotationSpeed = 10, sensivity = 0.1f, stepMtr = 0.5f , a;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        ScopeCam.enabled = false;
        // Camera.main.gameObject.GetComponent<CameraUI>().butt = -4;
        CamTransform = ThirdCam.gameObject.transform.position;
        TargetPosition = CamTransform - this.gameObject.transform.position;
        trenoga = GameObject.Find("trenoga");
        martixrotate = GameObject.Find("MATRIX_ROTATION");
        a = 0.035f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Quaternion rawRoation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetPosition), rotationSpeed * Time.deltaTime);
        //transform.rotation = new Quaternion(0, rawRoation.y, 0, rawRoation.w);
        //transform.LookAt(CamTransform);
        if (matrix.mainTextureOffset.x < 0.04f)
            matrix.mainTextureOffset = new Vector2(0.04f, matrix.mainTextureOffset.y);
        if (matrix.mainTextureOffset.x > 0.84f)
            matrix.mainTextureOffset = new Vector2(0.84f, matrix.mainTextureOffset.y);

    }
    void OnGUI()
    {
        switch (CameraUI.butt)
        {
            case -4:
                ThirdCam.enabled = true;
                if (GUI.RepeatButton(new Rect(new Vector2(Screen.width - 320, Screen.height - 70), new Vector2(Button1.texelSize.x + 150, Button1.texelSize.y + 50)), Button1, Style))
                    trenoga.transform.localPosition = new Vector3(trenoga.transform.localPosition.x - 0.01f, trenoga.transform.localPosition.y, trenoga.transform.localPosition.z);
                if (GUI.RepeatButton(new Rect(new Vector2(Screen.width - 160, Screen.height - 70), new Vector2(Button2.texelSize.x + 150, Button2.texelSize.y + 50)), Button2, Style))
                    trenoga.transform.localPosition = new Vector3(trenoga.transform.localPosition.x + 0.01f, trenoga.transform.localPosition.y, trenoga.transform.localPosition.z);
                if (GUI.Button(new Rect(new Vector2(Screen.width / 2 - 50, Screen.height - 70), new Vector2(Button3.texelSize.x + 200, Button3.texelSize.y + 50)), "", Style2))
                {
                    CameraUI.butt = -7;
                    
                }
                break;
            case -7:
                GameObject.Find("Вид_Cinema_4D").GetComponent<CameraUI>().JoyStick.SetActive(true);
                ScopeCam.enabled = true;
                ThirdCam.enabled = false;

                break;
            case -5:
                ScopeCam.enabled = false;
                ThirdCam.enabled = true;
                FourCam.Render();
                FiveCam.Render();
                bool RepBut1, RepBut2;
                RepBut1 = GUI.RepeatButton(new Rect(new Vector2(30, Screen.height - 90), new Vector2(down.texelSize.x + 100, down.texelSize.y + 100)),down, Style);
                RepBut2 = GUI.RepeatButton(new Rect(new Vector2(30, Screen.height - 190), new Vector2(up.texelSize.x + 100, up.texelSize.y + 100)), up, Style);
                if (RepBut2)
                {
                    martixrotate.transform.Rotate(0, stepMtr, 0);
                    matrix.mainTextureOffset = new Vector2(matrix.mainTextureOffset.x - Time.deltaTime * a, matrix.mainTextureOffset.y);
                   // a = Mathf.MoveTowards(a, 0.05f, Time.deltaTime * 0.01f);
                   
                }

                else if (RepBut1)
                {
                    martixrotate.transform.Rotate(0, -stepMtr, 0);
                    matrix.mainTextureOffset = new Vector2(matrix.mainTextureOffset.x + Time.deltaTime * a, matrix.mainTextureOffset.y);
                    //a = Mathf.MoveTowards(a, 0.05f, Time.deltaTime * 0.01f);
                }
                //else if (!RepBut1 & !RepBut2) { Debug.Log("1"); a = 0.01f; }
                break;
        }
    }


}
