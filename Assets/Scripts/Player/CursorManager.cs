using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private InputActionReference showCursor;
    [SerializeField] private GameObject cursorPointer;
    [SerializeField] public bool isLocked = false;
    public bool isSpecificView = false;

    public void OnEnable()
    {
        showCursor.action.Enable();
    }

    public void OnDisable()
    {
        showCursor.action.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(!isSpecificView)
        {
            if (showCursor.action.IsPressed())
            {
                UnlockCursor();
            }
            else
            {
                LockCursor();
            }
        }
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorPointer.SetActive(false);
        isLocked = false;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorPointer.SetActive(true);
        isLocked = true;
    }
}
