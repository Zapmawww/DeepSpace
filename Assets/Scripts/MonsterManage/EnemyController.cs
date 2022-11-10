using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(BasicCombatant))]
public class EnemyController : MonoBehaviour
{
    private BasicCombatant myComb;

    private GameObject attackTarget = null;
    private float attackTimer;


    [Header("Basic Setting")]

    public float speed;    //Units per sec
    public float sightRadius;
    public float attackRadius;
    public float attackTime;

    private void Awake()
    {
        myComb = GetComponent<BasicCombatant>();
    }

    private void Start()
    {
        attackTimer = attackTime;
    }
    private void Update()
    {
        Attacktimer();
        SeekTarget();
        FollowTarget();
        AttackRange();
    }

    void AttackRange()
    {
        if (attackTarget == null) return;
        if (attackTimer > 0) return;
        if (Vector3.Distance(attackTarget.transform.position, transform.position) <= attackRadius)
        {
            var comb = attackTarget.GetComponent<BasicCombatant>();
            if (comb != null)
                CombatSystem.AddCombatAct(myComb, comb, new DamageDealer { RawValue = 5 }, "");
            else
            {
                Debug.LogError("Missing Combatant at attackTarget");
            }
            attackTimer = attackTime;
        }
    }

    void FollowTarget()
    {
        if (attackTarget == null) return;
        transform.position += (attackTarget.transform.position - transform.position).normalized * speed * Time.deltaTime;
    }

    void SeekTarget()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);//Find all colliding bodies in the surrounding area with the enemy as the centre and the current radius.
        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                return;
            }
        }
        attackTarget = null;
    }

    void Attacktimer()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime * 2;
        }

    }
}