using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]        private         Transform[]         spawnBots;
    [SerializeField]        private         GameObject[]        bot;
    [SerializeField]        private         GameObject[]        scenarios;
    [HideInInspector]       private         CanvasController    canvasController;
    [HideInInspector]       private         PlayerController    playerController;

    [Header("Atributtes scene")]
    [SerializeField]        public          int                 waveCurrent;
    [SerializeField]        public          int                 countBots;

    void Awake()
    {
        scenarios[Random.Range(0, 2)].SetActive(true);
    }

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        canvasController = FindObjectOfType<CanvasController>();
        waveCurrent = 1;
        Time.timeScale = 1;
    }

    void Update()
    {
        ControllerBots();
        ControllerScene();
    }

    void ControllerBots()
    {
        if(countBots <= 0)
        {
            do
            {

                if(waveCurrent > 10)
                {
                    Instantiate(bot[Random.Range(0, 5)], spawnBots[Random.Range(0, 18)].transform.position, Quaternion.identity);
                }else 
                {
                    Instantiate(bot[0], spawnBots[Random.Range(0, 18)].transform.position, Quaternion.identity);
                }
                countBots += 1;
                canvasController.RefreshWave();
            }while(countBots < waveCurrent);

            if(waveCurrent < 15)
            {
                waveCurrent += 1;
            }
        }
    }



    public void ControllerScene()
    {
        if(playerController.death)
        {
            Time.timeScale = 0;
            canvasController.PanelGameOver();
        }
    }

}
