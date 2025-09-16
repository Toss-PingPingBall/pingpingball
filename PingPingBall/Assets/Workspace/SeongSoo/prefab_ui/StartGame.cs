using UnityEngine;

using System;

public class StartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (0 < Input.touchCount)
        {
            Debug.Log(DateTime.Now.ToString() + " : touch detected");
            gameObject.SetActive(false);
        }
    }
}
