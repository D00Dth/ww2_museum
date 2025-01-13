using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;


public class InteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private InputActionReference rotateCylinder;
    [SerializeField] private InputActionReference interactWith;

    [SerializeField] private float raySize = 1.0f;

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
            PickUpObject(hit);
            OpenDoor(hit);
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

            if (rotateCylinder.action.triggered)
            {
                interactable.Interact();
            }
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
