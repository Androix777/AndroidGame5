using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room 
{
    [SerializeField] public RoomType type { get; set;}

    [SerializeField] public Vector2 position { get; set; }

    [SerializeField] public bool[] SideActiv { get; set; }

    [SerializeField] public TypePos posType { get; set; }

    
}
