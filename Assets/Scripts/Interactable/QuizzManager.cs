using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuizzManager : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Question> questions = new List<Question>();
    private List<string> answerLetter = new List<string> { "A.", "B.", "C.", "D." };
    private int index = 0;


    [SerializeField] private CursorManager cursorManager;
    private bool isRunning = false;
    private bool isQuizzOver = false;

    [SerializeField] private GameObject cameraContainer;

    [Header("Quizz UI")]
    [SerializeField] private TextMeshProUGUI questionUI;
    [SerializeField] private List<TextMeshProUGUI> listAnswersUI;
    [SerializeField] private TextMeshProUGUI textBetweenQuestion;
    public bool isDisplayingQuestion = false;


    [Header("Answer")]
    private AnswerButton? answerButton; // permet de rendre l'enum nullable
    private int score = 0;
    private bool isRightAnswer = false;


    public void OnHoverEnter()
    {
        if (isQuizzOver) return;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnHoverExit()
    {
        if (isQuizzOver) return;
        gameObject.GetComponent<Renderer>().material.color = Color.cyan;
    }

    public bool Interact()
    {
        if (isQuizzOver) return false;

        if(cursorManager.isSpecificView && !isRunning)
        {
            isRunning = true;
            StartCoroutine(DisplayNextQuestion());
        }

        if(cursorManager.isSpecificView && isRunning)
        {
            CheckAnswer();
        }
        return true;
    }


    public void ChangeView(Camera camera, GameObject player)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        
        player.SetActive(false);
        camera.transform.SetParent(cameraContainer.transform);

        camera.transform.localPosition = new Vector3(0f, -0.08f, 0.07f);
        camera.transform.localRotation = Quaternion.Euler(-90, 0, -180);

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


    public void CheckAnswer()
    {
        if(answerButton.HasValue)
        {
            var correctAnswer = questions[index].answers.Find( a =>  a.isCorrect);

            if(correctAnswer != null && correctAnswer.answerButton == answerButton)
            {
                isRightAnswer = true;
                score++;
            }
            else
            {
                isRightAnswer = false;
            }

            print(score);   


            Color answerColor = isRightAnswer ? Color.green : Color.red; 
            ChangeAnswerColor(answerButton, answerColor);

            index++;
            isRightAnswer = false;
            answerButton = null;

            if(index < questions.Count) StartCoroutine(DisplayNextQuestion());
            else DisplayEndQuizz();
        }
    }


    public IEnumerator DisplayNextQuestion()
    {
        isDisplayingQuestion = true;
        
        yield return new WaitForSeconds(1f);
        if(index != 0) DeletePreviousQuestion();  


        textBetweenQuestion.text = "Question " + (index + 1) + " !";
        textBetweenQuestion.gameObject.SetActive(true);        

        yield return new WaitForSeconds(1.5f); 
        textBetweenQuestion.gameObject.SetActive(false);
       
        questionUI.text = questions[index].question;
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < questions[index].answers.Count; i++)
        {
            listAnswersUI[i].text = answerLetter[i] + " " + questions[index].answers[i].answerText;
            yield return new WaitForSeconds(1f);
        }     
        isDisplayingQuestion = false; 
    }

    public void DeletePreviousQuestion()
    {
        questionUI.text = "";
        for(int i = 0; i < listAnswersUI.Count; i++)
        {
            listAnswersUI[i].text = "";
            listAnswersUI[i].color = Color.black;
        }
    }

    public void DisplayEndQuizz()
    {
        isQuizzOver = true;

        questionUI.gameObject.SetActive(false);
        foreach(TextMeshProUGUI answer in listAnswersUI)
        {
            answer.gameObject.SetActive(false);
        }

        string congratsText = "";
        
        switch(score)
        {
            case 0:
                congratsText = "BTW apprends à lire bouffon.";
                break;
            case 1:
                congratsText = "Tu n'as pas fait d'efforts ...";
                break;
            case 2:
                congratsText = "Peut mieux faire ...";
                break;
            case 3:
                congratsText = "Pas trop mal chef !";
                break;
            case 4:
                congratsText = "Bravo le premier de la classe";
                break;

        }

        textBetweenQuestion.text = "Félicitation vous avez fini le quizz ! Votre score est de " + score + ". " + congratsText;
        textBetweenQuestion.fontSize = 300f;
        textBetweenQuestion.gameObject.SetActive(true);

    }

    public void ChangeAnswerColor(AnswerButton? answerSelected, Color color)
    {
        foreach(TextMeshProUGUI answer in listAnswersUI)
        {
            answer.color = Color.black;
        }

        switch(answerSelected)
        {
            case AnswerButton.A:
                listAnswersUI[0].color = color;
                break;
            case AnswerButton.B:
                listAnswersUI[1].color = color;
                break;
            case AnswerButton.C: 
                listAnswersUI[2].color = color;
                break;
            case AnswerButton.D:
                listAnswersUI[3].color = color;
                break;
        }

        answerButton = answerSelected;
    }
}