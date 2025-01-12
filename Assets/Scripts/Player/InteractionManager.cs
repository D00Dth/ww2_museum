using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private InputActionReference rotateCylinder;
    [SerializeField] private InputActionReference pickUpItem;

    [SerializeField] private float raySize = 1.0f;

    private GameObject hoveredObj;

    private void OnEnable()
    {
        rotateCylinder.action.Enable();
        pickUpItem.action.Enable();
    }

    private void OnDisable()
    {
        rotateCylinder.action.Disable();
        pickUpItem.action.Disable();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raySize))
        {
            CheckInteractable(hit);
            PickUpObject(hit);
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
        if(pickUpItem.action.triggered)
        {
            IInteractable item = hit.collider.gameObject.GetComponent<IInteractable>();

            if (item != null)
            {
                item.Interact();
            }
        }

    }
}

public interface IInteractable
{
    void OnHoverEnter();
    void OnHoverExit();
    void Interact();
}
