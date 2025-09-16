using UnityEngine;

public class GmScript : MonoBehaviour
{
    public GameObject ball;
    public StartGame start_ui;
    public BlockSpawner blockSpawner;
    public ItemSpawner itemSpawner;
    public CoinSpawner coinSpawner;

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

    private void initalize()
    {
        start_ui.whenStart += startGame;
        blockSpawner.gameObject.SetActive(false);
        itemSpawner.gameObject.SetActive(false);
        coinSpawner.gameObject.SetActive(false);
    }

    // ======================== Game Starter ==========================

    private void startGame()
    {
        start_ui.gameObject.SetActive(false);
        blockSpawner.gameObject.SetActive(true);
        itemSpawner.gameObject.SetActive(true);
        coinSpawner.gameObject.SetActive(true);
    }

    // ======================== Pause System ==========================

    private bool ballPaused = false;
    private Vector2 ballVelocity;
    private float ballAngularVelocity;

    private void processPause()
    {
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
