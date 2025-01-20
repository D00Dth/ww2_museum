using UnityEngine;

public class SecretButton : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject glass;

    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnHoverExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

    public bool Interact()
    {
        Destroy(glass);
        return true;
    }
}