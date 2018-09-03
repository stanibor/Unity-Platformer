using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    float baseSpeed = 5f;
    [SerializeField]
    float sprintingSpeed = 8f;
    [SerializeField]
    float rotationSpeed = 10f;

    [SerializeField]
    float maximumSlope = 0.1f;

    public float speedModifier;

    bool isSprinting = false;

    public Collider movementCollider { get; protected set; }
    public Rigidbody movementRigidbody { get; protected set; }
    public StateHolder stateHolder { get; protected set; }

    Vector3 mover;
    Vector3 rotator;
    

	// Use this for initialization
	void Start ()
    {
        speedModifier = 1f;

        mover = Vector3.zero;
        rotator = Vector3.zero;

        movementCollider = GetComponent<Collider>();
        movementRigidbody = GetComponent<Rigidbody>();
        stateHolder = GetComponent<StateHolder>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Sprint"))
            isSprinting = true;

        mover = Input.GetAxis("Vertical") * Vector3.forward + Input.GetAxis("Horizontal") * Vector3.right;
        mover = (mover.magnitude > 1f ? mover.normalized : mover);
        mover *= (isSprinting ? sprintingSpeed : baseSpeed) * speedModifier * Time.fixedDeltaTime;
        rotator = Input.GetAxis("Mouse X") * Vector3.up;
    }

    void FixedUpdate()
    {
        Rotate(rotator);

        

        if (IsGroundAt(Vector3.zero) && IsGroundAt(mover))
        {
            Move(mover);

            if(mover.magnitude <= 0f)
            {
                isSprinting = false;
                if (stateHolder != null)
                {
                    stateHolder.SetState("STOI");
                }
            }
            else
            {
                if (stateHolder != null)
                {
                    stateHolder.SetState(isSprinting ? "SPRINTUJE" : "CHODZI");
                }
            }

            
        }
            
        if (!IsGroundAt(Vector3.zero))
            Move(mover);
    }

    void Move(Vector3 move)
    {
        //move = (move.magnitude > 1f ? move.normalized : move) * baseSpeed * speedModifier * Time.fixedDeltaTime;
        transform.Translate(move);
    }

    void Rotate(Vector3 rotate)
    {
        rotate = (rotate.magnitude > 1f ? rotate.normalized : rotate) * rotationSpeed * Time.fixedDeltaTime;

        transform.Rotate(rotate);
    }


    public bool IsGroundAt(Vector3 offset)
    {
        Vector3 center = movementCollider.bounds.min;
        center.x = movementCollider.bounds.center.x;
        center.z = movementCollider.bounds.center.z;
        center += transform.rotation * offset;

        Collider[] hitColliders = Physics.OverlapSphere(center, maximumSlope);
        foreach (Collider ground in hitColliders)
        {
            if (ground != movementCollider)
                return true;
        }
        return false;
    }

    public bool IsGrounded()
    {
        return IsGroundAt(Vector3.zero);
    }

    void OnDrawGizmos()
    {
        if (movementCollider == null)
            return;
        Vector3 center = movementCollider.bounds.min;
        center.x = movementCollider.bounds.center.x;
        center.z = movementCollider.bounds.center.z;
        center += transform.rotation * mover;
        Gizmos.DrawWireSphere(center, maximumSlope);
    }
}
