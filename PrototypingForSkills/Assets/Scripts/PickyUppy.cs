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
}
