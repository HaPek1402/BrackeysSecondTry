using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Top,
    Down,
    Left,
    Right
}

public class SnakeHeadScript : TailPieceScript
{
    public Direction direction;
    private float stepSize = 0.5f;
    public SnakeGameScript snakeMap;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MoveHead", 0, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveHead()
    {
        int zMove = 0;
        int xMove = 0;
        bool hasEaten = false;
        switch (direction)
        {
            case Direction.Top:
                zMove = 1;
                break;
            case Direction.Down:
                zMove = -1;
                break;
            case Direction.Left:
                xMove = -1;
                break;
            case Direction.Right:
                xMove = 1;
                break;
        }
        Vector3 newPosition = transform.position + new Vector3(xMove * stepSize, 0, zMove * stepSize);
        if (IsTagOnPosition(newPosition, "Food"))
        {
            hasEaten = true;
            score++;
            Debug.Log("eat");
        }
        if (IsTagOnPosition(newPosition, "Tail") || !IsTagOnPosition(newPosition, "Tile"))
        {
            Debug.Log("GameOver");
        }
        if (nextPiece != null)
        {
            nextPiece.Move();
        }
        transform.position = newPosition;
        if (hasEaten)
        {
            snakeMap.SpawnFood();
            CreateNextPiece();
        }
    }

    bool IsTagOnPosition(Vector3 position, string tag)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.03f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(tag))
            {
                if (collider.CompareTag("Food"))
                {
                    Destroy(collider.gameObject);
                }
                return true;
            }
        }
        return false;
    }    
}