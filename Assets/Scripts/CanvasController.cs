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
    [HideInInspector]       private         Manager                 mg;

    void Start()
    {
        mg = FindObjectOfType<Manager>();
    }

    void Update()
    {
        
    }

    public void ButtonPlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene(0);
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
