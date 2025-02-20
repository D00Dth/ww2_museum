using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WireManager : MonoBehaviour 
{
    [SerializeField] private InventoryManager inventoryManager;
    [Header("Wire")]
    [SerializeField] private List<GameObject> wireListOrder = new List<GameObject>();
    private int index = 0;
    [SerializeField] private Door wireDoor;


    public bool CheckWireCutedOrder(GameObject wire)
    {
        bool hasShear = false;

        foreach (IItem item in inventoryManager.inventory)
        {
            if (item is Shear) 
            {
                hasShear = true;
                break; 
            }
        }

        if (!hasShear) return false;
        if (index >= wireListOrder.Count || wireListOrder[index] == null) return false;

        if (wire != wireListOrder[index])
        {
            
            foreach (GameObject wireObj in wireListOrder)
            {
                if (wireObj != null)
                {
                    Wire wireComponent = wireObj.GetComponent<Wire>();
                    if (wireComponent != null)
                    {
                        wireComponent.enabled = false;
                        wireComponent.WireEnabled = false;
                    }
                    wireObj.GetComponent<Renderer>().material.color = Color.black;
                }
            }

            return false;
        }
        else
        {
            index++;

            if (index == wireListOrder.Count)
            {
                wireDoor.OpenDoor();
            }
        }

        return true;
    }

}