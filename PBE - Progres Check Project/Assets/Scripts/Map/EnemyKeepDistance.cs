using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKeepDistance : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] public GameObject potato;
    public float radius, distance, speed;
    Vector3 calculatedDirection, appliedDirection;

    void Awake()
    {
        potato = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (potato != null)
        {
            distance = Vector3.Distance(transform.position, potato.transform.position);
            SetPosition();
            SetSpeed();
            FollowDirection();
        }
    }

    void SetPosition()
    {        
        enemy.SetDestination(potato.transform.position - (transform.forward * radius));
    }

    void FollowDirection()
    {
        speed = 2 * Time.deltaTime;
        calculatedDirection = potato.transform.position - transform.position;   // Calculate direction between GameObject vector positions
        appliedDirection = Vector3.RotateTowards(transform.forward, calculatedDirection, speed, 0f);   // Applying the direction into a usable Vector3

        transform.rotation = Quaternion.LookRotation(appliedDirection);   // Apply new calculated Vector3
    }

    void SetSpeed()
    {
        if (distance < radius)
        {
            enemy.speed = 100;
        }
        else
        {
            enemy.speed = 5;
        }
    }
}
