using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]        private         TextMeshProUGUI         textWave;
    [SerializeField]        private         GameObject              panelGameOver;
    [SerializeField]        private         GameObject              panelMenuGame;
    [HideInInspector]       private         Manager                 mg;

    [Header("Atributtes Panels")]
    [HideInInspector]       private         bool                    openPanelMenuGame;

    void Start()
    {
        openPanelMenuGame = false;
        mg = FindObjectOfType<Manager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonMenuGame();
        }
        
    }

    public void ButtonPlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ButtonMenuGame()
    {
        openPanelMenuGame ^= true;
        if(openPanelMenuGame)
        {
            panelMenuGame.SetActive(openPanelMenuGame);
            Time.timeScale = 0;
        }else 
        {
            Time.timeScale = 1;
            panelMenuGame.SetActive(openPanelMenuGame);
        }
    }

    public void RefreshWave()
    {
        textWave.text = "Wave: " + mg.waveCurrent;
    }

    public void PanelGameOver()
    {
        panelGameOver.SetActive(true);
    }
}
