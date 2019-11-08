using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] RoomView Room;
    [SerializeField] Side side;
    [SerializeField] Vector2 SpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Hero")
        {
            if (Room != null)
            {
                Room.TeleportHero(side, collision.gameObject);
            }
        }   
    }
}
