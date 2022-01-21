using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMenu : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]        private         GameObject[]    objectsMenu;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ButtonPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void ButtomRules()
    {
        for(int i = 0; i < objectsMenu.Length - 1; i++)
        {
            objectsMenu[i].SetActive(false);
        }
        objectsMenu[5].SetActive(true);
    }

    public void ButtomBackMenu()
    {
        for(int i = 0; i < objectsMenu.Length - 1; i++)
        {
            objectsMenu[i].SetActive(true);
        }
        objectsMenu[5].SetActive(false);
    }
}
