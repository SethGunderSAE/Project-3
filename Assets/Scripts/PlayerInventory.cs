using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Boolean flag to check if player has a fish
    public bool hasFish = false;

    // Variable to store the fish data when caught
    public FishData currentFish;

    // Reference to FishManager to interact with it
    public FishManager fishManager;
}
