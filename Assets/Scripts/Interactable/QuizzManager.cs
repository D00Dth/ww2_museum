using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuizzManager : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Question> questions = new List<Question>();
    [SerializeField] private CursorManager cursorManager;
    private bool isRunning = false;

    [Header("Quizz UI")]
    [SerializeField] private TextMeshProUGUI questionUI;
    [SerializeField] private List<TextMeshProUGUI> listAnswersUI;


    public void OnHoverEnter()
    {
        if(!cursorManager.isSpecificView)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void OnHoverExit()
    {
        if(!cursorManager.isSpecificView)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }    
    }

    public bool Interact()
    {
        if(cursorManager.isSpecificView && isRunning == false)
        {
            isRunning = true;
            StartQuizz();
        }
        return true;
    }

    public void StartQuizz()
    {
        foreach(Question question in questions)
        {
            StartCoroutine(DisplayQuestion(question));
        }
    }

    public IEnumerator DisplayQuestion(Question question)
    {
        questionUI.text = question.question;
        yield return new WaitForSeconds(2f);

        int index = 0;
        List<string> answerLetter = new List<string> { "A.", "B.", "C.", "D." };

        foreach(TextMeshProUGUI answer in listAnswersUI)
        {
            answer.text = answerLetter[index] + " " + question.answers[index].answerText;
            index ++;
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(15f);
    }

    public void ChangeView(Camera camera, GameObject player)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        
        player.SetActive(false);
        camera.transform.SetParent(gameObject.transform);

        camera.transform.localPosition = new Vector3(0f, -0.05f, 0.06f);
        camera.transform.localRotation = Quaternion.Euler(-90, -90, -90);

        cursorManager.isSpecificView = true;
        cursorManager.UnlockCursor();
    }

    public void ResetView(Camera camera, GameObject player)
    {
        isRunning = false;

        player.SetActive(true);
        camera.transform.SetParent(player.transform);

        camera.transform.localPosition = new Vector3(0f, 2.0f, 0f);

        cursorManager.isSpecificView = false;
        cursorManager.LockCursor();
    }
}