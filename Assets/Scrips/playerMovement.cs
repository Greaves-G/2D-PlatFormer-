using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float m_speed = 10.0f;
    Rigidbody2D m_Rigidbody;
    public float m_JumpPower = 10.0f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xmove = Input.GetAxisRaw("Horizontal");
        float ymove = Input.GetAxisRaw("Vertical");

        m_Rigidbody.linearVelocity = new Vector2(xmove * m_speed, m_Rigidbody.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Rigidbody.AddForce(Vector2.up * m_JumpPower, ForceMode2D.Impulse); 
        }
    }
}
