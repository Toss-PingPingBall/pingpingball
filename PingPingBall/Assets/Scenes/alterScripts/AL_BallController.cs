using UnityEngine;

public class AL_BallController : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            GameManager.instance.addCollisionScore(EntityType.COIN);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.CompareTo("BasicBlock(Clone)") == 0)
        {
            GameManager.instance.addCollisionScore(EntityType.BLOCK_DEFAULT);
        }
    }
}