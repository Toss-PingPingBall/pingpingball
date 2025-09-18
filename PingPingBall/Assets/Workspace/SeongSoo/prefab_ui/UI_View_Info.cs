using UnityEngine;
using System;

public class UI_View_Info : MonoBehaviour
{
    [SerializeField]
    private UI_Button closeBtn;

    private void OnEnable() { GameManager.instance.onUi = true; }
    private void OnDisable() { GameManager.instance.onUi = false; }

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
