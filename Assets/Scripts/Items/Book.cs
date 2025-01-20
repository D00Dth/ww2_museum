using UnityEngine;

public class Book : MonoBehaviour, IItem, IInteractable
{
    [SerializeField] public string bookName;
    [SerializeField] public Sprite bookIcon;
    [SerializeField] public GameObject bookModel;
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private Material material;


    public string itemName => bookName;
    public Sprite itemIcon => bookIcon;
    public GameObject itemModel => bookModel;

    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnHoverExit()
    {
        gameObject.GetComponent<Renderer>().material.color = material.color;
    }

    public bool Interact()
    {
        bool isAdded = inventoryManager.AddItemToInventory(this);
        if (isAdded)
        {
            Destroy(gameObject);
        }
        return isAdded;
    }

    public void Use()
    {
        if(!inventoryManager.isDisplayed) 
        {
            GameObject newBook = Instantiate(bookModel, inventoryManager.showObject.transform);
            inventoryManager.objectDisplay = newBook;

            newBook.transform.localScale = new Vector3(20, 20, 20);
            newBook.transform.localRotation = Quaternion.Euler(-25, 180, 0);
            newBook.transform.localPosition = new Vector3(0, 0, 1);

            inventoryManager.isDisplayed = true;
        }
        else
        {
            Destroy(inventoryManager.objectDisplay);
            inventoryManager.objectDisplay = null;
            inventoryManager.isDisplayed = false;
        }
    }

}