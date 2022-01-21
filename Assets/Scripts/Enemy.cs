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

    public enum TypeWeapon
    {
        Pistol,
        Shotgun,
    }

    [Header("Components")]
    [SerializeField]        private         ActionsCurrent          actionsCurrent;
    [SerializeField]        private         TypeWeapon              typeWeapon;
    [SerializeField]        private         Transform[]             exitBulletShotgun;
    [SerializeField]        private         GameObject              bullet;
    [SerializeField]        private         Transform               exitBullet;
    [SerializeField]        private         AudioSource             sourceEffects;
    [SerializeField]        private         AudioClip               clipShoot;
    [SerializeField]        private         Sprite                  spriteDead;
    [SerializeField]        private         GameObject              gunPistol;
    [SerializeField]        private         GameObject              gunShotgun;
    [HideInInspector]       private         Rigidbody2D             rb2;
    [HideInInspector]       private         PlayerController        playerController;
    [HideInInspector]       private         Manager                 mg;

    
    [Header("Atributtes Health")]
    [SerializeField]        public          float                   life;

    [Header("Atributtes Bot")]
    [SerializeField]        private         float                   speedChasingPlayer;
    [SerializeField]        private         float                   timeShooting;
    [SerializeField]        private         float                   distancePlayer;
    [HideInInspector]       private         bool                    dropWeapon;
    [HideInInspector]       private         bool                    touchPlayer;
    

    void Start()
    {
        mg = FindObjectOfType<Manager>();
        dropWeapon = true;
        life = 100;
        playerController = FindObjectOfType<PlayerController>();
        rb2 = GetComponent<Rigidbody2D>();
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

        if(touchPlayer)
        {
            rb2.velocity = Vector2.zero;
        }
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
                
                switch(typeWeapon)
                {
                    case TypeWeapon.Pistol:
                        if(timeShooting > 2)
                        {
                            bullet.GetComponent<Bullet>().transformExit = exitBullet;
                            bullet.GetComponent<Bullet>().typePlayer = Bullet.TypePlayer.Bot;
                            Instantiate(bullet, exitBullet.transform.position, Quaternion.identity);
                            sourceEffects.PlayOneShot(clipShoot);
                            timeShooting = 0;
                        }else 
                        {
                            timeShooting += Time.deltaTime;
                        }
                    break;

                    case TypeWeapon.Shotgun:
                        if(timeShooting > 2)
                        {
                            bullet.GetComponent<Bullet>().transformExit = exitBulletShotgun[0];
                            bullet.GetComponent<Bullet>().transformExit = exitBulletShotgun[1];
                            bullet.GetComponent<Bullet>().typePlayer = Bullet.TypePlayer.Bot;
                            Instantiate(bullet, exitBulletShotgun[0].transform.position, Quaternion.identity);
                            Instantiate(bullet, exitBulletShotgun[1].transform.position, Quaternion.identity);
                            sourceEffects.PlayOneShot(clipShoot);
                            timeShooting = 0;
                        }else 
                        {
                            timeShooting += Time.deltaTime;
                        }
                    break;
                }
                if(distancePlayer > 10)
                {
                    actionsCurrent = ActionsCurrent.Chasing;
                }


            break;

            case ActionsCurrent.Dead:
                GetComponent<SpriteRenderer>().sprite = spriteDead;
                GetComponent<BoxCollider2D>().isTrigger = true;
                GetComponent<BoxCollider2D>().enabled = false;

                if(dropWeapon)
                {
                    switch(typeWeapon)
                    {
                        case TypeWeapon.Pistol:
                            Instantiate(gunPistol, transform.position, Quaternion.identity);
                            dropWeapon = false;
                        break;

                        case TypeWeapon.Shotgun:
                            Instantiate(gunShotgun, transform.position, Quaternion.identity);
                            dropWeapon = false;
                        break;
                    }
                    Destroy(gameObject, 30);
                    mg.countBots -= 1;

                }

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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            touchPlayer = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            touchPlayer = false;
        }
    }
}
