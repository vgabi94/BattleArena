using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyDeath : MonoBehaviour
{
    public GameObject[] spawnables;

    void OnDeath()
    {
        if (spawnables != null && spawnables.Length > 0)
        {
            int index = Random.Range(0, spawnables.Length);
            Instantiate(spawnables[index], transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
