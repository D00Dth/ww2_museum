using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;

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

    void Start()
    {
        userMsg = Instantiate(msgSend, msgContainer.transform);
        userMsg.text = "> Enter the password :";
        
        ManagePasswordMsg(userMsg);
        userMsg = null;
    }

    void Update()
    {
        if (isWritting)
        {
            if (userMsg == null)
            {
                userMsg = Instantiate(msgSend, msgContainer.transform);
                ManagePasswordMsg(userMsg);
            }

            userMsg.text = "> " + textWritten;
        }
    }

    public void CheckPassword()
    {
        if (userMsg != null)
        {
            TextMeshProUGUI newComputerMsg = Instantiate(successMsg, msgContainer.transform);
            newComputerMsg.text = "> Checking ";

            StartCoroutine(AnimateVerification(() =>
            {
                isPasswordEnter = textWritten == password;

                newComputerMsg.text = isPasswordEnter ? successMsg.text : errorMsg.text;

                ManagePasswordMsg(newComputerMsg);

                textWritten = "";
                userMsg = null;
            }, newComputerMsg));
        }
    }

    private IEnumerator AnimateVerification(System.Action onComplete, TextMeshProUGUI displayMsg)
    {
        string baseText = displayMsg.text;

        for (int j = 0; j < 2; j++) 
        {
            for (int i = 0; i < 3; i++)
            {
                displayMsg.text += ".";
                yield return new WaitForSeconds(0.5f);
            }
            displayMsg.text = baseText;
        }

        onComplete?.Invoke();
    }


    public void ManagePasswordMsg(TextMeshProUGUI newMsg)
    {
        allMsg.Add(newMsg);

        if (allMsg.Count > 6)
        {
            Destroy(allMsg[0].gameObject);
            allMsg.RemoveAt(0);
        }
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
