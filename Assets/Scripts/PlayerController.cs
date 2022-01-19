using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]        private         CircleCollider2D    checkItens;
    [SerializeField]        private         GameObject          itemProximity;
    [HideInInspector]       private         Rigidbody2D         rb2;
    [HideInInspector]       private         Animator            animator;
    
    

    [Header("Atributtes Moviment")]
    [SerializeField]        private         float               speedMoviment;

    [Header("Atributtes Slots")]
    [SerializeField]        private         bool                handRightOcupped;

    [Header("Inputs")]
    [HideInInspector]       private         bool                keyDownE;
    [HideInInspector]       private         bool                keyDownG;
    [HideInInspector]       private         bool                mouse0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovimentController();
        PickupItemController();
        DropItemController();
        PushItemController();
        Inputs();
    }

    void MovimentController()
    {
        float axisHorizontal = Input.GetAxis("Horizontal");
        float axisVertical = Input.GetAxis("Vertical");

        rb2.velocity = new Vector2(axisHorizontal, axisVertical) * speedMoviment;

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedMoviment * Time.deltaTime);

        if(rb2.velocity.x != 0 || rb2.velocity.y != 0)
        {
            animator.SetBool("Walk", true);
        }else 
        {
            animator.SetBool("Walk", false);
        }
    }

    void PickupItemController()
    {
       float distanceItem = Vector2.Distance(transform.position, itemProximity.transform.position);

       if(distanceItem <= 2 && 
       keyDownE &&
       !handRightOcupped)
       {
           animator.SetBool("PickupItem", true);
           itemProximity.GetComponent<Item>().modeItem = Item.ModeItem.Equipped;
           handRightOcupped = true;
       }else 
       {
           animator.SetBool("PickupItem", false);
       }
    }

    void DropItemController()
    {
       if(keyDownG &&
       handRightOcupped)
       {
           itemProximity.GetComponent<Item>().modeItem = Item.ModeItem.Dropped;
           handRightOcupped = false;
       }
    }

    void PushItemController()
    {
        if(mouse0 &&
        handRightOcupped &&
        rb2.velocity.x == 0)
        {
            animator.SetBool("PushItem", true);
            itemProximity.GetComponent<Item>().modeItem = Item.ModeItem.Push;
            handRightOcupped = false;
        }else 
        {
            animator.SetBool("PushItem", false);
        }
    }

    void Inputs()
    {
        keyDownE = Input.GetKeyDown(KeyCode.E) ? keyDownE = true : keyDownE = false;
        keyDownG = Input.GetKeyDown(KeyCode.G) ? keyDownG = true : keyDownG = false;
        mouse0   = Input.GetKeyDown(KeyCode.Mouse0) ? mouse0 = true : mouse0 = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(checkItens == other.gameObject.CompareTag("Item") &&
        !handRightOcupped)
        {
            itemProximity = other.gameObject;
        }
    }
}
