using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.VFX;

public class Movement : MonoBehaviour
{

    [Header("Controle")]
    private PlayerControls playerControls;
    
    
    [Header("Parametre du Player")]
    public float movespeed = 8f;
    public VisualEffect vfxRenderer;

    public Animator anim;
    
    
    [Header(("Physics et armes"))]
    public Rigidbody _rb;
    public Vector2 moveDirection;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;
    
    

    private Boolean canDash;
    private Boolean isDashing;
    private Boolean isAttacking;
    private Boolean isMoving;
    [SerializeField] public Boolean canMove;



    private float smoothtime = 0.1f;
    private float turnsmoothvelocity;


    private void Awake()
    {
        playerControls = new PlayerControls();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        canMove = true;
    }

    private void Update()
    {
        
        move();
        if (playerControls.Land.Attack.IsPressed()&&!isAttacking)
        {
            attack();
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove&&!isAttacking)
        {
            _rb.velocity = new Vector3(moveDirection.x * movespeed, 0f, moveDirection.y * movespeed);
            if (moveDirection != Vector2.zero)
            {
                float targetangle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref smoothtime,
                    turnsmoothvelocity);
                transform.rotation = Quaternion.Euler(0f, targetangle, 0f);
            }
        }
      
            
        
    }

    void move()
    {
        moveDirection = playerControls.Land.Move.ReadValue<Vector2>();
        if (moveDirection != Vector2.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        anim.SetBool("isMoving",isMoving);
        vfxRenderer.SetVector3("PlayerPosition",gameObject.transform.position);
    }

    void attack()
    {
        _rb.velocity =  Vector3.zero;
        isAttacking = true;
        anim.SetTrigger("Attack");
        Collider[] hitenemys = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        foreach (var enemy in hitenemys)
        {
            Debug.Log("We hit"+ enemy.gameObject.name);
            EnemyAIScript enemyAIScript = enemy.gameObject.GetComponent<EnemyAIScript>();
            enemyAIScript.Damage(1);
        }
        
        Invoke(nameof(Reset),0.2f);
    }

    private void Reset()
    {
        isAttacking = false;
    }
}
