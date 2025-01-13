using UnityEngine;
using System.Collections.Generic;

public class RotatingCipher : MonoBehaviour, IInteractable
{
    [SerializeField] public List<string> _symbols = new List<string>();
    [SerializeField] public int index;
    private bool isRotating = false;


    void Start()
    {
        index = Random.Range(0, _symbols.Count);
    }


    public bool Interact()
    {
        if(!isRotating)
        {
            isRotating = true;
            LeanTween.rotate(gameObject, new Vector3(0f, 0f, 60f) + transform.localEulerAngles, 1f)
                .setEase(LeanTweenType.easeInOutQuad)
                .setOnComplete(() =>
                {
                    isRotating = false;
                });

            index = index == _symbols.Count - 1 ? 0 : index + 1;
            print(gameObject.name + " a le symbole " + _symbols[index]);
        }
        return true;

    }


    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnHoverExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
