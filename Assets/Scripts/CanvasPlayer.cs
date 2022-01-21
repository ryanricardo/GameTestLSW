using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasPlayer : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]        private         TextMeshProUGUI         textAmmo;
    [HideInInspector]       private         PlayerController        playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        textAmmo.text = playerController.ammmunationCurrent.ToString();
    }

    public void DesactiveText()
    {
        textAmmo.gameObject.SetActive(false);
    }
}
