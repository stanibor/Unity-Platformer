using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    Transform originPoint;
    [SerializeField]
    Bullet projectile;
    [SerializeField]
    float cooldown = 0.5f;
    [SerializeField]
    float projectileInitialSpeed;

    float timer = 0f;
    bool isLoading = false;
    bool isShooting = false;

    StateHolder stateHolder;

	// Use this for initialization
	void Start ()
    {
        stateHolder = GetComponent<StateHolder>();

        timer = 0f;
        isLoading = false;
        isShooting = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isLoading)
        timer += Time.deltaTime;

        if(timer >= cooldown)
        {
            timer = 0f;
            isLoading = false;
        }

        isShooting = !isLoading && Input.GetButton("Fire");
    }

    void FixedUpdate ()
    {
        if (isShooting)
            Shoot();

        if((isShooting || isLoading) && stateHolder != null)
        {
            stateHolder.SetState("STRZELA");
        }
    }

    void Shoot()
    {
        Bullet shootedProjectile = Instantiate(projectile, originPoint.position, originPoint.rotation);
        shootedProjectile.SetSpeed(projectileInitialSpeed);
        isLoading = true;
    }
}
