using UnityEngine;

public class OneShotBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            // GameManager 필요
            Destroy(gameObject);
        }
    }
}