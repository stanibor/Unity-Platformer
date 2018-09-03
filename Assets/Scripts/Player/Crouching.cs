using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Crouching : MonoBehaviour
{
    [SerializeField]
    float heightReduction = 0.5f;

    Movement movement;
    bool isCrouching;

    void Start()
    {
        movement = GetComponent<Movement>();
    }

	// Update is called once per frame
	void Update ()
    {
        isCrouching = Input.GetButton("Crouch");
	}

    void FixedUpdate ()
    {
        if (isCrouching)
        {
            Crouch();
            if (movement.stateHolder != null)
                movement.stateHolder.SetState("KUCA");
        }
            
        else
            StandUp();
    }

    void Crouch()
    {
        transform.localScale = Vector3.one - heightReduction * Vector3.up;
    }

    void StandUp()
    {
        transform.localScale = Vector3.one;
    }
}
