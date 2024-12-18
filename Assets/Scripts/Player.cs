﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;     
    private Rigidbody2D rb;           
    public Animator animator;
    public float rollBoost = 2f;
    private float rollTime;
    public float RollTime;
    bool rollOnce = false;
    public Vector3 moveInput;         

    private void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal"); 
        moveInput.y = Input.GetAxis("Vertical");  
        transform.position += moveInput * moveSpeed * Time.deltaTime; 

        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space) && rollTime <= 0)
        {
            animator.SetBool("Roll", true);
            moveSpeed += rollBoost; 
            rollTime = RollTime;     
            rollOnce = true;         
        }

        if (rollTime <= 0 && rollOnce == true)
        {
            animator.SetBool("Roll", false);
            moveSpeed -= rollBoost;  
            rollOnce = false;        
        }
        else
        {
            rollTime -= Time.deltaTime; 
        }

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
                transform.localScale = new Vector3(0.2f, 0.2f, 1f); 
            else
                transform.localScale = new Vector3(-0.2f, 0.2f, 1f);
        }
    }

    public PlayerHealth currentHealth;
    public void TakeDamageP(int damage)
    {
        currentHealth.TakeDamage(damage);
    }
}
