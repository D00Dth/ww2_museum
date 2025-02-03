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
            
            LeanTween.rotateAround(gameObject,Vector3.forward,40,1f)
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
