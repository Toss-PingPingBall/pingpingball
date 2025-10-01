using UnityEngine;
using System;

public class GameManager
{
    // ======================== Singleton ==========================

    private static GameManager _instance = null;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();
            return _instance;
        }
    }

    private GameManager()
    {
        score = 0;
        allowedRevival = 2;
        onUi = true;
    }

    // ======================== External Events ==========================

    public Action scoreChanged;
    public Action<bool> onUiChanged;
    public Action<float> onBallSizeChanged; // �� ũ�� Ȯ�� ȿ�� ����/�ð� ���� �̺�Ʈ (float duration ����)

    // ------ score functions ------ 

    private float[] timer = new float[4];
    private int blocks = 0;

    private void blockBonus()
    {
        if (timer.Length <= blocks)
        {
            // 2�� �ȿ� 5�� ��� �浹��
            if (Time.time - timer[0] <= 2.0)
            {
                score += 50;
                blocks = 0;
            }
            // 2�� �ʰ��� ��� �ð� ���
            else
            {
                for (int i = 1; i < timer.Length; i++)
                    timer[i - 1] = timer[i];
                timer[timer.Length - 1] = Time.time;
            }
        }
        else
            timer[blocks++] = Time.time;
    }

    public void addCollisionScore(EntityType type)
    {
        switch (type)
        {
            case EntityType.COIN:
                score += 100;
                break;
            case EntityType.BLOCK_DEFAULT:
            case EntityType.BLOCK_ONE_SHOT:
                blockBonus();
                score += 5;
                break;
        }
    }

    public void addBreakScore(EntityType type)
    {
        switch (type)
        {
            case EntityType.BLOCK_ONE_SHOT:
                score += 30;
                break;
        }
    }

    public void applyBallSizeUp(float duration) // �� ũ�� Ȯ�� ȿ�� ���� �� ���� �ð� ����
    {
        onBallSizeChanged?.Invoke(duration);
    }

    public void cancelBallSizeUp() // �� ũ�� Ȯ�� ȿ�� ����
    {
        onBallSizeChanged?.Invoke(0f);
    }

    public void invokeCollisionProcess(GameObject go) // ���� �浹�� ������Ʈ�� ó���ϴ� �߾� �޼ҵ�
    {
        if (go.CompareTag("Coin"))
        {
            addCollisionScore(EntityType.COIN);
            UnityEngine.Object.Destroy(go);
            return;
        }

        if (go.CompareTag("ItemSizeUp"))
        {
            applyBallSizeUp(10.0f); // ���� �ð��� 10.0�ʷ� ����
            UnityEngine.Object.Destroy(go);
            return;
        }

        if (go.name.Contains("Block"))
        {
            if (go.name.Contains("OneShot"))
            {
                addCollisionScore(EntityType.BLOCK_ONE_SHOT);
            }
            else
            {
                addCollisionScore(EntityType.BLOCK_DEFAULT);
            }
            return;
        }
    }

    // ======================== Game System ==========================

    // ------ properties ------ 

    private int _score;
    public int score { get { return _score; } private set { _score = value; scoreChanged?.Invoke(); } }
    public int allowedRevival { get; private set; }
    private bool _onUi;
    public bool onUi { get { return _onUi; } private set { _onUi = value; onUiChanged?.Invoke(_onUi); } }

    // ------ setter ------ 

    public bool canRevive()
    {
        return (allowedRevival <= 0);
    }

    public void revive()
    {
        if (!canRevive())
            throw new Exception("�� �̻� ��Ȱ�� ��ȸ�� �����ϴ�!");
        allowedRevival--;
    }

    public void changeOnUi(bool isOnUi)
    {
        onUi = isOnUi;
    }
}