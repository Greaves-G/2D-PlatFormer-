using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public string[] m_Questions; //List of questions
    public string[] m_CorrectAnswers; //List of answers

    public TextMeshProUGUI m_QuestionText; // Current question that is being shown

    private int m_CurrentQuestion = 0; //question that we're on

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowQuestion();
        
    }

    // Update is called once per frame
    void Update()
    {
        
 
    }

    void ShowQuestion()
    {
        if(m_CurrentQuestion < m_Questions.Length)
        {
            m_QuestionText.text = m_Questions[m_CurrentQuestion];
        }
        else
        {
            m_QuestionText.text = "All questions complete!";
        }

    }

    public bool CheckAnswer(string chosenAnswer)
    {

        if(m_CurrentQuestion >= m_CorrectAnswers.Length)
        {
            return false;
        }

        //compare chosen answer with the right answer
        if (chosenAnswer == m_CorrectAnswers[m_CurrentQuestion])
        {
            Debug.Log("Correct");
            m_CurrentQuestion++;
            ShowQuestion();
            return true;
        }
        else
        {
            Debug.Log("Wrong!");
            return false;
        }

    }
}
