using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int m_Damage = 1;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(m_Damage);
            }

        }

    }
}