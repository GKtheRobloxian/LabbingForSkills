using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForEvents : MonoBehaviour
{
    public GameObject linkedObj;
    public int eventType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Pickup"))
        {
            if (eventType == 0)
            {
                MovePlatformEvent platEvent = GetComponent<MovePlatformEvent>();
                platEvent.Activate(linkedObj);
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Pickup"))
        {
            if (eventType == 0)
            {
                MovePlatformEvent platEvent = GetComponent<MovePlatformEvent>();
                platEvent.Deactivate(linkedObj);
            }
        }
    }
}
