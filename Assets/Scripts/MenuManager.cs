using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [Header("UE Museum Menu")]
    [SerializeField] private GameObject EUMuseumMenu;
    private bool isEUMenuOpen = false;

    [SerializeField] private GameObject museumButtonContainer;
    private bool isDefaultSize = true;


    [SerializeField] private Button franceButton;
    [SerializeField] private Button englandButton;
    [SerializeField] private Button germanyButton;


    public void ToggleUEMenu()
    {
        isEUMenuOpen = !isEUMenuOpen;
        EUMuseumMenu.SetActive(isEUMenuOpen);
    }

    public void ToggleEUMuseumButton(Button buttonClicked)
    {
        GridLayoutGroup gridLayout = museumButtonContainer.GetComponent<GridLayoutGroup>();

        Vector2 largeSize = new Vector2(500, 50);
        Vector2 smallSize = new Vector2(150, 50);

        Vector2 sizeButton = isDefaultSize ? smallSize : largeSize;

        LeanTween.value(gameObject, gridLayout.cellSize, sizeButton, 1f)
            .setOnUpdate((Vector2 size) =>
            {
                gridLayout.cellSize = size;
            })
            .setEase(LeanTweenType.easeInOutSine);

        if(isDefaultSize) DisableButtonInteraction(buttonClicked);
        else ResetButtonInteraction();

        isDefaultSize = !isDefaultSize;
    }

    #region BUTTON EU MUSEUM
    public void DisableButtonInteraction(Button buttonClicked)
    {
        franceButton.interactable = buttonClicked == franceButton; 
        englandButton.interactable = buttonClicked == englandButton; 
        germanyButton.interactable = buttonClicked == germanyButton; 
    }

    public void ResetButtonInteraction()
    {
        franceButton.interactable = true;
        englandButton.interactable = true; 
        germanyButton.interactable = true; 
    }

    #endregion
}
