using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum TypePlayer
    {
        Player,
        Bot,
    }

    [Header("Components")]
    [SerializeField]        public          Transform               transformExit;
    [SerializeField]        public          TypePlayer              typePlayer;
    [HideInInspector]       private         PlayerController        playerController;
    [HideInInspector]       private         Rigidbody2D             rb2;

    [Header("Atributtes Bullets")]
    [SerializeField]        private         float                   speedBulletPlayer;
    [SerializeField]        private         float                   speedBulletBot;



    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rb2 = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4);

        if(transformExit != null)
        {
            switch(typePlayer)
            {
                case TypePlayer.Player:
                    rb2.AddForce(playerController.direction * speedBulletPlayer, ForceMode2D.Impulse);
                break;

                case TypePlayer.Bot:
                    rb2.AddForce(transformExit.transform.right * speedBulletBot, ForceMode2D.Impulse);
                break;
            }
        }
    }

    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        

        if(other.gameObject.CompareTag("Player") && 
        typePlayer != TypePlayer.Player)
        {
            other.gameObject.GetComponent<PlayerController>().life -= 50;
            Destroy(gameObject, 0);
        }

        if(other.gameObject.CompareTag("Enemy") &&
        typePlayer == TypePlayer.Player)
        {
            other.gameObject.GetComponent<Enemy>().life -= 50;
            Debug.Log(other.gameObject.GetComponent<Enemy>().life);
            Debug.Log("Hit enemy");
            Destroy(gameObject, 0);
        }
        
    }
}
