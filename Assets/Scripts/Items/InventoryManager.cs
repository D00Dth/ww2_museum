using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour 
{
    [SerializeField] public List<IItem> inventory = new List<IItem>();


    [Header("Display Menu")]
    [SerializeField] private InputActionReference toggleMenu;
    [SerializeField] private GameObject menuUI;
    private bool isMenuOpen = false;

    public void OnEnable()
    {
        toggleMenu.action.Enable();
    }

    public void OnDisable()
    {
        toggleMenu.action.Disable();
    }

    private void Update()
    {
        if (toggleMenu.action.triggered)
        {
            Debug.Log("Action déclenchée par le clavier (désactivée ici pour éviter un conflit).");
        }
    }

    public void ToggleInventory()
    {
        isMenuOpen = !isMenuOpen;
        menuUI.SetActive(isMenuOpen);

        Debug.Log("Inventaire ouvert : " + isMenuOpen);
    }
}

public interface IItem
{
    string itemName { get; }
    GameObject model { get; }
    Sprite icon { get; }

    void Use();
}