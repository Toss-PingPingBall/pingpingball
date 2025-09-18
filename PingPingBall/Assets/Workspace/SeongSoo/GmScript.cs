using UnityEngine;

public class GmScript : MonoBehaviour
{
    [Header("game object")]
    public GameObject ball;
    public BlockSpawner blockSpawner;
    public ItemSpawner itemSpawner;
    public CoinSpawner coinSpawner;

    [Header("ui")]
    public StartGame start_ui;
    public TMPro.TMP_Text score_ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        initalize();
    }

    // Update is called once per frame
    void Update()
    {
        // check State

        // apply state
        processPause();
    }

    // ======================== Game Initializer ==========================

    private void warnNullProperty(bool isNull, string name)
    {
        if (isNull) Debug.LogWarning("[" + gameObject.name + "] : " + name + "이 등록되지 않았습니다.");
    }

    private void initalize()
    {
        warnNullProperty(ball == null, nameof(ball));
        warnNullProperty(blockSpawner == null, nameof(blockSpawner));
        warnNullProperty(itemSpawner == null, nameof(itemSpawner));
        warnNullProperty(coinSpawner == null, nameof(coinSpawner));

        warnNullProperty(start_ui == null, nameof(start_ui));
        warnNullProperty(score_ui == null, nameof(score_ui));

        if (start_ui != null)
            start_ui.whenStart += startGame;
        GameManager.instance.scoreChanged += changeScore;
        changeScore();

        blockSpawner?.gameObject.SetActive(false);
        itemSpawner?.gameObject.SetActive(false);
        coinSpawner?.gameObject.SetActive(false);
    }

    // ======================== Game Starter ==========================

    private void startGame()
    {
        start_ui?.gameObject.SetActive(false);
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

    // ======================== Score Display System ==========================

    private void changeScore()
    {
        if (score_ui != null)
            score_ui.text = string.Format("{0:#,##0}", GameManager.instance.score);
    }
}
