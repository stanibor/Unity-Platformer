using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    float modifier = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        if (Apply(other.transform))
            AttachAndHide(other.transform);
    }

    void AttachAndHide(Transform other)
    {
        transform.parent = other;
        gameObject.SetActive(false);
    }

    bool Apply(Transform other)
    {
        Movement poweredMovement = other.GetComponent<Movement>();
        Jumping poweredJumping = other.GetComponent<Jumping>();

        if (poweredJumping != null)
        {
            poweredJumping.jumpingModifier = modifier;
        }

        if (poweredMovement != null)
        {
            poweredMovement.speedModifier = modifier;
            return true;
        }

        return false;
    }
}
