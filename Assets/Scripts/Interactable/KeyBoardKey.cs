using UnityEngine;

public class KeyBoardKey : MonoBehaviour, IInteractable
{
    [SerializeField] private string keyBoardKey;
    
    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    public void OnHoverExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    public bool Interact()
    {
        print(keyBoardKey);
        return true;
    }
}