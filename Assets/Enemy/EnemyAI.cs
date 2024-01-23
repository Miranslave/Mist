using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAIScript : MonoBehaviour
{
    [Header("IANav")]
    public NavMeshAgent agent;
    public Transform playertf;
    public LayerMask theGround, thePlayer;
    
    
    [Header("Patroling")]
    public Vector3 walkpoint;
    private bool walkPointSet;
    public float walkPointRange;
    public bool canbedamaged;
    public int health=3;
    public bool damaged;
    
    
    [Header("Attacking")]
    public float attackCd;
    private bool attacked;
    public int AttackDmg;
    public Transform attackpoint;
    
    [Header("Animation")]
    public Animator anim;
    public String speed;
    
    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    

    
    
    private void Awake()
    {
        speed = "Walking";
        anim = this.gameObject.GetComponent<Animator>();
        playertf = GameObject.FindWithTag("Player").transform;
        agent.GetComponent<NavMeshAgent>();
        
    }



    // Update is called once per frame
    void Update()
    {
        //Check for sight range
        if (!damaged)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, thePlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, thePlayer);
            if (!playerInAttackRange && !playerInSightRange) Patroling();
            if (!playerInAttackRange && playerInSightRange) Chasing();
            if (playerInAttackRange && playerInSightRange) Attacking();
            inFog();
        }
    }

    private void Patroling()
    {
        anim.SetBool(speed,true);
        if(!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkpoint);
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkpoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkpoint, -transform.up, 2f, theGround))
        {
            walkPointSet = true;
        }
    }
    private void Chasing()
    {
        anim.SetBool(speed,true);
        agent.SetDestination(playertf.position);
    }
    
    private void Attacking()
    {
        if (!attacked)
        {
            agent.SetDestination(transform.position);
            int a = 1;//Random.Range(0, 1);
            switch (a)
            {
                default:
                    break;

                case 0:
                    push();
                    break;

                case 1:
                    punch();
                    break;
            }
        }
    }

    // a bit bugged
    private void push()
    {
        attacked = true;
        Collider[] playerhit = Physics.OverlapSphere(attackpoint.position,attackRange,thePlayer);
        foreach (var check in playerhit)
        {
            Rigidbody playerRb =check.gameObject.GetComponent<Rigidbody>();
            // no push upward or we go to the sky lol 
            Vector3 pushdir = new Vector3(playerRb.transform.position.x-transform.position.x,0,playerRb.transform.position.z-transform.position.z);
            Debug.Log(pushdir);
            playerRb.AddForce(pushdir,ForceMode.Impulse);
            
        }
        agent.SetDestination(transform.position);
        Invoke(nameof(resetAttack),attackCd);
    }

    // work fine
    private void punch()
    {
        anim.SetTrigger("Attacking");
        attacked = true;
        Collider[] playerhit = Physics.OverlapSphere(attackpoint.position,attackRange,thePlayer);
        foreach (var check in playerhit)
        {
            check.gameObject.GetComponent<PlayerLight>().Damage(AttackDmg);
        }
        agent.SetDestination(transform.position);
        Invoke(nameof(resetAttack),attackCd);
    }
    public void Damage(int dmg)
    {
        if (canbedamaged)
        {
            anim.SetTrigger("Hitted");
            health = health - dmg;
            //if enemy low they want to kill you harder
            if (health == 1)
            {
                anim.SetBool(speed,false);
                speed = "Sprint";
                agent.speed = agent.speed * 2; 
            }
            if (health <= 0)
            {
                this.gameObject.SetActive(false);
            }

            damaged = true;
            Invoke(nameof(resetDamaged), 1f);
        }
    }

    public void inFog()
    {
        Vector3 distanceToPlayer = transform.position - playertf.position;
        if (distanceToPlayer.magnitude < 2f)
        {
            agent.speed = 1f; 
            canbedamaged = true;
        }
        else
        {
            canbedamaged = false;
        }
    }

    public void resetAttack()
    {
        attacked = false;
    }

    public void resetDamaged()
    {
        damaged = false;
    }
}
