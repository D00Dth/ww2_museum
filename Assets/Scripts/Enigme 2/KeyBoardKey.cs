using UnityEngine;

public class KeyBoardKey : MonoBehaviour, IInteractable
{
    [SerializeField] private string keyBoardKey;
    [SerializeField] private ComputerCipher computerCipher;
    

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
        switch(keyBoardKey)
        {
            case "Enter":
                computerCipher.CheckPassword();
                computerCipher.isWritting = false;
                break;
            case "Delete":
                computerCipher.DeleteMsg();
                computerCipher.isWritting = false;
                break;
            default:
                computerCipher.textWritten += keyBoardKey;
                computerCipher.isWritting = true;
                break;
        }

        return true;
    }
}