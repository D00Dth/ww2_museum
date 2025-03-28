using UnityEngine;

public class OpenableObj : MonoBehaviour, IInteractable
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Key specificKey;


    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void OnHoverExit()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
    }

    public bool Interact()
    {
        for(int i = 0; i < inventoryManager.inventory.Count; i++)
        {
            if(inventoryManager.inventory[i] == specificKey)
            {   
                inventoryManager.inventory.RemoveAt(i);
                OpenObj();
            }
        }
        return true;
    }

    public void OpenObj()
    {
        float duration = 3.0f;
        float targetY = transform.position.y + 3;

        LeanTween.moveY(gameObject, targetY, duration).setEase(LeanTweenType.easeInOutQuad);
    }


}