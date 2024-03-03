using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] public GameObject potato;

    void Awake()
    {
        potato = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        enemy.SetDestination(potato.transform.position);
    }
}

