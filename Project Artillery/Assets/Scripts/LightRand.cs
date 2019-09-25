using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRand : MonoBehaviour
{
    private GameObject Mirror;
    private Vector3 TargetPosition;
    public GameObject matrix;
    public float z, x, y;
    private RaycastHit hit;
    public Material Matrix;
    public bool start;
    public Color oldColor;
    private void Awake()
    {
       oldColor = Matrix.color;
    }
    void Start()
    {
        start = true;
        Matrix.color = new Color (oldColor.r, oldColor.g, oldColor.b, 0f);
    }

 
    void FixedUpdate()
    {
        int layerMask = 1 << 9;
        float r;
        r = Matrix.color.r;
        
        if (CameraUI.butt == -5)
        {
            if (start == true)
            {
                
                Mirror = GameObject.Find("Steklo");
                this.transform.localPosition = new Vector3(Random.Range(-5, 5), Random.Range(5, 7), Random.Range(-5, 0));
                GameObject Steklo = GameObject.Find("MIRROR_ROTATION");
                Steklo.transform.localRotation = new Quaternion(Steklo.transform.localRotation.x,Steklo.transform.localRotation.y,Random.Range(-1.0f,1.0f), Steklo.transform.localRotation.w);
                TargetPosition = Mirror.transform.position - this.transform.position;
                Quaternion rawRoation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetPosition), 1000f * Time.deltaTime);
                transform.rotation = new Quaternion(0, rawRoation.y, 0, rawRoation.w);
                transform.LookAt(Mirror.transform.position);
                Steklo.transform.localRotation = new Quaternion(Steklo.transform.localRotation.x, Steklo.transform.localRotation.y, Random.Range(-1.0f, 1.0f), Steklo.transform.localRotation.w);
                this.GetComponentInChildren<Light>().range = Vector3.Distance(this.transform.position, Mirror.transform.position) + 1f;
                //z = Random.Range(1.5f, 10.2f);
                //x = Random.Range(-0.03f, 0.027f);
                //y = Random.Range(-0.017f, 0.01f);
                start = false;
            }
            Ray ray = new Ray(transform.position, TargetPosition);
            Debug.DrawRay(transform.position, TargetPosition, Color.yellow);
            if (Physics.Raycast(ray, out hit, Vector3.Distance(this.transform.position, GameObject.Find("Steklo").transform.position) + 10f, layerMask))
            {
                string ObjectName;
                ObjectName = hit.collider.name;
                if (ObjectName == Mirror.name)
                {
                    Vector3 test = hit.point;

                    float dist = Vector2.Distance(test, Mirror.transform.position);

                    Color color = new Color(oldColor.r, oldColor.g, oldColor.b, 0.005f / dist);
                    Matrix.color = color;
                }
            }
        }
 
    }
}