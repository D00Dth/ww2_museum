using UnityEngine;

public class QuizzButton : MonoBehaviour, IInteractable 
{
    [SerializeField] private QuizzManager quizzManager;
    [SerializeField] private CursorManager cursorManager;

    [SerializeField] private Material material;
    [SerializeField] private AnswerButton answerButton;

    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    public void OnHoverExit()
    {
        gameObject.GetComponent<Renderer>().material.color = material.color;
    }

    public bool Interact()
    {
        if(!quizzManager.isDisplayingQuestion && cursorManager.isSpecificView)
        {
            quizzManager.ChangeAnswerColor(answerButton, Color.blue);
        }
        return true;
    }
}

public enum AnswerButton {A, B, C, D}