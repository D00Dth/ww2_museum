using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class InventoryManager : MonoBehaviour 
{
    [SerializeField] public List<IItem> inventory = new List<IItem>();


    [Header("Display Menu")]
    [SerializeField] private InputActionReference toggleMenu;
    [SerializeField] private GameObject inventoryUI;
    private bool isMenuOpen = false;
    private bool isAnimating = false;

    [Header("Picked Up Item UI")]
    [SerializeField] private GameObject pickedUpItem;
    [SerializeField] private TextMeshProUGUI nameUI;


    [Header("Inventory UI")]
    [SerializeField] private GameObject itemContainer;
    [SerializeField] private List<Button> objectButtonList;


    public void OnEnable()
    {
        toggleMenu.action.Enable();
    }

    public void OnDisable()
    {
        toggleMenu.action.Disable();
    }

    void Start()
    {
        inventory.Clear();
    }

    void Update()
    {
        if (toggleMenu.action.triggered)
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if(isAnimating) return;

        isAnimating = true;
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen)
        {
            inventoryUI.SetActive(true);
            inventoryUI.transform.localScale = Vector3.zero;
            LeanTween.scale(inventoryUI, Vector3.one, 0.5f)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setOnComplete(() =>
                    {
                        isAnimating = false;
                        isMenuOpen = true;
                    });
            
            for(int i = 0; i < inventory.Count; i++)
            {   
                Image buttonImg = objectButtonList[i].GetComponentInChildren<Image>();
                buttonImg.gameObject.SetActive(true);

                buttonImg.sprite = inventory[i].itemIcon;
            }

        }
        else
        {
            LeanTween.scale(inventoryUI, Vector3.zero, 0.5f)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setOnComplete(() => 
                    {
                        inventoryUI.SetActive(false);
                        isAnimating = false;
                        isMenuOpen = false;
                    });
        }
    }

    public bool AddItemToInventory(IItem item)
    {
        if(inventory.Count < 3) 
        {
            inventory.Add(item);
            return true;
        }
        else 
        {
            StartCoroutine(DisplayInventoryMsg());
            return false;
        }
    }

    public IEnumerator DisplayInventoryMsg()
    {
        pickedUpItem.SetActive(true);
        nameUI.text = "The inventory is full";

        yield return new WaitForSeconds(2.0f);
        pickedUpItem.SetActive(false);
    }

}

public interface IItem
{
    string itemName { get; }
    GameObject itemModel { get; }
    Sprite itemIcon { get; }
}