using UnityEngine;

public class GmScript : MonoBehaviour
{
    public GameObject ball;
    public GameObject start_ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameManager singleton = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // check State
        
        // apply state
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
