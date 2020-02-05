using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHPUp : MonoBehaviour, IItem
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
