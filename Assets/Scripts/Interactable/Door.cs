using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Key specificKey;
    [SerializeField] private Color colorDoor;


    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void OnHoverExit()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = colorDoor;
    }

    public bool Interact()
    {
        for(int i = 0; i < inventoryManager.inventory.Count; i++)
        {
            if(inventoryManager.inventory[i] == specificKey)
            {   
                inventoryManager.inventory.RemoveAt(i);
                OpenDoor();
            }
        }
        return true;
    }

    public void OpenDoor()
    {
        float duration = 3.0f;
        float targetY = transform.position.y + 3;

        LeanTween.moveY(gameObject, targetY, duration).setEase(LeanTweenType.easeInOutQuad);
    }


}