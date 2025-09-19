using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int m_Value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only trigger if the player touches the coin
        if (collision.CompareTag("Player"))
        {
            //find the ScoreManager and add points 
            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
            {
                if (scoreManager != null)
                {
                    scoreManager.AddScore(m_Value);
                }

                //Destory the coin so it will disapper after touching it
                Destroy(gameObject);
            }
        }
    }
}
