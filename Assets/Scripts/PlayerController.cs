using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Components")]
    [HideInInspector]       private         Rigidbody2D         rb2;
    [HideInInspector]       private         Animator            animator;

    [Header("Atributtes Moviment")]
    [SerializeField]        private         float               speedMoviment;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovimentController();
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
}
