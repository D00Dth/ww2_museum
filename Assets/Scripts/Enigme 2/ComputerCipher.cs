using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerCipher : MonoBehaviour
{
    private string password = "1"; // À remplacer par 18071940
    private bool isPasswordEnter = false;
    [SerializeField] private List<string> messagesList = new List<string>();
    [SerializeField] public string textWritten;
    public bool isWritting = false;

    [Header("Messages UI")]
    [SerializeField] private GameObject msgContainer;
    [SerializeField] private TextMeshProUGUI msgSend;
    private TextMeshProUGUI userMsg = null;
    [SerializeField] private TextMeshProUGUI successMsg;
    [SerializeField] private TextMeshProUGUI errorMsg;
    [SerializeField] private List<TextMeshProUGUI> allMsg = new List<TextMeshProUGUI>();

    void Update()
    {
        if (isWritting)
        {
            if (userMsg == null)
            {
                userMsg = Instantiate(msgSend, msgContainer.transform);
            }
            userMsg.text = "> " + textWritten;
        }
    }

    public void CheckPassword()
    {       
        isPasswordEnter = textWritten == password ? true : false;

        TextMeshProUGUI newComputerMsg = isPasswordEnter 
            ? Instantiate(successMsg, msgContainer.transform) 
            : Instantiate(errorMsg, msgContainer.transform);
    }

    public void DeleteMsg()
    {
        if (userMsg != null)
        {
            Destroy(userMsg.gameObject);
            userMsg = null;
        }
        textWritten = "";
    }
}
