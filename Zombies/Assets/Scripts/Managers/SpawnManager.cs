using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnDelay = 2;
    public float spawnRate = 2;

    public GameObject[] zombies;

    public float minSpawnRadius = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnZombies", spawnDelay, spawnRate);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnZombies()
    {
        CreateZombieAtRandomPosition();
    }

    private void CreateZombieAtRandomPosition()
    {
        var index = Random.Range(0, 2);
        Instantiate(zombies[index], GetRandomPosition(), zombies[index].transform.rotation);
    }

    private Vector2 GetRandomPosition()
    {
        var index = Random.Range(0, 5);
        var ranWidth = 0f;
        var ranHeight = 0f;

        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

       switch (index)
        {
            case 1:
                ranHeight = bounds.y / 2 + minSpawnRadius;
                break;
            case 2:
                ranWidth = bounds.x / 2 + minSpawnRadius;
                break;
            case 3:
                ranHeight = bounds.y / 2 + -minSpawnRadius; 
                break;
            case 4:
                ranWidth = bounds.x / 2 + -minSpawnRadius; 
                break;
        }


        return new Vector2(ranWidth, ranHeight);
    }
}
