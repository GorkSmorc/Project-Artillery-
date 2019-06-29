using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRand : MonoBehaviour
{
    private GameObject Mirror;
    private Vector3 TargetPosition;
    // private Ray ray;
    public float z, x, y;
    private RaycastHit hit;
    public Material Matrix;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        Matrix.color = Color.black;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float r;
        r = Matrix.color.r;
        if (r >= 130f)
            Matrix.color = new Color(130, 130, 130);

        if (Camera.main.gameObject.GetComponent<CameraUI>().butt == -5 & start == true)
        {
            Mirror = GameObject.FindGameObjectWithTag("Mirror");
            this.transform.localPosition = new Vector3(Random.Range(-5, 5), Random.Range(5, 7), Random.Range(-5, 0));
            TargetPosition = Mirror.transform.position - this.transform.position;
            Quaternion rawRoation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetPosition), 1000f * Time.deltaTime);
            transform.rotation = new Quaternion(0, rawRoation.y, 0, rawRoation.w);
            transform.LookAt(Mirror.transform);
            this.GetComponent<Light>().range = Vector3.Distance(this.transform.position, Mirror.transform.position) + 1f;
            z = Random.Range(1.7f, 10f);
            x = Random.Range(-0.02f, 0.024f);
            y = Random.Range(-0.015f, 0.007f);
            start = false;

        }




        int layerMask = 1 << 9;
        //layerMask = ~layerMask;
        if (Camera.main.gameObject.GetComponent<CameraUI>().butt == -5)
        {

            Ray ray = new Ray(transform.position, transform.TransformDirection(new Vector3(x, y, z)));

            
            if (Physics.Raycast(ray, out hit, Vector3.Distance(this.transform.position, GameObject.Find("Steklo").transform.position)+10f, layerMask))
            {
                string ObjectName;
                ObjectName = hit.collider.name;


                
          
                //Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);
                if (ObjectName == Mirror.name)
                {
                    Vector3 test = hit.point;
                    
                    float dist = Vector2.Distance(test, Mirror.transform.position);
                    Debug.Log(dist);
                    Matrix.color = new Color((0.03f / dist), (0.03f / dist), (0.03f / dist));
                    



                }
            }
        }
    }
}