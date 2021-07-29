using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTest : MonoBehaviour
{
    private float timer = 0;
    private float timerThresh = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= timerThresh)
        {
            timer += Time.deltaTime;
        }
        // else
        // {
        //     timer = 0;
        // }    
        
        Banana();
        
        Debug.Log(timer);
    }

    void Banana()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            timer = 0;
        }
    }
}
