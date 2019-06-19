using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollimatorRotate : MonoBehaviour
{
    public Camera Camera;
    private Vector3 CamTransform, TargetPosition;
    public float rotationSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
        CamTransform = Camera.gameObject.transform.position;
        TargetPosition = CamTransform - this.gameObject.transform.position;
        //TargetPosition.y = 0;
        //TargetPosition.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Quaternion.Angle(transform.localRotation, Quaternion.LookRotation(TargetPosition));
        this.gameObject.transform.localRotation = Quaternion.Slerp(this.gameObject.transform.localRotation, Quaternion.LookRotation(TargetPosition), Mathf.Min(1f, Time.deltaTime * rotationSpeed / angle));
    }
}
