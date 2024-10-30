using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickyUppy : MonoBehaviour
{
    public bool pickedUp;
    GameObject objPicker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            transform.position = objPicker.transform.position + objPicker.transform.forward;
            transform.Rotate(Vector3.up * 90 * Time.deltaTime);
        }
    }

    public void PickedUp(GameObject gamer)
    {
        objPicker = gamer;
        pickedUp = true;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void Dropped(GameObject gamer)
    {
        objPicker = null;
        pickedUp = false;
        GetComponent<BoxCollider>().isTrigger = false;
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
