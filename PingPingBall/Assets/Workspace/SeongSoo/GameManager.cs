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
