using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject player;
    private GameObject room;
    int[] MAX_ROOMS = new int[] {3, 1};
    void Start()
    {
        room = Instantiate(Resources.Load("Prefabs/Rooms/Normal/Room1") as GameObject, gameObject.transform.position, Quaternion.identity);
        room.transform.parent = gameObject.transform;
        player.transform.position = room.transform.Find("Spawn").transform.position;
        player.transform.parent = room.transform;
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
        player.transform.parent = gameObject.transform;
        Destroy(room);
        
        room = Instantiate(Resources.Load("Prefabs/Rooms/Normal/Room2") as GameObject, gameObject.transform.position, Quaternion.identity);
        room.transform.parent = gameObject.transform;
        player.transform.position = room.transform.Find("Spawn").transform.position;
        player.transform.parent = room.transform;
    }
}
