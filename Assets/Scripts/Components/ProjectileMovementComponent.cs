using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileMovementComponent : MonoBehaviour
{
    public float ProjectileSpeed = 800;

    protected GameObject instigator;
    public GameObject Instigator { get { return instigator; } set { instigator = value; } }

    Rigidbody2D projectileRB;
    Vector2 projectileDirection;

    private void Awake()
    {
        projectileRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        projectileRB.velocity = projectileDirection * Time.fixedDeltaTime;
    }

    public virtual void SetProjectileDirection(Vector2 newDirection) 
    {
        projectileDirection = newDirection * ProjectileSpeed;
    }
}
