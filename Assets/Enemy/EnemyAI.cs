using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Script : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform playertf;

    public LayerMask theGround, thePlayer;
    //Patroling

    public Vector3 walkpoint;

    private bool walkPointSet;

    public float walkPointRange;
    
    //Attacking
    public float Attackcd;

    private bool attacked;
    //States
    public float sightRange, attackRange;

    public bool playerInSightRange, playerInAttackRange;
    // Start is called before the first frame update

    private void Awake()
    {
        playertf = GameObject.FindWithTag("Player").transform;
        agent.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check for sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange,thePlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, thePlayer);
        if(!playerInAttackRange && !playerInSightRange) Patroling();
        if(!playerInAttackRange && playerInSightRange)Chasing();
        if(playerInAttackRange && playerInSightRange)Attacking();
    }

    private void Patroling()
    {
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
        agent.SetDestination(playertf.position);
    }
    private void Attacking()
    {
        // aTtack
    }
}
