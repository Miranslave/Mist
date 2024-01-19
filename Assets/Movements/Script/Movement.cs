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
    public GameObject attackZone;
    
    

    private Boolean canDash;
    private Boolean isDashing;
    private Boolean isAttacking;
    private Boolean isMoving;
    private Boolean canMove;
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
        Debug.Log(playerControls!=null);
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
        if (canMove)
        {
            moveDirection = playerControls.Land.Move.ReadValue<Vector2>();

        }

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

    // Update is called once per frame
    void FixedUpdate()
    {
            _rb.velocity = new Vector3(moveDirection.x * movespeed,0f,moveDirection.y * movespeed);
            if (moveDirection != Vector2.zero)
            {
                float targetangle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref smoothtime,
                    turnsmoothvelocity);
                transform.rotation = Quaternion.Euler(0f, targetangle, 0f);
            }
    }


}
