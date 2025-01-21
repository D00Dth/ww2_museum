using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Quiz/Question")]
public class Question : ScriptableObject
{
    [SerializeField] public string question;
    [SerializeField] public List<Answer> answers;

    [Serializable]
    public class Answer
    {
        public string answerText;
        public bool isCorrect;
    }


}