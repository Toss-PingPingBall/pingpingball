using UnityEngine;
using System;

public class UiMgrScript : MonoBehaviour
{
    [Header("ui")]
    public StartGame start_ui;
    public TMPro.TMP_Text score_ui;

    // ======================== Game Initializer ==========================

    private void warnNullProperty(bool isNull, string name)
    {
        if (isNull) Debug.LogWarning("[" + gameObject.name + "] : " + name + "이 등록되지 않았습니다.");
    }

    public void initalize()
    {
        warnNullProperty(start_ui == null, nameof(start_ui));
        warnNullProperty(score_ui == null, nameof(score_ui));

        if (start_ui != null)
        {
            start_ui.onStarted += onUiStarted;
            start_ui.onClosed += onUiClosed;
            start_ui.onClosed += gameStarted;
        }
    }

    // ======================== internal process ==========================

    private void onUiStarted()
    {
        GameManager.instance.onUi = true;
    }

    private void onUiClosed()
    {
        GameManager.instance.onUi = false;
    }

    // ======================== external process ==========================

    public Action onGameStartedEvent;
    private void gameStarted() { onGameStartedEvent?.Invoke(); }

    // ======================== operations ==========================

    public void changeScore(int score)
    {
        if (score_ui != null)
            score_ui.text = string.Format("{0:#,##0}", score);
    }
}
