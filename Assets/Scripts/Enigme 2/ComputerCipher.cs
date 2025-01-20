using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComputerCipher : MonoBehaviour
{
    private string password = "1";
    [SerializeField] private List<string> messagesList = new List<string>();
    [SerializeField] public string textWritten;
    private int index = 0;
    public bool isWritting = false;
    public bool isConnected = false;
    public bool isChecking = false;




    [Header("Messages UI")]
    [SerializeField] private GameObject msgContainer;
    [SerializeField] private TextMeshProUGUI msgSend;
    private TextMeshProUGUI message = null;
    [SerializeField] private List<TextMeshProUGUI> allMsg = new List<TextMeshProUGUI>();


    void Start()
    {
        message = Instantiate(msgSend, msgContainer.transform);
        message.text = "> Enter the password :";
        
        ManagePasswordMsg(message);
        message = null;
    }

    void Update()
    {
        if(isWritting)
        {
            string textMsg = "> " + textWritten;
            GenerateMsg(msgSend, textMsg); 
        }
    }


    public void GenerateMsg(TextMeshProUGUI msg, string textMsg)
    {
        if (message == null)
        {
            message = Instantiate(msg, msgContainer.transform);
            ManagePasswordMsg(message);
        }

        message.text = textMsg;

    }

    public void CheckPassword()
    {
        isChecking = true;
        if (message != null)
        {
            TextMeshProUGUI newComputerMsg = Instantiate(msgSend, msgContainer.transform);
            StartCoroutine(VerifyPasswordSequence(newComputerMsg));
        }
    }

    private IEnumerator VerifyPasswordSequence(TextMeshProUGUI displayMsg)
    {
        yield return StartCoroutine(TypeText(displayMsg, "> Checking ", 0.1f));
        yield return StartCoroutine(AnimateVerification(null, displayMsg));

        isConnected = textWritten == password;

        string resultText = isConnected ? "> Connected" : "> Error";
        yield return StartCoroutine(TypeText(displayMsg, resultText, 0.1f));

        ManagePasswordMsg(displayMsg);

        textWritten = "";
        message = null;
        isChecking = false;

        if (isConnected)
            StartCoroutine(ClearMessages());
    }


    public IEnumerator TypeText(TextMeshProUGUI textComponent, string fullText, float delay)
    {
        textComponent.text = "";
        foreach (char letter in fullText)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }


    public IEnumerator ClearMessages()
    {
        yield return new WaitForSeconds(2.0f);
        foreach(TextMeshProUGUI msg in allMsg)
        {
            Destroy(msg.gameObject);
        }
        allMsg.Clear();
        StartCoroutine(DisplayComputerMsg());
    }

    public IEnumerator DisplayComputerMsg()
    {
        foreach(string msg in messagesList)
        {
            TextMeshProUGUI newComputerMsg = Instantiate(msgSend, msgContainer.transform);
            yield return StartCoroutine(TypeText(newComputerMsg, msg, 0.1f));
            ManagePasswordMsg(newComputerMsg);
        }
    }


    public IEnumerator AnimateVerification(System.Action onComplete, TextMeshProUGUI displayMsg)
    {
        string baseText = displayMsg.text;

        for (int j = 0; j < 3; j++)
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
        if (message != null)
        {
            Destroy(message.gameObject);
            allMsg.Remove(message);
            message = null;
        }
        textWritten = "";
    }
}
