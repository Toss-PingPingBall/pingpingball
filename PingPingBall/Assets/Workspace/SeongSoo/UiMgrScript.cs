using UnityEngine;
using System;

public class UiMgrScript : MonoBehaviour
{
    [Header("ui")]
    public StartGame start_ui;
    public TMPro.TMP_Text score_ui;
    public UI_Button stopBt_ui;

    [Header("ui - info")]
    public UI_View_Info info_ui;
    public UI_Button infoBt_ui;

    [Header("ui - stop")]
    public UI_GameOver gameover_ui;
    public UI_Pause pause_ui;

    // ======================== Game Initializer ==========================

    private void warnNullProperty(bool isNull, string name)
    {
        if (isNull) Debug.LogWarning("[" + gameObject.name + "] : " + name + "이 등록되지 않았습니다.");
    }

    public void initalize()
    {
        warnNullProperty(start_ui == null, nameof(start_ui));
        warnNullProperty(score_ui == null, nameof(score_ui));
        warnNullProperty(stopBt_ui == null, nameof(stopBt_ui));

        warnNullProperty(info_ui == null, nameof(info_ui));
        warnNullProperty(infoBt_ui == null, nameof(infoBt_ui));

        if (start_ui != null)
            start_ui.onClosed += gameStarted;

        if (infoBt_ui != null)
            infoBt_ui.onClick += () => info_ui?.show();

        if (stopBt_ui != null)
            stopBt_ui.onClick += () => pause_ui?.show(GameManager.instance.score, 1);
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

    public void gameOver()
    {
        gameover_ui?.show(GameManager.instance.score, 1);
    }
}
