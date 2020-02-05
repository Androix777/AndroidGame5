using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNumOfGuns : MonoBehaviour, IItem
{
    Shooter shooter;
    void Start()
    {

        shooter = gameObject.GetComponent<Shooter>();
        shooter.numOfGuns++;
        shooter.distanceBetweenGuns = 1f / shooter.numOfGuns;
    }

    void Update()
    {
        
    }
}
