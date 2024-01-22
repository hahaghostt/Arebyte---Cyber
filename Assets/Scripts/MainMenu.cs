using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{

    public AudioSource futuristicclick;
    public AudioClip futuristic; 

    public void Start()
    {
        
    }

    public void NextScene()
    {
        futuristicclick.clip = futuristic; 
        futuristicclick.Play();
   
    }

    public void NextScene2()
    {
        SceneManager.LoadScene(2);
    }
   
    

}
