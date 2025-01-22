using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;


public class InteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera camera;
    [SerializeField] private InputActionReference rotateCylinder;
    [SerializeField] private InputActionReference interactWith;

    [SerializeField] private float raySize = 1.0f;
    [SerializeField] private CursorManager cursorManager;

    private GameObject hoveredObj;


    [Header("Picked Up Item UI")]
    [SerializeField] private GameObject pickedUpItem;
    [SerializeField] private TextMeshProUGUI nameUI;

    private void OnEnable()
    {
        rotateCylinder.action.Enable();
        interactWith.action.Enable();
    }

    private void OnDisable()
    {
        rotateCylinder.action.Disable();
        interactWith.action.Disable();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raySize))
        {
            CheckInteractable(hit);

            RotateCylinder(hit);
            PickUpObject(hit);
            OpenDoor(hit);
            StartQuizz(hit);

            LookRadio(hit);

            PressKeyboard(hit);
        }
        else
        {
            if (hoveredObj != null)
            {
                hoveredObj.GetComponent<IInteractable>().OnHoverExit();
                hoveredObj = null;
            }
        }
    }

    public void StartQuizz(RaycastHit hit)
    {
        if(interactWith.action.triggered && hit.collider.GetComponent<QuizzManager>() != null)
        {
            QuizzManager quizzManager = hit.collider.GetComponent<QuizzManager>();
            
            if(!cursorManager.isSpecificView) quizzManager.ChangeView(camera, player);
            else quizzManager.ResetView(camera, player);
        }
    }

    public void LookRadio(RaycastHit hit)
    {
        if(interactWith.action.triggered && hit.collider.GetComponent<RadioManager>() != null)
        {
            RadioManager radio = hit.collider.GetComponent<RadioManager>();

            if(!cursorManager.isSpecificView) radio.ChangeView(camera, player);
            else radio.ResetView(camera, player);
        }
    }

    public void CheckInteractable(RaycastHit hit)
    {
        IInteractable interactable = hit.collider.GetComponent<IInteractable>();

        if (interactable != null)
        {
            if (hoveredObj != hit.collider.gameObject && hoveredObj != null)
            {
                hoveredObj.GetComponent<IInteractable>().OnHoverExit();
            }

            hoveredObj = hit.collider.gameObject;
            interactable.OnHoverEnter();
        }
        else
        {
            if (hoveredObj != null)
            {
                hoveredObj.GetComponent<IInteractable>().OnHoverExit();
                hoveredObj = null;
            }
        }
    }

    public void RotateCylinder(RaycastHit hit)
    {
        if(rotateCylinder.action.triggered)
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void PickUpObject(RaycastHit hit)
    {
        if(interactWith.action.triggered && hit.collider.GetComponent<IItem>() != null)
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            IItem item = hit.collider.gameObject.GetComponent<IItem>();

            if (interactable != null && item != null)
            {
                bool isPickedUp = interactable.Interact();
                if(isPickedUp) StartCoroutine(DisplayPickedUpObjectName(item.itemName));
            }
        }
    }

    public void OpenDoor(RaycastHit hit)
    {
        if(interactWith.action.triggered && hit.collider.GetComponent<Door>() != null)
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                bool isDoorOpen = interactable.Interact();
            }
        }
    }

    public void PressKeyboard(RaycastHit hit)
    {
        if(interactWith.action.triggered && hit.collider.GetComponent<KeyBoardKey>() != null)
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if(interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public IEnumerator DisplayPickedUpObjectName(string itemName)
    {
        pickedUpItem.SetActive(true);
        nameUI.text = itemName;

        yield return new WaitForSeconds(2.0f);

        nameUI.text = "";
        pickedUpItem.SetActive(false);
    }

}

public interface IInteractable
{
    void OnHoverEnter();
    void OnHoverExit();
    bool Interact();
}
