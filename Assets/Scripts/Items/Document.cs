using UnityEngine;

public class Document : MonoBehaviour, IItem
{
    [SerializeField] public string documentName;
    [SerializeField] public Sprite documentIcon;
    [SerializeField] public GameObject documentModel;
    [SerializeField] private InventoryManager inventoryManager;


    public string itemName => documentName;
    public Sprite itemIcon => documentIcon;
    public GameObject itemModel => documentModel;

    public void Use()
    {

        if(!inventoryManager.isDisplayed) 
        {
            GameObject newDocument = Instantiate(documentModel, inventoryManager.showObject.transform);
            inventoryManager.objectDisplay = newDocument;

            gameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.075f);
            gameObject.transform.localRotation = Quaternion.Euler(60, 180, 0);
            gameObject.transform.localPosition = new Vector3(0, 0, 1);

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