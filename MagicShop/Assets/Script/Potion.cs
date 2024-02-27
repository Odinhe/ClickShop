using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public string potionName;
    public int ability;
    // Start is called before the first frame update
    public  void PotionCreate()
    {
        Debug.Log("You are using a " + potionName);
    }
}
