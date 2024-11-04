using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    Rigidbody rb;
    public bool gettingFixed = false;
    public bool operative = false;
    public char[] availableSequence;
    public List<char> currentSequence = new List<char>();
    public int currentPartRunning;
    public float locateRad;
    public float pickupRad = 2.5f;
    float currentClosest = 0;
    public float moveSpeed;
    public bool onTheMove;
    GameObject objectToGoTo = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentClosest = locateRad;
    }

    // Update is called once per frame
    void Update()
    {
        if (gettingFixed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                foreach (char c in availableSequence)
                {
                    if (c.ToString() == "1")
                    {
                        currentSequence.Add(c);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                foreach (char c in availableSequence)
                {
                    if (c.ToString() == "3")
                    {
                        currentSequence.Add(c);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                foreach (char c in availableSequence)
                {
                    if (c.ToString() == "4")
                    {
                        currentSequence.Add(c);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                foreach (char c in availableSequence)
                {
                    if (c.ToString() == "5")
                    {
                        currentSequence.Add(c);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                foreach (char c in availableSequence)
                {
                    if (c.ToString() == "2")
                    {
                        currentSequence.Add(c);
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                currentPartRunning = 0;
                ReadyToGo();
            }
        }
        
        if (objectToGoTo != null)
        {
            transform.LookAt(objectToGoTo.transform.position - Vector3.up * (objectToGoTo.transform.position.y - transform.position.y));
        }
    }

    void ReadyToGo()
    {
        StartCoroutine(CodeRun());
        gettingFixed = false;
        operative = true;
    }

    IEnumerator CodeRun()
    {
        yield return new WaitForSeconds(0.5f);
        if (currentPartRunning < currentSequence.Count)
        {
            if (char.ToString(currentSequence[currentPartRunning]) == "1")
            {
                StartCoroutine(Locate());
            }
            if (char.ToString(currentSequence[currentPartRunning]) == "3")
            {
                onTheMove = true;
                StartCoroutine(GoToObject());
            }
            if (char.ToString(currentSequence[currentPartRunning]) == "4")
            {
                StartCoroutine(PickUp());
            }
            if (char.ToString(currentSequence[currentPartRunning]) == "2")
            {
                StartCoroutine(Return());
            }
            currentPartRunning++;
        }
    }

    IEnumerator Locate()
    {
        objectToGoTo = null;
        yield return new WaitForSeconds(0.75f);
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        currentClosest = locateRad;
        foreach (GameObject p in pickups)
        {
            if ((p.transform.position - transform.position).magnitude < currentClosest)
            {
                currentClosest = (p.transform.position - transform.position).magnitude;
                objectToGoTo = p;
            }
        }
        StartCoroutine(CodeRun());
    }

    IEnumerator GoToObject()
    {
        if (onTheMove && objectToGoTo != null)
        {
            rb.velocity = Vector3.zero + Vector3.up * rb.velocity.y;
            if ((objectToGoTo.transform.position - transform.position).magnitude < pickupRad)
            {
                onTheMove = false;
                StartCoroutine(CodeRun());
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                rb.AddRelativeForce(Vector3.forward * moveSpeed, ForceMode.VelocityChange);
                yield return new WaitForEndOfFrame();
                StartCoroutine(GoToObject());
            }
        }
    }

    IEnumerator PickUp()
    {
        yield return new WaitForSeconds(0.4f);
        if (objectToGoTo.CompareTag("Pickup"))
        {
            objectToGoTo.GetComponent<PickyUppy>().PickedUp(gameObject);
        }
        StartCoroutine(CodeRun());
    }

    void Drop()
    {

    }

    IEnumerator Return()
    {
        yield return new WaitForSeconds(0.75f);
        objectToGoTo = GameObject.FindWithTag("Player");
        StartCoroutine(CodeRun());
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Platform"))
        {
            transform.parent = c.gameObject.transform;
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}
