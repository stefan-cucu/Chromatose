using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRayBehaviour : MonoBehaviour
{
    public enum Direction
    {
        Up, Right, Down, Left
    }
    public Direction direction;

    [HideInInspector]
    public Color laserColor;

    private Vector2 vectorDirection;
    private Vector3 pos;

    void Start()
    {
        pos = transform.position;

        if (direction == Direction.Up)
            vectorDirection = Vector2.up;
        else if (direction == Direction.Right)
            vectorDirection = Vector2.right;
        else if (direction == Direction.Down)
            vectorDirection = Vector2.down;
        else if (direction == Direction.Left)
            vectorDirection = Vector2.left;
        else
        {
            vectorDirection = new Vector2();
            Debug.Log("!! No direction chosen for laser " + gameObject.name);
        }

        GetComponent<LineRenderer>().startColor = laserColor;
        GetComponent<LineRenderer>().endColor = laserColor;
    }

    void Update()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0, pos);
        RaycastHit2D hit = Physics2D.Raycast(pos, vectorDirection);
        if(hit.collider == null)
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(1, vectorDirection * 1000f);
        }
        else
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(1, hit.point);
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.SetActive(false);
                PlayerMovement.endLevel = true;
            }
        }
        
    }
}
