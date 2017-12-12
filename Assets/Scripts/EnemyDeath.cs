using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyDeath : MonoBehaviour
{
    public GameObject[] spawnables;

    GameObject lastAggressor;

    void OnDeath()
    {
        if (spawnables != null && spawnables.Length > 0)
        {
            int index = Random.Range(0, spawnables.Length);
            Instantiate(spawnables[index], transform.position + Vector3.up * 1f, Quaternion.identity);
        }

        GameManager.Instance.PlayerKills++;
        EventObserver.Instance.Notify(ObservableEvents.KillsUpdate, gameObject, GameManager.Instance.PlayerKills);

        Destroy(gameObject);
    }

    void HitBy(GameObject aggressor)
    {
        lastAggressor = aggressor;
    }
}
