using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class CharacterController2D : MonoBehaviour
{


    public float runSpeed = 20.0f;

    private bool m_isMoving = false;
    private Vector2Int m_targetPosition = Vector2Int.zero;
    private Vector2 m_motionVector;
    private Rigidbody2D m_rigidBody2D;

    void Awake()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_motionVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        Move();  
    }

    private void Move()
    {
        m_rigidBody2D.velocity = m_motionVector * runSpeed;
    }
}