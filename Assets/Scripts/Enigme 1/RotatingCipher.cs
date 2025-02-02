using UnityEngine;
using System.Collections.Generic;

public class RotatingCipher : MonoBehaviour, IInteractable
{
    [SerializeField] private List<int> listNumber = new List<int>();
    [SerializeField] private int index = 1;
    private bool isRotating = false;

    public int GetIndex { get { return index; } }
    public bool GetIsRotating { get { return isRotating; } }


    public bool Interact()
    {
        if (!isRotating)
        {
            isRotating = true;

            // Calculer la rotation cible en quaternion
            Quaternion currentRotation = transform.localRotation;
            Quaternion targetRotation = currentRotation * Quaternion.Euler(-40f, 0f, 0f); // Ajouter 40° sur X

            // Animer vers la rotation cible
            LeanTween.rotate(gameObject, targetRotation.eulerAngles, 1f)
                .setEase(LeanTweenType.easeInOutQuad)
                .setOnComplete(() =>
                {
                    isRotating = false;
                });

            // Mettre à jour l'index
            index = index == 9 ? 1 : index + 1;
        }
        return true;
    }



    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnHoverExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }
}
