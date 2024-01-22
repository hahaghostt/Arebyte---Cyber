using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{

    AudioSource futuristicclick;

    public void Start()
    {
        futuristicclick = GetComponent<AudioSource>();
    }

    public void NextScene()
    {
        futuristicclick.Play();
        SceneManager.LoadScene(1); 
    }

    public void NextScene2()
    {
        SceneManager.LoadScene(2);
    }
   
    

}
