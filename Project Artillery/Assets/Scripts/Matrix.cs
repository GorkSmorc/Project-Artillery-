using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    Camera Main;
    GameObject KostilStatic, KostilDynamic;
    public Material MatrixTexture;
    bool start = true, start2=true;
    float OffsetOrig, OffsetSet, OffsetDelta, x1,x2, timeout;
    // Start is called before the first frame update
    public Texture Button3, Good, Error;
    public GUIStyle Style;
    private Quaternion kost;
    int error = 0;
    void Start()
    {
        kost = GameObject.Find("K1").gameObject.transform.localRotation;
        Main = Camera.main;
        KostilStatic = GameObject.Find("KostilStatic");
        KostilDynamic = GameObject.Find("KostilDynamic");
        KostilDynamic.GetComponent<MeshRenderer>().enabled = false;
        KostilStatic.GetComponent<MeshRenderer>().enabled = false;
        MatrixTexture.mainTextureOffset = new Vector2(0.6860631f, 0.39f);

        OffsetOrig = MatrixTexture.mainTextureOffset.x;
        OffsetSet = OffsetOrig;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeout -= Time.fixedDeltaTime;
        if (timeout < 0)
            timeout = 0;
        if(CameraUI.butt == -5)
        {
            // Debug.Log(Vector3.Distance(KostilDynamic.transform.position, KostilStatic.transform.position));
            GameObject.Find("K1").gameObject.transform.localRotation = kost;
            OffsetSet = MatrixTexture.mainTextureOffset.x;
            if (OffsetOrig != OffsetSet)
            {
                Debug.Log("Darova1");
                OffsetDelta = OffsetSet - OffsetOrig;
                OffsetOrig = OffsetSet;
            }
            if (start)
            {
                Debug.Log("DarovaStart");
                x1 = KostilDynamic.transform.localPosition.x;
                start = false; 

            }
            if(start2)
            {
                KostilDynamic.transform.localPosition = new Vector3(x1 + 2.8152018566898752901077930140793f * 100.0239990f, KostilDynamic.transform.localPosition.y, KostilDynamic.transform.localPosition.z);
                MatrixTexture.mainTextureOffset = new Vector2(Random.Range(0.04f, 0.84f), MatrixTexture.mainTextureOffset.y);
                start2 = !start2;
            }
            if (OffsetDelta != 0 & KostilDynamic.transform.localPosition.x != 0)
            {

                KostilDynamic.transform.localPosition = new Vector3(x1 + 2.8152018566898752901077930140793f * OffsetDelta, KostilDynamic.transform.localPosition.y, KostilDynamic.transform.localPosition.z);
                x2 = KostilDynamic.transform.localPosition.x;
                OffsetDelta = 0;
            }
            if (x1 != x2)
            {
                Debug.Log("Darova2");
                x1 = x2;
            }
           
        }

    }
    private void OnGUI()
    {
        if (CameraUI.butt == -5)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 + 20,20, 100, 100), Good, ScaleMode.ScaleToFit);
            GUI.DrawTexture(new Rect(Screen.width / 2 - 100, 20, 100, 100), Error, ScaleMode.ScaleToFit);
            if (timeout == 0)
            {
                Good = Resources.Load("good") as Texture;
                Error = Resources.Load("error") as Texture;
                Main.GetComponent<Control>().sounds.Stop();
            }
            if (GUI.Button(new Rect(new Vector2(Screen.width / 2 - 50, Screen.height - 70), new Vector2(Button3.texelSize.x + 200, Button3.texelSize.y + 50)), "", Style))
            {
               
                if (Vector3.Distance(KostilDynamic.transform.position, KostilStatic.transform.position) <= 0.032)
                {

                    Good = Resources.Load("goodActive") as Texture;
                    Main.GetComponent<Control>().sounds.clip = Main.GetComponent<Control>().goodS;
                    Main.GetComponent<Control>().sounds.Play();
                    timeout = Main.GetComponent<Control>().goodS.length;
                    Main.enabled = false;
                    Main.enabled = true;
                    CameraUI.butt = -6;
                    
                    
                }
                else
                {
                    Error = Resources.Load("errorActive") as Texture;
                    error += 1;
                    if (error == 3)
                    {
                        Control.neud = true;
                        CameraUI.butt = -6;
                        
                    }
                    Main.GetComponent<Control>().sounds.clip = Main.GetComponent<Control>().errorS;
                    Main.GetComponent<Control>().sounds.Play();
                    timeout = Main.GetComponent<Control>().errorS.length;
                    //Debug.Log("NET");
                }
            }
        }
    }
}
