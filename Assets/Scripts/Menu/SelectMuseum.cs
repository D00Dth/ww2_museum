using UnityEngine;

public class SelectMuseum : MonoBehaviour
{

    [SerializeField] private GameObject menuUEMuseum;
    private bool isUEMenuOpen = false;


    public void ToggleEuropeanMuseumSelector()
    {
        isUEMenuOpen = !isUEMenuOpen;
        menuUEMuseum.SetActive(isUEMenuOpen);
    }
}
