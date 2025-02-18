using UnityEngine;

public class Wire : MonoBehaviour, IInteractable
{
    [SerializeField] private Color wireColor;
    [SerializeField] private WireManager wireManager;
    private bool wireEnabled = true;
    public bool WireEnabled { set { wireEnabled = value ; } }


    public void OnHoverEnter()
    {
        if(wireEnabled) gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    public void OnHoverExit()
    {
        if(wireEnabled) gameObject.GetComponent<Renderer>().material.color = wireColor;
    }

    public bool Interact()
    {
        if(wireEnabled)
        {
            wireManager.CheckWireCutedOrder(gameObject);
            Destroy(gameObject);
        } 
        
        return true;
    }
}