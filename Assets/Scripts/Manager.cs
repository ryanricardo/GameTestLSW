using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]        private         Transform[]         spawnBots;
    [SerializeField]        private         GameObject          bot;
    [HideInInspector]       private         CanvasController    canvasController;

    [Header("Atributtes scene")]
    [SerializeField]        public          int                 waveCurrent;
    [SerializeField]        public          int                 countBots;
    void Start()
    {
        canvasController = FindObjectOfType<CanvasController>();
        waveCurrent = 1;
    }

    void Update()
    {
        ControllerBots();
    }

    void ControllerBots()
    {
        if(countBots <= 0)
        {
            do
            {
                Instantiate(bot, spawnBots[Random.Range(0, 3)].transform.position, Quaternion.identity);
                countBots += 1;
                canvasController.RefreshWave();
            }while(countBots < waveCurrent);
            waveCurrent += 1;
        }
    }

}
