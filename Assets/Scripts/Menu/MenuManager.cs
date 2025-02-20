using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("UE Museum Menu")]
    [SerializeField] private GameObject EUMuseumMenu;
    [SerializeField] private GameObject interestPoints;
    private bool isEUMenuOpen = false;

    [SerializeField] private GameObject museumButtonContainer;
    private bool isDefaultSize = true;


    [Header("France")]
    [SerializeField] private Button franceButton;
    [SerializeField] private Sprite franceMuseumSprite;


    [Header("England")]
    [SerializeField] private Button englandButton;
    [SerializeField] private Sprite englandMuseumSprite;


    private Dictionary<Button, MuseumData> buttonToSpriteMap;
    [SerializeField] private Image museumImg;
    [SerializeField] private Button clickedButton;
    [SerializeField] private Button validateButton;

    public struct MuseumData
    {
        public Sprite museumSprite;
        public string museumName;

        public MuseumData(Sprite sprite, string name)
        {
            museumSprite = sprite;
            museumName = name;
        }
    }



    void Start()
    {
        buttonToSpriteMap = new Dictionary<Button, MuseumData>
        {
            { franceButton, new MuseumData(franceMuseumSprite, "France") },
            { englandButton, new MuseumData(englandMuseumSprite, "England") }
        };
    }


    public void ToggleUEMenu()
    {
        isEUMenuOpen = !isEUMenuOpen;
        EUMuseumMenu.SetActive(isEUMenuOpen);
        interestPoints.SetActive(!isEUMenuOpen);
    }

    public void ToggleEUMuseumButton(Button buttonClicked)
    {
        clickedButton = isDefaultSize ? buttonClicked : null;

        GridLayoutGroup gridLayout = museumButtonContainer.GetComponent<GridLayoutGroup>();

        Vector2 largeSize = new Vector2(1000, 100);
        Vector2 smallSize = new Vector2(400, 100);

        Vector2 sizeButton = isDefaultSize ? smallSize : largeSize;

        LeanTween.value(gameObject, gridLayout.cellSize, sizeButton, 1.0f)
            .setOnUpdate((Vector2 size) =>
            {
                gridLayout.cellSize = size;
            })
            .setEase(LeanTweenType.easeInOutSine);

        if(isDefaultSize) 
        {
            Sprite museumSelectedSprite = GetSpriteForButton(buttonClicked);
            StartCoroutine(ShowMuseumSelected(museumSelectedSprite));
            DisableButtonInteraction(buttonClicked);
        }
        else 
        {
            HideMuseumSelected();
            ResetButtonInteraction();
        }

        isDefaultSize = !isDefaultSize;
    }

    private Sprite GetSpriteForButton(Button buttonClicked)
    {
        if (buttonToSpriteMap.TryGetValue(buttonClicked, out MuseumData museumData)) return museumData.museumSprite;
        else return null;
    }

    public IEnumerator ShowMuseumSelected(Sprite museumSelectedSprite)
    {
        yield return new WaitForSeconds(1.0f);
        museumImg.gameObject.SetActive(true);
        validateButton.gameObject.SetActive(true);
        museumImg.sprite = museumSelectedSprite;
    }

    public void HideMuseumSelected()
    {
        validateButton.gameObject.SetActive(false);
        museumImg.gameObject.SetActive(false);
    }


    #region BUTTON EU MUSEUM
    public void DisableButtonInteraction(Button buttonClicked)
    {
        franceButton.interactable = buttonClicked == franceButton; 
        englandButton.interactable = buttonClicked == englandButton; 
    }

    public void ResetButtonInteraction()
    {
        franceButton.interactable = true;
        englandButton.interactable = true; 
    }
    #endregion

    public void LoadScene()
    {
        if(buttonToSpriteMap.TryGetValue(clickedButton, out MuseumData museumData))
        {
            SceneManager.LoadScene(museumData.museumName);
        }
    }

}
