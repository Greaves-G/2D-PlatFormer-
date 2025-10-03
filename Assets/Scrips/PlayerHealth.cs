using JetBrains.Annotations;
using System.Collections;
using TMPro;
using Unity.XR.Oculus.Input;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Variables for players health
    public int m_Maxhealth = 3;
    private int m_CurrentHealth;

    //Respawn funtionality
    public Vector3 m_RespawnPosition;
    public float m_RespawnDelay = 0.6f;
    public TextMeshProUGUI m_HealthText;

    Rigidbody2D m_RB;
    playerMovement m_PlayerMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_CurrentHealth = m_Maxhealth;
        m_RB = GetComponent<Rigidbody2D>();
        m_PlayerMovement = GetComponent<playerMovement>();
        m_RespawnPosition = transform.position;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int amount)
    {
        // reduces health
        m_CurrentHealth -= amount;
        // Update Player UI
        UpdateHealthUI();
        // if health is below 0
        if (m_CurrentHealth <= 0)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    public void Heal(int amount)
    {
        // gives hp
        m_CurrentHealth += amount;
        if(m_CurrentHealth > m_Maxhealth)
        {
            m_CurrentHealth = m_Maxhealth;
        }
        // update UI after healing
        UpdateHealthUI();
    }

    public void SetRespawnPosition(Vector3 pos)
    {
       // setting the respawn position that is passed in
        m_RespawnPosition = pos;
    }

    IEnumerator RespawnCoroutine()
    {
        if (m_PlayerMovement != null)
        {
            m_PlayerMovement.enabled = false;
        }
        if(m_RB != null)
        {
            m_RB.linearVelocity = Vector2.zero;
        }

        yield return new WaitForSeconds(m_RespawnDelay);
        
        transform.position = m_RespawnPosition;
        m_CurrentHealth = m_Maxhealth;
        UpdateHealthUI();

        if(m_PlayerMovement != null)
        {
            m_PlayerMovement.enabled = true;
        }
    }

    void UpdateHealthUI()
    {

        m_HealthText.text = "health" + m_CurrentHealth;
    }
}
