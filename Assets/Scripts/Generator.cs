using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject player;
    private GameObject room;
    private int levelNum;
    private LevelName levelName;
    int NORMAL_NUM = 3, TREASURY_NUM = 1;
    void Start()
    {
        CreateRoom("Prefabs/Rooms/Normal/Room1");
        levelNum = 1;
        levelName = LevelName.Start;
    }

    void Update()
    {
        GameObject firstEnemy = GameObject.FindWithTag("Enemy");
        if (!firstEnemy)
        {
            room.transform.Find("Exit").gameObject.SetActive(true);
        }
    }

    public void NextLevel()
    {

        CreateRoom(CalcChance(levelNum, levelName));
        levelNum++;
    }

    void CreateRoom(string path)
    {
        player.transform.parent = gameObject.transform;
        Destroy(room);
        room = Instantiate(Resources.Load(path) as GameObject, gameObject.transform.position, Quaternion.identity);
        room.transform.parent = gameObject.transform;
        player.transform.position = room.transform.Find("Spawn").transform.position;
        player.transform.parent = room.transform;
    }

    string CalcChance(int level, LevelName name)
    {
        switch(name)
        {
            case LevelName.Start:
                if (level % 2 == 0) return "Prefabs/Rooms/Normal/Room"+Random.Range(1,NORMAL_NUM+1).ToString();
                else return "Prefabs/Rooms/Treasury/Room"+Random.Range(1,TREASURY_NUM+1).ToString();
            default:
                return "No";
        }
    }
}
