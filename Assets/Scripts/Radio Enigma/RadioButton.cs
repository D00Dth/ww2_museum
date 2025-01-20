using UnityEngine;

public class RadioButton : MonoBehaviour, IInteractable
{
    [SerializeField] private string buttonName;
    [SerializeField] private Material material;
    [SerializeField] private CursorManager cursorManager;
    [SerializeField] private RadioManager radioManager;


    private bool isIncreasing = true;
    private float actualStep = 5;
    private float maxSteps = 9;



    public void OnHoverEnter()
    {
        if(cursorManager.isSpecificView)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void OnHoverExit()
    {
        if(cursorManager.isSpecificView)
        {
            gameObject.GetComponentInChildren<Renderer>().material.color = material.color;
        }
    }

    public bool Interact()
    {
        if(cursorManager.isSpecificView)
        {
            
            if(isIncreasing)
            {
                if(actualStep == maxSteps) 
                {
                    isIncreasing = false;
                }
            }
            else
            {
                if(actualStep == 1)
                {
                    isIncreasing = true;
                }
            }

            actualStep = isIncreasing ? actualStep + 1 : actualStep -1;
            gameObject.transform.localRotation *= isIncreasing ? Quaternion.Euler(0, 40, 0) : Quaternion.Euler(0, -40, 0);
            

            float value = actualStep / maxSteps;
            radioManager.UpdateButtonValue(buttonName, value);
        }

        return true;
    }
}