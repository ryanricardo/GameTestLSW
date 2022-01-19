using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Components")]
    [HideInInspector]       private         PlayerController        playerController;
    [HideInInspector]       private         Rigidbody2D             rb2;



    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rb2 = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4);
        Invoke("DesactiveTrigger", 0.1f);
    }

    void Update()
    {
        rb2.AddForce(playerController.transform.right * 10, ForceMode2D.Impulse);

        
    }

    void DesactiveTrigger()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
