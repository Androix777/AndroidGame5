using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    [SerializeField] Generator generator;
    [SerializeField] GameObject Hero;

    Vector2 PosHero;
    GameObject RoomWithHero;

    private void Start()
    {
        
    }

    public void Teleport(Vector2 pos, GameObject Hero)
    {
        if (generator != null && Hero != null)
        {
            if (generator.GetLayout(generator.GetRooms(), pos))
            {
                if (Hero != null && generator.GetGameObjectsRooms()[(int)pos.x, (int)pos.y] != null)
                {
                    Hero.transform.position = generator.GetGameObjectsRooms()[(int)pos.x, (int)pos.y].transform.position;
                    PosHero = new Vector2((int)pos.x, (int)pos.y);
                    RoomWithHero = generator.GetGameObjectsRooms()[(int)pos.x, (int)pos.y];
                }
            }
        }
    }

    public void Teleport(RoomType type, GameObject Hero)
    {
        if (generator != null && Hero != null)
        {
            Vector2 pos = Vector2.positiveInfinity;
            foreach (GameObject room in generator.GetGameObjectsRooms())
            {
                if (room.GetComponent<RoomView>().GetRoomType() == type)
                {
                    pos = room.transform.position;
                    break;
                }
            }
           
            if (pos != Vector2.positiveInfinity && generator.GetLayout(generator.GetRooms(), pos))
            {
                if (Hero != null && generator.GetGameObjectsRooms()[(int)pos.x, (int)pos.y] != null)
                {
                    Hero.transform.position = generator.GetGameObjectsRooms()[(int)pos.x, (int)pos.y].transform.position;
                    PosHero = new Vector2((int)pos.x, (int)pos.y);
                    RoomWithHero = generator.GetGameObjectsRooms()[(int)pos.x, (int)pos.y];
                }
            }
        }
    }

    public void Teleport(Side side, Vector2 pos,GameObject Hero)
    {
        if (generator != null && Hero != null)
        {
            Vector2 position = pos;

            switch (side)
            {
                case Side.Up:
                    position = pos + Vector2.up;
                    break;
                case Side.Right:
                    position = pos + Vector2.right;
                    break;
                case Side.Down:
                    position = pos + Vector2.down; 
                    break;
                case Side.Left:
                    position = pos + Vector2.left;
                    break;
            }

            if (generator.GetLayout(generator.GetRooms(), position))
            {
                if (Hero != null && generator.GetGameObjectsRooms()[(int)position.x, (int)position.y] != null)
                {
                    Vector2 vector = generator.GetGameObjectsRooms()[(int)position.x, (int)position.y].GetComponent<RoomView>().GetTeleportPosition(side);
                    if (vector != Vector2.positiveInfinity)
                    {
                        Hero.transform.position = vector;
                        PosHero = new Vector2((int)position.x, (int)position.y);
                        RoomWithHero = generator.GetGameObjectsRooms()[(int)position.x, (int)position.y];
                    }
                }
            }
        } 
    }


}
