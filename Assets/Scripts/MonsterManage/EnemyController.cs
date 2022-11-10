using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }  // State of guards, patrols, pursuits, death
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BasicCombatant))]
public class EnemyController : MonoBehaviour
{

    private EnemyStates enemyStates;
    private NavMeshAgent agent;    //Make sure the variable component must exist
    private BasicCombatant myComb;

    private Animator anim;


    [Header("Basic Setting")]

    public float sightRadius;
    public GameObject attackTarget;
    public bool isGuard;
    private float speed;    //One speed when chasing a player, another when going back on your own
    public float lookAtTime;
    private float AttackWaitTime;
    private float remainLookAtTime;

    [Header("Patrol State")]

    public float patrolRange;

    private Vector3 waypoint;

    private Vector3 guardPos;  //Get the coordinates of the first monster



    //Monster Injuries

    private float attackTimer;
    public float attackTime;
    public float HP = 15;

    //bool value with animation
    bool isWalk;
    bool isChase;
    bool isFollow;
    bool isHit;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        myComb = GetComponent<BasicCombatant>();

        speed = agent.speed;
        guardPos = transform.position;
        remainLookAtTime = lookAtTime;

    }

    private void Start()
    {
        attackTime = 2;
        attackTimer = attackTime;
        if (isGuard)
        {
            enemyStates = EnemyStates.GUARD;
        }
        else
        {
            enemyStates = EnemyStates.PATROL;
            GetNewWayPoint();//Give a point at the beginning
        }
    }
    private void Update()
    {
        Attacktimer();
        SwitchStates();
        SwitchAnimation();
    }

    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
        anim.SetBool("Hit", isHit); //bool values are associated together

    }
    void SwitchStates()
    {
        //If an enemy is found switch to chase
        if (FoundPlayer())
        {
            enemyStates = EnemyStates.CHASE;
            //Debug.Log("’“µΩplayers");
        }

        switch (enemyStates)
        {
            case EnemyStates.GUARD:
                break;
            case EnemyStates.PATROL:
                isChase = false;
                agent.speed = speed * 0.5f;  //Lower movement speed in patrol condition
                //The nav mesh agent helps us to control the distance at which the monster should stop and to determine whether it has reached that point.
                if (Vector3.Distance(waypoint, transform.position) <= agent.stoppingDistance)
                {
                    isWalk = false;
                    if (remainLookAtTime > 0)
                    {
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else
                    {
                        GetNewWayPoint();
                    }
                }
                else
                {
                    isWalk = true;
                    agent.destination = waypoint;
                }



                break;
            case EnemyStates.CHASE:

                //Matching animation
                isWalk = false;
                isChase = true;
                agent.speed = speed;
                if (!FoundPlayer())
                {
                    //Back to the previous state

                    isFollow = false;
                    if (remainLookAtTime > 0)
                    {
                        //After leaving the target, the chase animation is disabled however the animal will still move in idle state to the position of the character at the time of leaving the target.
                        agent.destination = transform.position;
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else if (isGuard)
                    {
                        enemyStates = EnemyStates.GUARD;
                    }
                    else
                    {
                        enemyStates = EnemyStates.PATROL;
                    }
                }
                else
                {
                    isFollow = true;
                    agent.SetDestination(attackTarget.transform.position);
                    if (attackTimer > 0) break;
                    if (Vector3.Distance(attackTarget.transform.position, transform.position) <= 3)
                    {
                        isHit = true;
                        var comb = attackTarget.GetComponent<BasicCombatant>();
                        if (comb != null)
                            CombatSystem.AddCombatAct(myComb, comb, new DamageDealer { RawValue = 5 }, "");
                        else
                        {
                            Debug.LogError("Missing Combatant at attackTarget");
                        }
                        attackTimer = attackTime;
                    }
                    else
                    {
                        isHit = false;
                    }
                }

                //Chasing the player and attacking

                break;
            case EnemyStates.DEAD:
                break;

        }
    }


    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);//Find all colliding bodies in the surrounding area with the enemy as the centre and the current radius.
        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                return true;
            }


        }
        attackTarget = null;
        return false;

    }


    void GetNewWayPoint()
    {
        remainLookAtTime = lookAtTime;  //Restoration time
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);

        Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y, guardPos.z + randomZ);    //Get new coordinates based on initial position ,terrain has potholes, y constant
                                                                                                                //Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);   //Get the new coordinates based on the current position

        // waypoint = randomPoint;  //Selecting a point where you can't walk is stuck in a bug
        NavMeshHit hit;
        waypoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : transform.position;  //When a point is acquired, it is determined whether it is movable or not, and if not, the current coordinates are kept and a new point is acquired.
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);  //Easy to adjust for enemy pursuit range.
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetMouseButton(0) && attackTimer <= 0)
        {
            attackTimer = attackTime;
            Debug.Log("kill");
            HP -= 5;

            if (HP == 0)
            {
                gameObject.SetActive(false);
            }

        }
    }
    void Attacktimer()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime * 2;
        }

    }
}