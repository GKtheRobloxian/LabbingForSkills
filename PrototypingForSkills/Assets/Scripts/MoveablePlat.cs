using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlat : MonoBehaviour
{
    public Vector3 startNode;
    public Vector3 endNode;
    public float time;
    public bool moveForward = false;
    public bool moveBackward = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startNode;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveForward)
        {
            transform.Translate((endNode - startNode) / time * Time.deltaTime);
            if ((endNode - transform.position).magnitude < 0.5f)
            {
                moveForward = false;
            }
        }
        else if (moveBackward)
        {
            transform.Translate((startNode - endNode) / time * Time.deltaTime);
            if ((startNode - transform.position).magnitude < 0.5f)
            {
                moveBackward = false;
            }
        }

        if (!moveBackward && !moveForward)
        {
            transform.position = transform.position;
        }
    }

    public void TimeToMove()
    {
        moveForward = true;
        moveBackward = false; 
    }

    public void TimeToBack()
    {
        moveBackward = true;
        moveForward = false;
    }
}
