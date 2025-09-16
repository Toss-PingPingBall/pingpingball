using UnityEngine;

using System;

public class StartGame : MonoBehaviour
{
    private bool doStarted = false;
    public Action whenStart;

    private void Awake()
    {
        GameManager.instance.onUi = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (0 < Input.touchCount)
            if (!doStarted)
            {
                doStarted = true;
                GameManager.instance.onUi = false;
                whenStart();
            }
    }
}
