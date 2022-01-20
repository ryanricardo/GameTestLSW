using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum ActionsCurrent
    {
        Chasing,
        Shooting,
        Dead,
        Nothing,
    }

    [Header("Components")]
    [SerializeField]        private         ActionsCurrent          actionsCurrent;
    [SerializeField]        private         GameObject              bullet;
    [SerializeField]        private         Transform               exitBullet;
    [SerializeField]        private         AudioSource             sourceEffects;
    [SerializeField]        private         AudioClip               clipShoot;
    [SerializeField]        private         Sprite                  spriteDead;
    [HideInInspector]       private         PlayerController        playerController;

    
    [Header("Atributtes Health")]
    [SerializeField]        public          float                   life;

    [Header("Atributtes Bot")]
    [SerializeField]        private         float                   speedChasingPlayer;
    [SerializeField]        private         float                   timeShooting;
    [SerializeField]        private         float                   distancePlayer;
    

    void Start()
    {
        life = 100;
        playerController = FindObjectOfType<PlayerController>();

        actionsCurrent = ActionsCurrent.Chasing;
    }

    void Update()
    {
        ActionsController();
        CheckLifePlayer();
        ControllerLife();
    }

    void ActionsController()
    {
        distancePlayer = Vector2.Distance(transform.position, playerController.transform.position);

        switch(actionsCurrent)
        {
            case ActionsCurrent.Chasing:

                LookingPlayer();

                if(distancePlayer > 10)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerController.transform.position, speedChasingPlayer * Time.deltaTime);
                }else 
                {
                    actionsCurrent = ActionsCurrent.Shooting;
                }

            break;

            case ActionsCurrent.Shooting:

                LookingPlayer();

                if(distancePlayer > 10)
                {
                    actionsCurrent = ActionsCurrent.Chasing;
                }

                if(timeShooting > 2)
                {
                    bullet.GetComponent<Bullet>().transformExit = exitBullet;
                    bullet.GetComponent<Bullet>().typePlayer = Bullet.TypePlayer.Bot;
                    Instantiate(bullet, exitBullet.transform.position, Quaternion.identity);
                    sourceEffects.PlayOneShot(clipShoot);
                    Debug.Log("Shoot");
                    timeShooting = 0;
                }else 
                {
                    timeShooting += Time.deltaTime;
                }
            break;

            case ActionsCurrent.Dead:
                GetComponent<SpriteRenderer>().sprite = spriteDead;
                GetComponent<BoxCollider2D>().isTrigger = true;
            break;

            case ActionsCurrent.Nothing:

            break;
        }
    }

    void ControllerLife()
    {
        if(life <= 0)
        {
            actionsCurrent = ActionsCurrent.Dead;
        }
    }

    void CheckLifePlayer()
    {
        if(playerController.death)
        {
            actionsCurrent = ActionsCurrent.Nothing;
        }
    }
    
    void LookingPlayer()
    {
        Vector2 direction = playerController.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedChasingPlayer * Time.deltaTime);
    }
}
