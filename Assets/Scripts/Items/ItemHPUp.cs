using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUp : MonoBehaviour, IItem
{
    void Start()
    {
        gameObject.GetComponent<Life>().maxHP += 1;
        gameObject.GetComponent<Life>().HP += 1;
    }

    void Update()
    {
        
    }
}
