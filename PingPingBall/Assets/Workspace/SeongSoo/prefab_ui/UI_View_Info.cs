using UnityEngine;
using System;

public class UI_View_Info : MonoBehaviour
{
    [SerializeField]
    private UI_Button closeBtn;

    // ======================== UI External Event ==========================

    public Action onShow;
    public Action onClosed;
    
    // =======================================================================

    private void OnEnable() { onShow?.Invoke(); }
    private void OnDisable() { onClosed?.Invoke(); }

    public void show()
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
        if (closeBtn == null)
        {
            Debug.LogError("UI�� ����Ǿ�� �� ������Ʈ�� ã�� �� �����ϴ�!! : " + nameof(closeBtn));
            return;
        }

        closeBtn.onClick += () => gameObject.SetActive(false);
    }
}
