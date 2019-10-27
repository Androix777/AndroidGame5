using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOnDeath : MonoBehaviour, IDeathEffect
{
    [SerializeField] private Shooter shooter;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ActivateEffect()
    {
        shooter.Shoot(transform.TransformPoint(Vector2.up) - transform.position, true);
    }
}
