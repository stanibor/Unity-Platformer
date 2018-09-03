using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    Rigidbody bulletRigidbody;

    [SerializeField]
    float lifespan = 5f;

    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, lifespan);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    } 

    public void SetSpeed(float speed)
    {
        bulletRigidbody.velocity = transform.rotation * Vector3.forward * speed;
    }
}
