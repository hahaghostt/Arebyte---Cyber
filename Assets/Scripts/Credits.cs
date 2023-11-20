using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update

    public void ConceptArt()
    {
        SceneManager.LoadScene(3); 
    }

    public void Menu()
    {
        SceneManager.LoadScene(0); 
    }
 
}
