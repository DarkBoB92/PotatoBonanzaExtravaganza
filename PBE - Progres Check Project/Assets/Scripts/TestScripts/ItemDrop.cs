using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    [SerializeField] public GameObject weaponPrefab;
    public Transform enemyPosition;
    [SerializeField] Vector3 currentPosition;

    private void Awake()
    {
        enemyPosition = gameObject.transform;
    }

    private void Update()
    {
        if(enemyPosition != null)
        {
            currentPosition = new Vector3(enemyPosition.position.x, enemyPosition.position.y + 2, enemyPosition.position.z);
        }
    }

    public void SpawnWeapon()
    {
        GameObject weaponToSpawn = Instantiate(weaponPrefab, currentPosition, Quaternion.identity);
    }
}
