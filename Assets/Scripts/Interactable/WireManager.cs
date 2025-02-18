using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WireManager : MonoBehaviour 
{
    [Header("Wire")]
    [SerializeField] private List<GameObject> wireListOrder = new List<GameObject>();
    private int index = 0;
    [SerializeField] private Door wireDoor;

    public void CheckWireCutedOrder(GameObject wire)
    {
        if(wire != wireListOrder[index])
        {
            foreach(GameObject wireObj in wireListOrder)
            {
                wireObj.GetComponent<Wire>().enabled = false;
                wireObj.GetComponent<Wire>().WireEnabled = false;
                wireObj.GetComponent<Renderer>().material.color = Color.black;
            }
        }
        else
        {
            index++;
            if(index == wireListOrder.Count) wireDoor.OpenDoor();

        }
    }
}