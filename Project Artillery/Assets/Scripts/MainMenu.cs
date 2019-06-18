using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button Play, SettingsB, Quit, Back;
    public Slider Volume;
    public AudioSource MainSound;
   
    public int menupoint;
    // Start is called before the first frame update
    void Start()
    {
        menupoint = 0;
        Play.gameObject.SetActive(true);
        SettingsB.gameObject.SetActive(true);
        Quit.gameObject.SetActive(true);
        Back.gameObject.SetActive(false);
        Volume.gameObject.SetActive(false);
        Play.onClick.AddListener(PlayButton);
        SettingsB.onClick.AddListener(SettingsButton);
        Quit.onClick.AddListener(QuitButton);
        Back.onClick.AddListener(BackButton);
        Volume.onValueChanged.AddListener(VolumeSlider);
        MainSound.Play();
        MainSound.loop = true;


        MainSound.volume = 0.2f;
        Volume.value = MainSound.volume;

    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void PlayButton()
    {
        MainSound.Stop();
        GameObject.FindGameObjectWithTag("MainMenu").SetActive(false);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        menupoint = 1;


    }
    void SettingsButton()
    {
        Play.gameObject.SetActive(false);
        SettingsB.gameObject.SetActive(false);
        Quit.gameObject.SetActive(false);
        Back.gameObject.SetActive(true);
        Volume.gameObject.SetActive(true);

    }
    void QuitButton()
    {
        Application.Quit();
    }
    void BackButton()
    {
        Play.gameObject.SetActive(true);
        SettingsB.gameObject.SetActive(true);
        Quit.gameObject.SetActive(true);
        Back.gameObject.SetActive(false);
        Volume.gameObject.SetActive(false);
    }
    void VolumeSlider(float arg)
    {
        MainSound.volume = arg;
    }



}
