using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float speed = 200f;

    Rigidbody rb;
    Animator anim;

    Vector3 gizmoPos;
    readonly float EPS = 0.01f;
    readonly float rightOffset = 0.5f;

	// Use this for initialization
	void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
    {
        float xFact = Input.GetAxis("Horizontal");
        float yFact = Input.GetAxis("Vertical");

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
	}

    private void LookAtCursor()
    {
        var pos = transform.position + transform.right * rightOffset;
        var plane = new Plane(Vector3.up, pos);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist;

        if (plane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            gizmoPos = targetPoint;

            Vector3 targetVector = targetPoint - pos;
            var targetRotation = Quaternion.LookRotation(targetVector);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(gizmoPos, Vector3.up * 100 + gizmoPos);
        Gizmos.DrawLine(transform.position, gizmoPos);
    }
}
