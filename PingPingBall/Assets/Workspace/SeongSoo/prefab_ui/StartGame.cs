using UnityEngine;

using System;

public class StartGame : MonoBehaviour
{
    private bool doStarted = false;

    public Action onGameStart;
    public Action onShow;
    public Action onClosed;

    private void OnEnable() { onShow?.Invoke(); }
    private void OnDisable() { onClosed?.Invoke(); }

    // Update is called once per frame
    void Update()
    {
        if (0 < Input.touchCount)
            if (!doStarted)
            {
                doStarted = true;
                onGameStart?.Invoke();
                gameObject.SetActive(false);
            }
    }
}
