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

    [Header("Components")]
    [SerializeField]        public          ModeItem        modeItem;
    [SerializeField]        private         Transform[]     handsPlayer;
    [HideInInspector]       private         Rigidbody2D     rb2;

    [Header("Atributtes Object")]
    [SerializeField]        private         float           forcePush;
    [HideInInspector]       private         bool            lauchPush;

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ManagerModeItem();
    }

    void ManagerModeItem()
    {
        switch(modeItem)
        {
            case ModeItem.Dropped:

            break;

            case ModeItem.Equipped:
                transform.position = new Vector2(handsPlayer[1].transform.position.x, handsPlayer[1].transform.position.y);
                lauchPush = true;
            break;

            case ModeItem.Push:

                if(lauchPush)
                {
                    rb2.AddForce(Vector2.right * forcePush, ForceMode2D.Impulse);
                    lauchPush = false;
                }
            break;
        }
    }


}
