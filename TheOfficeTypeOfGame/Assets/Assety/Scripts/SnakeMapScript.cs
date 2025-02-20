using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGameScript : MonoBehaviour
{
    public GameObject mapTilePrefab;
    public GameObject snakePrefab;
    public GameObject foodPrefab;
    public int mapWidth = 15;
    public int mapHeight = 15;
    public float tileSize = 0.05f;
    public float offset = 0f;
    public List<Vector3> snake;

    private void Start()
    {
        GenerateTileMap();
        GameObject snake = Instantiate(snakePrefab, new Vector3(transform.position.x + 2f, transform.position.y + 0.01f, transform.position.z+4f), Quaternion.identity);
        snake.transform.parent = transform;
        GameObject snakeHead = snake.transform.Find("SnakeHead").gameObject;
        snakeHead.GetComponent<SnakeHeadScript>().snakeMap = this;

        SpawnFood();
    }

    private void GenerateTileMap()
    {
        for (float x = 0; x < mapWidth; x++)
        {
            for (float z = 0; z < mapHeight; z++)
            {
                Vector3 position = new Vector3(transform.position.x + x * tileSize / 2, transform.position.y, transform.position.z + z * tileSize / 2); // Set the position of the tile
                GameObject tile = Instantiate(mapTilePrefab, position, Quaternion.identity); // Instantiate the tile
                tile.transform.parent = transform;
            }
        }
    }

    public void SpawnFood()
    {
        bool correctPosition = false;
        Vector3 chosenPosition = Vector3.zero;
        int x;
        int z;
        while (!correctPosition)
        {
            x = Random.Range(0, mapWidth);
            z = Random.Range(0, mapHeight);
            chosenPosition = new Vector3(transform.position.x + x * tileSize / 2, transform.position.y, transform.position.z + z * tileSize / 2);
            if (!IsPositionOccupied(chosenPosition))
            {
                correctPosition = true;
            }
        }
        Instantiate(foodPrefab, chosenPosition, Quaternion.identity);
    }

    bool IsPositionOccupied(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.01f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("SnakeHead") || collider.CompareTag("Tail")) // Check by tags
            {
                return true;
            }
        }
        return false;
    }
}
