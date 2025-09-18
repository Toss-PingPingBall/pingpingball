using UnityEngine;

using System;

public class StartGame : MonoBehaviour
{
    private bool doStarted = false;
    public Action onClosed;

    private void OnEnable() { GameManager.instance.onUi = true; }
    private void OnDisable()
    {
        onClosed?.Invoke(); 
        GameManager.instance.onUi = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (0 < Input.touchCount)
            if (!doStarted)
            {
                doStarted = true;
                gameObject.SetActive(false);
            }
    }
}
