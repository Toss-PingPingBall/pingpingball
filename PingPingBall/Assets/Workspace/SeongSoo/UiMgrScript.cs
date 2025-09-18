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

        if (start_ui != null) { start_ui.onShow += openUi;  start_ui.onClosed += closeUi; }
        if (gameover_ui != null) { gameover_ui.onShow += openUi; gameover_ui.onClosed += closeUi; }
        if (pause_ui != null) { pause_ui.onShow += openUi; pause_ui.onClosed += closeUi; }
        if (info_ui != null) { info_ui.onShow += openUi; info_ui.onClosed += closeUi; }

        if (start_ui != null) start_ui.onGameStart += gameStarted;

        if (infoBt_ui != null) infoBt_ui.onClick += () => info_ui?.show();
        if (stopBt_ui != null) stopBt_ui.onClick += () => pause_ui?.show(GameManager.instance.score, 1);
    }

    // ======================== internal process ==========================

    private void gameStarted()
    {
        onGameStartedEvent?.Invoke();
    }

    private int runningUiCnt = 0;

    private void openUi()
    {
        runningUiCnt++;
        if (1 == runningUiCnt)
            GameManager.instance.changeOnUi(true);
    }

    private void closeUi()
    {
        runningUiCnt--;
        if (0 == runningUiCnt)
            GameManager.instance.changeOnUi(false);
    }

    // ======================== external process ==========================

    public Action onGameStartedEvent;

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
