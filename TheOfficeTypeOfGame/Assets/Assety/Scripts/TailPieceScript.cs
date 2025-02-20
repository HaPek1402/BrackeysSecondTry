using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailPieceScript : MonoBehaviour
{
    public TailPieceScript nextPiece;
    public TailPieceScript previousPiece;
    public Vector3 previousPosition;
    public GameObject tailPiecePrefab;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        if (nextPiece != null)
        {
            nextPiece.Move();
        }
        previousPosition = transform.position;
        transform.position = previousPiece.transform.position;
    }

    public void CreateNextPiece()
    {
        if (nextPiece != null)
        {
            nextPiece.CreateNextPiece();
            return;
        }
        GameObject newTailPiece = Instantiate(tailPiecePrefab, previousPosition, Quaternion.identity);
        TailPieceScript newTailScript = newTailPiece.GetComponent<TailPieceScript>();
        nextPiece = newTailScript;
        if (newTailScript != null)
        {
            newTailScript.previousPiece = this;
        }
    }
}