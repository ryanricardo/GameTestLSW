using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public enum ModeItem
    {
        Dropped,
        Equipped,
        Push,
    }

    public enum TypeItem
    {
        Pusher,
        Weapon,
    }

    public enum TypeWeapon
    {
        Pistol,
        Shotgun,
    }

    [Header("Components")]
    [SerializeField]        public          ModeItem            modeItem;
    [SerializeField]        public          TypeItem            typeItem;
    [SerializeField]        public          TypeWeapon          typeWeapon;
    [SerializeField]        private         Transform[]         handsPlayer;
    [HideInInspector]       private         Rigidbody2D         rb2;
    [HideInInspector]       private         PlayerController    playerController;

    [Header("Atributtes Object")]
    [SerializeField]        private         float               forcePush;
    [SerializeField]        public          float               localAmmunation;
    [HideInInspector]       private         bool                lauchPush;
    

    void Start()
    {
        
        rb2 = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
        
        if(typeItem == TypeItem.Weapon)
        {

            switch(typeWeapon)
            {
                case TypeWeapon.Pistol:
                    localAmmunation = Random.Range(5, 10);
                break;

                case TypeWeapon.Shotgun:
                    localAmmunation = Random.Range(4, 8);
                break;
            }
        }
    }

    void Update()
    {
        ManagerModeItem();
    }

    void ManagerModeItem()
    {

        switch(typeItem)
        {
            case TypeItem.Pusher:

                switch(modeItem)
                {
                    case ModeItem.Dropped:
                        GetComponent<BoxCollider2D>().isTrigger = true;
                    break;

                    case ModeItem.Equipped:
                        GetComponent<BoxCollider2D>().isTrigger = true;
                        transform.position = new Vector2(handsPlayer[1].transform.position.x, handsPlayer[1].transform.position.y);
                        lauchPush = true;
                    break;

                    case ModeItem.Push:
                        if(lauchPush)
                        {
                            GetComponent<BoxCollider2D>().isTrigger = false;
                            rb2.AddForce(playerController.transform.right * forcePush, ForceMode2D.Impulse);
                            StartCoroutine(StopVelocityItem());
                            lauchPush = false;
                        }
                    break;
                }

            break;

            case TypeItem.Weapon:
                switch(modeItem)
                {
                    case ModeItem.Dropped:
                        GetComponent<BoxCollider2D>().isTrigger = true;
                    break;

                    case ModeItem.Equipped:
                        playerController.ammmunationCurrent = localAmmunation;
                        gameObject.SetActive(false);
                        playerController.gunEquipped = gameObject;

                        switch(typeWeapon)
                        {
                            case TypeWeapon.Pistol:
                                playerController.categoryItens = PlayerController.CategoryItens.Pistol;
                            break;

                            case TypeWeapon.Shotgun:
                                playerController.categoryItens = PlayerController.CategoryItens.Shotgun;
                            break;
                        }
                    break;
                }
            break;
        }

    }

    public void EquipItem()
    {
        modeItem = ModeItem.Equipped;
    }

    IEnumerator StopVelocityItem()
    {
        do
        {
            yield return new WaitForSeconds(0.5f);
            rb2.velocity = Vector2.zero;
            modeItem = ModeItem.Dropped;
        }while(rb2.velocity.x != 0);

    }


}
