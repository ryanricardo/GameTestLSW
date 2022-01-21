using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]        private         Transform[]         spawnBots;
    [SerializeField]        private         GameObject          bot;

    [Header("Atributtes scene")]
    [SerializeField]        private         int                 waveCurrent;
    [SerializeField]        public          int                 countBots;
    void Start()
    {
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
            }while(countBots < waveCurrent);
            waveCurrent += 1;
        }
    }

}
