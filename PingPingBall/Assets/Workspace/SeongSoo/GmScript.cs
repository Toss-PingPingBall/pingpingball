using UnityEngine;

public class GmScript : MonoBehaviour
{
    [Header("game object")]
    public GameObject ball;
    public BlockSpawner blockSpawner;
    public ItemSpawner itemSpawner;
    public CoinSpawner coinSpawner;

    [Header("game manager scripts")]
    public UiMgrScript uiMgr;

    // Update is called once per frame
    void Update()
    {
        // check State

        // apply state
        processPause();
    }

    // ======================== Game Initializer ==========================

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        initalize();
    }

    private void warnNullProperty(bool isNull, string name)
    {
        if (isNull) Debug.LogWarning("[" + gameObject.name + "] : " + name + "이 등록되지 않았습니다.");
    }

    private void initalize()
    {
        uiMgr.initalize();
        uiMgr.onGameStartedEvent += startGame;

        GameManager.instance.scoreChanged += () => uiMgr.changeScore(GameManager.instance.score);

        warnNullProperty(ball == null, nameof(ball));
        warnNullProperty(blockSpawner == null, nameof(blockSpawner));
        warnNullProperty(itemSpawner == null, nameof(itemSpawner));
        warnNullProperty(coinSpawner == null, nameof(coinSpawner));
        
        blockSpawner?.gameObject.SetActive(false);
        itemSpawner?.gameObject.SetActive(false);
        coinSpawner?.gameObject.SetActive(false);
    }

    // ======================== Game Starter ==========================

    private void startGame()
    {
        blockSpawner?.gameObject.SetActive(true);
        itemSpawner?.gameObject.SetActive(true);
        coinSpawner?.gameObject.SetActive(true);
    }

    // ======================== Pause System ==========================

    private bool ballPaused = false;
    private Vector2 ballVelocity;
    private float ballAngularVelocity;

    private void processPause()
    {
        if (ball == null) return;

        if (GameManager.instance.onUi && !ballPaused)
        {
            Rigidbody2D rigidbody = ball.GetComponent<Rigidbody2D>();
            ballVelocity = rigidbody.linearVelocity;
            ballAngularVelocity = rigidbody.angularVelocity;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

            ballPaused = true;
        }
        if (!GameManager.instance.onUi && ballPaused)
        {
            Rigidbody2D rigidbody = ball.GetComponent<Rigidbody2D>();
            rigidbody.linearVelocity = ballVelocity;
            rigidbody.angularVelocity = ballAngularVelocity;
            rigidbody.constraints = RigidbodyConstraints2D.None;

            ballPaused = false;
        }
    }
}
