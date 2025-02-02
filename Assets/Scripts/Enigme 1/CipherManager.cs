using UnityEngine;
using System.Collections.Generic;

public class CipherManager : MonoBehaviour
{
    [SerializeField] private List<RotatingCipher> allCylinders = new List<RotatingCipher>();
    [SerializeField] private List<string> symbols = new List<string>();

    // void Update()
    // {
    //     foreach(string symbol in symbols)
    //     {
    //         foreach(RotatingCipher cylinder in allCylinders)
    //         {
    //             if(symbol != cylinder._symbols[cylinder.index])
    //             {
    //                 return;
    //             }
    //         }
    //     }
    //     print("trouve");
    // }
    
}