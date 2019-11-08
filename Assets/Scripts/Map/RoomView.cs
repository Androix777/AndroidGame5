using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomView : MonoBehaviour
{
    [System.Serializable]
    struct Teleport
    {
        public Side side;
        public GameObject Obj;
    }

    [SerializeField] Room room;

    [SerializeField] RoomType RoomType;

    [SerializeField] bool[] SideActiv = new bool[4];

    [SerializeField] Vector2 position;

    [SerializeField] TypePos posType;

    [SerializeField] Teleport[] Teleports;

    [SerializeField]public Map Map{ get; set; }

    public void Start()
    {
        Map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
    }

    public void SetStat(Room room)
    {
        this.room = room;
        RoomType = room.type;
        SideActiv = room.SideActiv;

        for (int i = 0; i < 4; i++)
        {
            if (Teleports[i].Obj != null) Teleports[i].Obj.SetActive(SideActiv[i]);
        }

        position = room.position;
        posType = room.posType;

        //if (RoomType == RoomType.Start)
        //{
        //    GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
        //}

        //if (RoomType == RoomType.End)
        //{
        //    GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        //}
    }

    public RoomType GetRoomType()
    {
        return RoomType;
    }

    public void TeleportHero(Side side,GameObject Hero)
    {
        if (SideActiv[(int)side])
        {
            Map.Teleport(side, position, Hero);
        }
    }

    public Vector2 GetTeleportPosition(Side side)
    {
        switch (side)
        {
            case Side.Up:
                side = Side.Down;
                break;
            case Side.Right:
                side = Side.Left;
                break;
            case Side.Down:
                side = Side.Up;
                break;
            case Side.Left:
                side = Side.Right;
                break;
        }
        foreach (Teleport teleport in Teleports)
        {

            if (teleport.side == side)
            {
                return teleport.Obj.transform.position;
            }
        }
        return Vector2.positiveInfinity;
    }
}
