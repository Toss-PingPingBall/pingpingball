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
        /* 제약사항 추가시 유효성 검증을 추가할 것 */
        this.score = score;
    }

    public bool canRevive() 
    { 
        return (allowedRevival <= 0); 
    }

    public void revive()
    {
        if (!canRevive())
            throw new Exception("더 이상 부활할 기회가 없습니다!");
        allowedRevival--;
    }
}
