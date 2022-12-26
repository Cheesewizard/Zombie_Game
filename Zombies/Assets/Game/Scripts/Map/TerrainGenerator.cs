using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject[] tiles;

    public Vector3 tileStartPos;
    Vector2 tileSpacing;

    public int gridWidth;
    public int gridHeight;

    // Start is called before the first frame update
    void Start()
    {
        tileSpacing = tiles[0].GetComponent<Renderer>().bounds.size;
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateMap()
    {
        GenerateGrass();
        GenerateFoliage();
        GenerateDirt();
        GenerateObjects();
    }

    private void GenerateGrass()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Instantiate(tiles[0], new Vector3(tileStartPos.x + (y * tileSpacing.x), tileStartPos.y + (x * tileSpacing.y)), Quaternion.identity);
            }
        }
    }

    private void GenerateFoliage()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                var index = Random.Range(0, 100);
                if (index % 3 == 0)
                {
                    Instantiate(tiles[1], new Vector3(tileStartPos.x + (y * tileSpacing.x), tileStartPos.y + (x * tileSpacing.y)), Quaternion.identity);
                }
            }
        }
    }

    private void GenerateDirt()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                var index = Random.Range(0, 100);
                if (index % 2 == 0)
                {
                    Instantiate(tiles[2], new Vector3(tileStartPos.x + (y * tileSpacing.x), tileStartPos.y + (x * tileSpacing.y)), Quaternion.identity);
                }
            }
        }
    }

    private void GenerateObjects()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                var index = Random.Range(0,100);
                if (index % 50 == 0)
                {
                    Instantiate(tiles[3], new Vector3(tileStartPos.x + (y * tileSpacing.x), tileStartPos.y + (x * tileSpacing.y)), Random.rotation);
                }
            }
        }
    }
}
