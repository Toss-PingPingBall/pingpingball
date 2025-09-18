using UnityEngine;

using System;

public class StartGame : MonoBehaviour
{
    private bool doStarted = false;
    public Action onClosed;
    public Action onStarted;

    void Start()
    {
        onStarted?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (0 < Input.touchCount)
            if (!doStarted)
            {
                doStarted = true;
                onClosed?.Invoke();
                gameObject.SetActive(false);
            }
    }
}
