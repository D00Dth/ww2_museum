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
        if(!computerCipher.isChecking)
        {
            switch(keyBoardKey)
            {
                case "Enter":
                    if(!computerCipher.isConnected)
                    {
                        computerCipher.CheckPassword();
                        computerCipher.isWritting = false;
                    }
                    else
                    {
                        // computerCipher.CheckAnswer();
                    }

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
        }
        return true;
    }
}