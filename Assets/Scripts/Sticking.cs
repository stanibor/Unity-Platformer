using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticking : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        other.transform.parent = transform;
    }

    void OnCollisionExit(Collision other)
    {
        other.transform.parent = transform.parent;
    }
}
