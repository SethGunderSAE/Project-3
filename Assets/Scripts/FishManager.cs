using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    // CREATE LIST TO ADD FISH TO
    public List<FishData> fishList = new List<FishData>();
    public FishData currentFish;

    void Start()
    {
        //Loops for every fish in the list
        foreach (FishData fish in fishList) //note: Foreach is a loop that keeps going through everything in the list, FishData is the class, fish is a temporary value assigned (not predefined, could call it anything.)
        {
            // Random Length Generator for Each Fish
            fish.GenerateRandomLength();

        }
    }

    //FUNCTION TO USE IN INTERACT SCRIPT (Easier to write it here)
    public FishData CatchRandomFish()
    {
        // Generate a random index from the fishList
        int randomIndex = Random.Range(0, fishList.Count);

        // Grab a fish from the index
        FishData caughtFish = fishList[randomIndex];

        // Give the Fish to the player
        currentFish = caughtFish;

        // Show what the player has caught
        Debug.Log($"Caught a {caughtFish.fishName} with a base value of {caughtFish.baseValue}!");
        return caughtFish;

    }

   
}
