using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum ActionsCurrent
    {
        Chasing,
        Shooting,
    }

    [Header("Components")]
    [SerializeField]        private         ActionsCurrent          actionsCurrent;
    [HideInInspector]       private         PlayerController        playerController;

    [Header("Atributtes Bot")]
    [SerializeField]        private         float                   speedChasingPlayer;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        actionsCurrent = ActionsCurrent.Chasing;
    }

    void Update()
    {
        ActionsController();
    }

    void ActionsController()
    {
        switch(actionsCurrent)
        {
            case ActionsCurrent.Chasing:

                float distancePlayer = Vector2.Distance(transform.position, playerController.transform.position);

                Vector2 direction = playerController.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedChasingPlayer * Time.deltaTime);

                if(distancePlayer > 10)
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerController.transform.position, speedChasingPlayer * Time.deltaTime);
                }

            break;

            case ActionsCurrent.Shooting:

            break;
        }
    }
}
