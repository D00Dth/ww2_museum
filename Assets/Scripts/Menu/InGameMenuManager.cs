using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;


public class InGameMenuManager : MonoBehaviour
{

    [SerializeField] private InputActionReference openMenu;
    [SerializeField] private PlayerManager playerManager;
    private bool isMenuOpen = false;

    [SerializeField] private GameObject menuContainer;
    [SerializeField] private GameObject leaveButton;

    void Update()
    {
        if(openMenu.action.triggered)
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;  

        playerManager.enabled = isMenuOpen ? false : true;   
        menuContainer.SetActive(isMenuOpen);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }




}