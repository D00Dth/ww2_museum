using UnityEngine;
using System.Collections.Generic;

public class CipherManager : MonoBehaviour
{
    [SerializeField] private List<RotatingCipher> allCylinders = new List<RotatingCipher>();
    [SerializeField] private List<int> numbers = new List<int>();
    [SerializeField] private Key key;

    private bool isKeyAdded = false;

    [SerializeField] private InventoryManager inventoryManager;

    public void IsCombinationCorrect()
    {
        if(!isKeyAdded)
        {
            for (int i = 0; i < allCylinders.Count; i++)
            {
                RotatingCipher cylinder = allCylinders[i];

                if (cylinder.GetListNumber[cylinder.GetIndex] != numbers[i])
                {
                    return;
                }
            }
            
            inventoryManager.AddItemToInventory(key);
            isKeyAdded = true;
        }
    }
}
