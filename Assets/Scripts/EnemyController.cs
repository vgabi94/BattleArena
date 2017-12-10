using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    public float patrollingRange = 5f;
    public float viewRange = 10f;
    public float attackRange = 5f;
    public float waitScanning = 1f;

    public GameObject projectilePrefab;

    Animator anim;
    NavMeshAgent agent;

    enum State
    {
        Patroling, Attack, Chase, Scan, Wait, WaitAttack
    }

    State state;
    Vector3 targetDestination;
    GameObject targetObj;
    Transform daggerTransform;
    MeshRenderer mrDagger;

    bool waitFlag = true;
    bool waitAttackFlag = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        state = State.Patroling;
        targetObj = null;
    }

    private void Start()
    {
        var children = GetComponentsInChildren<Transform>();
        foreach (var item in children)
        {
            if (item.CompareTag("Offset"))
            {
                daggerTransform = item.transform;
                mrDagger = daggerTransform.gameObject.GetComponent<MeshRenderer>();
                mrDagger.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void Update()
    {
        if (targetObj != null)
            targetDestination = targetObj.transform.position;

        switch (state)
        {
            case State.Patroling:
                anim.SetBool("walk", true);
                RandomPoint(transform.position, patrollingRange, out targetDestination);
                state = State.Chase;
                break;
            case State.Attack:
                //agent.isStopped = true;
                anim.SetBool("attack", true);
                transform.LookAt(targetDestination);
                state = State.Scan;
                waitAttackFlag = true;
                break;
            case State.Chase:
                if (targetObj == null && Vector3.Distance(transform.position, targetDestination) < 0.01f)
                {
                    state = State.Wait;
                    //anim.SetBool("walk", false);
                    waitFlag = true;
                }
                else if (targetObj != null && Vector3.Distance(transform.position, targetDestination) < attackRange)
                {
                    state = State.Attack;
                    //anim.SetBool("walk", false);
                }
                else
                {
                    if (agent.isActiveAndEnabled)
                        agent.SetDestination(targetDestination);
                }
                break;
            case State.Wait:
                if (!waitFlag)
                    state = State.Scan;
                else
                    StartCoroutine(ScanCooldown());
                break;
            case State.WaitAttack:
                if (!waitAttackFlag)
                    state = State.Scan;
                break;
            case State.Scan:
                var collider = Physics.OverlapSphere(transform.position, viewRange);
                bool outOfRange = true;

                //if (targetObj == null)
                //{
                    foreach (var item in collider)
                    {
                        if (item.CompareTag("Player"))
                        {
                            targetObj = item.gameObject;
                            outOfRange = false;
                            anim.SetBool("walk", true);
                            state = State.Chase;
                            break;
                        }
                    }
                //}

                if (targetObj == null || (targetObj != null && outOfRange))
                {
                    state = State.Patroling;
                    targetObj = null;
                }
                else
                {
                    anim.SetBool("walk", true);
                    state = State.Chase;
                }
                break;
            default:
                break;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 rnd = Random.insideUnitSphere;
            Vector3 randomPoint = center +  rnd * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    IEnumerator ScanCooldown()
    {
        yield return new WaitForSeconds(waitScanning);
        waitFlag = false;
    }

    public void ResetEnemy()
    {
        state = State.Patroling;
        targetObj = null;
    }

    public void SpawnProjectile()
    {
        mrDagger.gameObject.SetActive(true);
    }

    public void ThrowProjectile()
    {
        var proj = Instantiate(projectilePrefab, daggerTransform.position, daggerTransform.rotation);
        var rb = proj.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 18f + Vector3.up * 4f, ForceMode.VelocityChange);
        rb.AddTorque(daggerTransform.up * 100f, ForceMode.VelocityChange);
        mrDagger.gameObject.SetActive(false);
        waitAttackFlag = false;
    }

    public void FinishThrowing()
    {
        anim.SetBool("attack", false);
    }
}
