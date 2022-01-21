using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]        private         TextMeshProUGUI         textWave;
    [HideInInspector]       private         Manager                 mg;

    void Start()
    {
        mg = FindObjectOfType<Manager>();
    }

    void Update()
    {
        
    }

    public void RefreshWave()
    {
        textWave.text = "Wave: " + mg.waveCurrent;
    }
}
