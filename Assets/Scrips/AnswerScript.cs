using TMPro;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{

    public string m_AnswerText;
    public TextMeshProUGUI m_WorldText;

    private QuestionManager m_QuestionManagerRef;

    //Damage
    public int m_Damage = 1;

    //Correct sound
    [SerializeField] private AudioClip CorrectSoundClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_QuestionManagerRef = FindAnyObjectByType<QuestionManager>();

        if(m_WorldText != null)
        {
            m_WorldText.text = m_AnswerText;
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool correct = m_QuestionManagerRef.CheckAnswer(m_AnswerText);

            if (correct)
            {
                SoundFX.instance.PlaySoundFXClip(CorrectSoundClip, transform, 1f);
                Debug.Log("Player chose the correct answer");
                
            }
            else
            {
                //If the player gets it wrong take one damage
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(m_Damage);
                }

                Debug.Log("player chose the wrong answer");
            }
        }
    }

}
