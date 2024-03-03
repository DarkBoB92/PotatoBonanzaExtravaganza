using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject psObject;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private int emissionRate;



    void Start()
    {
        psObject.SetActive(true);
        ps.emissionRate = emissionRate;
    }
}
