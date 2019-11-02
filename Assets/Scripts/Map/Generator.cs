using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;


public enum LevelType { Basic,Hell };
public enum RoomType {Boss, Normal, Shop, Start, End};
public enum Side { Up, Right, Down, Left}
public enum TypePos { One, Two, Three, Four}

public class Generator : MonoBehaviour
{
    const float scale = 3;

    [SerializeField] GameObject Prot;
    [SerializeField] int floorWidth = 0, floorLenght = 0;
    [SerializeField] int numberRooms;
    [SerializeField] int Seed;
    [SerializeField] LevelType LevelType;


    Room[,] Map;
    GameObject[,] MapObj;

    

    [System.Serializable]
    struct RoomObj
    {
        public RoomType type;
        public string name;
        public LevelType[] level;
    }

    void SetParameters(int floorWidth,int floorLenght, int numberRooms, int Seed, LevelType LevelType)
    {
        this.floorWidth = floorWidth;
        this.floorLenght = floorLenght;
        this.numberRooms = numberRooms;
        this.Seed = Seed;
        this.LevelType = LevelType;
    }

    private void Start()
    {
        GenerationMap();
    }

    public void GenerationMap(int floorWidth, int floorLenght, int numberRooms, int Seed, LevelType LevelType)
    {
        this.floorWidth = floorWidth;
        this.floorLenght = floorLenght;
        this.numberRooms = numberRooms;
        this.Seed = Seed;
        this.LevelType = LevelType;
        GenerationMap();
    }

    public void GenerationMap()
    {
        Random.InitState(Seed);
        Map = new Room[floorWidth, floorLenght];
        MapObj = new GameObject[floorWidth, floorLenght];
        List<Room> roomTypes = new List<Room>();

        float dist = 0;
        Room end = new Room();
        Room Last = new Room();
        for (int i = 0; i < numberRooms; i++)
        {
            
            if (i == 0)
            {
                int x = Random.Range(0, floorWidth - 1);
                int y = Random.Range(0, floorLenght - 1);

                Map[x, y] = new Room();
                Map[x, y].position = (new Vector2(x, y));
                Map[x, y].type = (RoomType.Start);
                roomTypes.Add(Map[x, y]);
                Last = Map[x, y];
            }
            else
            {
                Vector2 pos = GenNextPosition(Map, Last.position);
                if (pos == (-Vector2.one * 10000))
                {
                    do
                    {
                        Last = GetLast(roomTypes, Map);
                        if (Last == null)
                        {
                            break;
                        }
                        pos = GenNextPosition(Map, Last.position);
                    }
                    while (pos == (-Vector2.one * 10000));
                }
                Debug.Log(pos);
                Map[(int)pos.x, (int)pos.y] = new Room();
                Map[(int)pos.x, (int)pos.y].position = (pos);
                Map[(int)pos.x, (int)pos.y].type = (RoomType.Normal);
                roomTypes.Add(Map[(int)pos.x, (int)pos.y]);
                Last = Map[(int)pos.x, (int)pos.y];
                if (Vector2.Distance(pos, roomTypes[0].position) > dist)
                {
                    dist = Vector2.Distance(pos, roomTypes[0].position);
                    end = Map[(int)pos.x, (int)pos.y];
                }
                
            }
            
        }
        
        end.type = (RoomType.End);
        SetAllLayouts(Map);
        CreateRooms(Map);

    }

    public Room[,] SetAllLayouts(Room [,] map)
    {
        for (int i = 0;i < floorWidth;i++)
        {
            for (int j = 0; j < floorLenght; j++)
            {
                if (map[i,j] != null)
                {
                    map[i, j].posType = (GetLayout(map, i, j));
                }
            }
        }
        return map;
    }

    public TypePos GetLayout(Room[,] map, int x ,int y)
    {
        bool[] side = new bool[4] {false, false, false, false};
        int col = -1;
        if (GetLayout(map, new Vector2(x,y) + Vector2.up))
        {
            col++;
            side[0] = true;
        }

        if (GetLayout(map, new Vector2(x, y) + Vector2.right))
        {
            col++;
            side[1] = true;
        }

        if (GetLayout(map, new Vector2(x, y) + Vector2.down))
        {
            col++;
            side[2] = true;
        }

        if (GetLayout(map, new Vector2(x, y) + Vector2.left))
        {
            col++;
            side[3] = true;
        }

        if (col > -1)
        {
            map[x, y].SideActiv = (side);
            return (TypePos)col;
        }
        return 0;
    }

    public Room GetLast(List<Room> rooms,Room[,] Map)
    {
        Room Last = rooms[rooms.Count - 1];
        while (true)
        {
            Vector2 pos = GenNextPosition(Map, Last.position);
            if (pos == (-Vector2.one * 10000))
            {
                if (rooms.Count > 1)
                {
                    rooms.RemoveAt(rooms.Count - 1);
                    Last = rooms[rooms.Count - 1];
                }
                else return null;
            }
            else return Last;
        }        
    }

    public Vector2 GenNextPosition(Room[,] map, Vector2 lastPos)
    {
        int startSide = Random.Range(0, 4);
        for (int i = 0; i < 4;i++)
        {
            int side = (i + startSide) % 4;
            switch (side)
            {
                case 0:
                    if (CheckRoomExist(map, lastPos + Vector2.up))
                    {
                        return lastPos + Vector2.up;
                    }
                    break;
                case 1:
                    if (CheckRoomExist(map, lastPos + Vector2.right))
                    {
                        return lastPos + Vector2.right;
                    }
                    break;
                case 2:
                    if (CheckRoomExist(map, lastPos + Vector2.down))
                    {
                        return lastPos + Vector2.down;
                    }
                    break;
                case 3:
                    if (CheckRoomExist(map, lastPos + Vector2.left))
                    {
                        return lastPos + Vector2.left;
                    }
                    break;
                default:
                    if (CheckRoomExist(map, lastPos + Vector2.up))
                    {
                        return -Vector2.one * 10000;
                    }
                    break;
            }
        }
        return - Vector2.one * 10000;
    }

    public bool CheckRoomExist(Room[,] map, Vector2 lastPos)
    {
        if (map != null && lastPos != null)
        {
            if (lastPos.y < floorLenght && lastPos.y >= 0 && lastPos.x < floorWidth && lastPos.x >= 0)
            {
                if (map[(int)lastPos.x, (int)lastPos.y] == null)
                {
                    return true;
                }
            }
        }        
        return false;
    }

    public bool GetLayout(Room[,] map, Vector2 lastPos)
    {
        if (map != null && lastPos != null)
        {
            if (lastPos.y < floorLenght && lastPos.y >= 0 && lastPos.x < floorWidth && lastPos.x >= 0)
            {
                if (map[(int)lastPos.x, (int)lastPos.y] != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void CreateRooms(Room[,] map)
    {
        for (int i = 0; i < floorWidth; i++)
        {
            for (int j = 0; j < floorLenght; j++)
            {
                if (map[i,j] != null)
                {
                    
                    GameObject obj = Instantiate(Prot);
                    obj.transform.position = new Vector3(i * scale, j * scale);
                    obj.GetComponent<RoomView>().SetStat(map[i, j]);
                    MapObj[i, j] = obj;
                }
                
            }
        }
    }
    
    void DeleteRooms()
    {
        for (int i = 0; i < floorWidth; i++)
        {
            for (int j = 0; j < floorLenght; j++)
            {
                if (MapObj[i, j] != null)
                {
                    Destroy(MapObj[i, j]);
                }
            }
        }
    }

    public Room[,] GetRooms()
    {
        return Map;
    }

    public GameObject[,] GetGameObjectsRooms()
    {
        return MapObj;
    }
}
