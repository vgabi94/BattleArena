using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WeaponController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 250f;
    public float rotationSpeed = 10f;

    Rigidbody rb;
    Animator anim;
    WeaponController wc;
    Vector3 forward, right;
    readonly float EPS = 0.01f;

	// Use this for initialization
	void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        wc = GetComponent<WeaponController>();
	}
	
	// Update is called once per frame
	void Update()
    {
        float xFact = Input.GetAxis("Horizontal");
        float yFact = Input.GetAxis("Vertical");
        bool shoot = Input.GetButtonDown("Fire1");

        anim.SetBool("walk", false);
        if (Mathf.Abs(xFact) > EPS || Mathf.Abs(yFact) > EPS)
        {
            anim.SetBool("walk", true);
            DoMovement(xFact, yFact);
        }
        else
        {
            LookAtCursor();
        }

        if (shoot)
        {
            if (wc.MainWeapon != null)
                wc.MainWeapon.Use(gameObject);
        }
	}

    private void LookAtCursor()
    {
        var plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0f;
        if (plane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            rb.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                rotationSpeed * Time.deltaTime);
        }
    }

    private void AlignToDirection(Vector3 direction)
    {
        var targetRotation = Quaternion.LookRotation(direction);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 1);
    }

    private void DoMovement(float xFact, float yFact)
    {
        Vector3 newForward = Vector3.forward * yFact + Vector3.right * xFact;
        newForward.Normalize();
        AlignToDirection(newForward);

        rb.velocity = newForward * speed * Time.deltaTime;
        rb.angularVelocity = Vector3.zero;

        anim.SetFloat("speedFac", 3f);
    }
}
