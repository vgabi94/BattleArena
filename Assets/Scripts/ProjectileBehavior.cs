using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{

    public string enemyTag = "Player";
    public float damagePoints = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            other.gameObject.GetComponent<Health>().ApplyDamage(damagePoints, other.ClosestPoint(transform.position), gameObject);
        }
    }
}
