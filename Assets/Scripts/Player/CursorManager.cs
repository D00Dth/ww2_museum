using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private InputActionReference showCursor;
    [SerializeField] private GameObject cursorPointer;
    [SerializeField] public bool isLocked = false;

    private void OnEnable()
    {
        showCursor.action.Enable();
    }

    private void OnDisable()
    {
        showCursor.action.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
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

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorPointer.SetActive(false);
        isLocked = false;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorPointer.SetActive(true);
        isLocked = true;
    }
}
