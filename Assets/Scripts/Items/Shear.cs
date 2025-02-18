using UnityEngine;

public class Shear : MonoBehaviour, IItem, IInteractable
{

    [SerializeField] public string shearName;
    [SerializeField] public Sprite shearIcon;
    [SerializeField] public GameObject shearModel;
    [SerializeField] private InventoryManager inventoryManager;



    public string itemName => shearName;
    public Sprite itemIcon => shearIcon;
    public GameObject itemModel => shearModel;

    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnHoverExit()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.black;
    }

    public bool Interact()
    {
        bool isAdded = inventoryManager.AddItemToInventory(this);
        if (isAdded)
        {
            gameObject.transform.SetParent(inventoryManager.showObject.transform);
            gameObject.transform.localPosition = new Vector3(0, 0 , 0.5f);
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 45);
            gameObject.transform.localScale = new Vector3(5, 5, 5);
            
            gameObject.SetActive(false);
        }
        return isAdded;
    }

    public void Use()
    {
        if(!inventoryManager.isDisplayed) 
        {
            inventoryManager.objectDisplay = gameObject;
            gameObject.SetActive(true);
            inventoryManager.isDisplayed = true;
        }
        else
        {
            gameObject.SetActive(false);
            inventoryManager.objectDisplay = null;
            inventoryManager.isDisplayed = false;
        }
    }
}