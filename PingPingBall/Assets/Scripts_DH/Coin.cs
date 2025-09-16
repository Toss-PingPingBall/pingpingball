using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            // GameManager 필요

            Destroy(gameObject); // 일단 먹으면 사라지도록
        }
    }
}