using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Jumping : MonoBehaviour
{
    [SerializeField]
    float jumpingPower = 3f;

    public float jumpingModifier;

    bool isJumping;

    Movement movement;

    // Use this for initialization
    void Start ()
    {
        jumpingModifier = 1f;

        movement = GetComponent<Movement>();
	}

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Jump"))
            isJumping = true;
    }

	
	void FixedUpdate ()
    {
        if (movement.IsGrounded() && isJumping)
        {
            Jump();
        }

        if (movement.stateHolder != null && !movement.IsGrounded())
            movement.stateHolder.SetState("SKACZE");
    }

    void Jump()
    {
        movement.movementRigidbody.velocity += (Vector3.up * jumpingPower) * jumpingModifier;
        isJumping = false;
    }
}
