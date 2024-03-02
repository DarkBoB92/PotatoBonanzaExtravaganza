using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKeepDistance : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] public GameObject potato;
    [SerializeField] float radius;

    void Awake()
    {
        potato = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        enemy.SetDestination(potato.transform.position - (transform.forward * radius));
    }
}
