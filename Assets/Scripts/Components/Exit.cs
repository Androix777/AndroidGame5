using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Generator generator = gameObject.transform.parent.parent.gameObject.GetComponent<Generator>();
            generator.NextLevel();
        }
    }
}
