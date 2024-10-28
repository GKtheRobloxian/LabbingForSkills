using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTime : MonoBehaviour
{
    Rigidbody rb;
    public float maxSpeed = 5;
    public float vertMove;
    public float horzMove;
    public float rotationSpeed;
    public bool grounded = false;
    public GameObject currentPick;
    public float pickupRad;
    public bool picking;
    // Start is called before the first frame update
    void Start()
    {
        currentPick = null;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vertMove = Input.GetAxisRaw("Vertical");
        horzMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = Vector3.zero + Vector3.up * rb.velocity.y;
        rb.AddRelativeForce(Vector3.forward * vertMove * maxSpeed, ForceMode.VelocityChange);

        transform.Rotate(horzMove * rotationSpeed * Time.deltaTime * Vector3.up);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddRelativeForce(Vector3.up * 10, ForceMode.Impulse);
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && grounded)
        {
            if (!picking)
            {
                GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
                foreach (GameObject p in pickups)
                {
                    if ((transform.position - p.transform.position).magnitude < pickupRad)
                    {
                        currentPick = p;
                    }
                }
                currentPick.GetComponent<PickyUppy>().PickedUp(gameObject);
                picking = true;
            }
            else
            {
                currentPick.GetComponent<PickyUppy>().Dropped(gameObject);
                picking = false;
            }
        }
    }

    void OnCollisionStay (Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (col.gameObject.CompareTag("Platform"))
        {
            grounded = true;
            transform.parent = col.gameObject.transform;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }

        if (col.gameObject.CompareTag("Platform"))
        {
            grounded = false;
            transform.parent = null;
        }
    }
}
