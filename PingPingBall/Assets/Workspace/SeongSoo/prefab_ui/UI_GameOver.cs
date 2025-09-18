using UnityEngine;
using System;
using System.Text;

public class UI_GameOver : MonoBehaviour
{
    [SerializeField]
    private UI_Button replayBtn;
    [SerializeField]
    private TMPro.TMP_Text round_ui;
    [SerializeField]
    private TMPro.TMP_Text score_ui;

    public Action onReplay;

    private void OnEnable() { GameManager.instance.onUi = true; }
    private void OnDisable() { GameManager.instance.onUi = false; }

    private void Start()
    {
        StringBuilder nullComponents = new StringBuilder();
        if (replayBtn == null) nullComponents.Append('\n').Append(nameof(replayBtn));
        if (round_ui == null) nullComponents.Append('\n').Append(nameof(round_ui));
        if (score_ui == null) nullComponents.Append('\n').Append(nameof(score_ui));

        if (0 < nullComponents.Length)
        {
            Debug.LogError("UI에 연결되어야 할 컴포넌트를 찾을 수 없습니다!! : " + nullComponents.ToString());
            return;
        }

        if (replayBtn != null)
            replayBtn.onClick += () =>
            {
                onReplay?.Invoke();
                gameObject.SetActive(false);
            };
    }

    public void show(int score, int round)
    {
        if (round_ui != null) round_ui.text = "ROUND" + round;
        if (score_ui != null) score_ui.text = string.Format("{0:#,##0}", score);
    }
}
