using UnityEngine;

public class Key : MonoBehaviour, IItem, IInteractable
{
    [SerializeField] public string keyName;
    [SerializeField] public Sprite keyIcon;
    [SerializeField] public GameObject keyModel;
    [SerializeField] private InventoryManager inventoryManager;


    public string itemName => keyName;
    public Sprite itemIcon => keyIcon;
    public GameObject itemModel => keyModel;


    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnHoverExit()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
    }

    public void Interact()
    {
        bool isAdded = inventoryManager.AddItemToInventory(this);
        if(isAdded) Destroy(gameObject);
    }


    
}