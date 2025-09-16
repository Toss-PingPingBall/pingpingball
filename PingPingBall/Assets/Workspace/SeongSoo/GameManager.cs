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
    }

    // ======================== Game System ==========================

    // ------ properties ------ 

    public int score { get; private set; }
    public int allowedRevival { get; private set; }
    public bool onUi { get; set; }

    // ------ setter ------ 

    public void setScore(int score)
    {
        /* ������� �߰��� ��ȿ�� ������ �߰��� �� */
        this.score = score;
    }

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
}
