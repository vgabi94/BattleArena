using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void HitBy(GameObject aggressor)
    {
        ps.transform.rotation = Quaternion.LookRotation(-aggressor.transform.forward);
    }

    private void HitLocation(Vector3 hitLocation)
    {
        ps.transform.position = hitLocation;
        ps.Play();
    }
}
