using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(GameObject platform)
    {
        MoveablePlat platScript = platform.GetComponent<MoveablePlat>();
        platScript.TimeToMove();
    }

    public void Deactivate(GameObject platform)
    {
        MoveablePlat platScript = platform.GetComponent<MoveablePlat>();
        platScript.TimeToBack();
    }
}
