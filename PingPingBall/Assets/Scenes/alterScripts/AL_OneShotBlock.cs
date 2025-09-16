using UnityEngine;

public class AL_OneShotBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            GameManager.instance.addCollisionScore(EntityType.BLOCK_ONE_SHOT);
            GameManager.instance.addBreakScore(EntityType.BLOCK_ONE_SHOT);
            // GameManager 필요
            Destroy(gameObject);
        }
    }
}