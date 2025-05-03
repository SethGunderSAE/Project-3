using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    public bool makingDecision;
    public bool fishSpamPrevent = false;
    public FishData currentFish;
    public bool gameOver = false;

    public TMP_Text aquariumValueText;
    public TMP_Text fishText;
    public TMP_Text caughtFishText;
    public TMP_Text finalValueText;
    public TMP_Text fishStressText;
    public TMP_Text highScoreText;
    public TMP_Text fishSoldText;
    public TMP_Text tryAgainText;
    public GameObject gameOverPanel;

    public AudioSource sourceToPlayAt;
    public AudioClip fishCaughtSound;
    public AudioClip fishSoldSound;


    public CollisionDetect detect;
    public FishManager fishManager;
    public AquariumValue aquaValue;

    public Transform[] fishSpots;
    public GameObject fishPrefab;

    //Teleport
    public Vector2 insideAquarium;
    public Vector2 outsideAquarium;
    public Transform player;
    private bool[] spotUsed;

    //Win Condition
    public int fishSold = 0;

    void Start()
    {
        // Initializing a bool to track fish spots
        spotUsed = new bool[fishSpots.Length];
        //caughtfish text
        caughtFishText.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        EnterAquarium();
        ExitAquarium();
        FishingInteract();
        SellFish();
        
        if (Input.GetKeyDown(KeyCode.E) && gameOver)
        {
            ResetGame();

        }

        //For debugging
        if (Input.GetKeyDown(KeyCode.T))
        {
            player.position = outsideAquarium;
        }
    }



    void EnterAquarium()
    {
        // PLAYER ENTERS AQUARIUM
        if (Input.GetKeyDown(KeyCode.Space) && detect.canEnter)
        {
            player.position = insideAquarium;
            Debug.Log(" Player has Entered the Aquarium");

            //Teleports player to aquarium
            detect.canEnter = false;
        }

        // UI PROMPT TO ENTER AQUARIUM
        if (detect.canEnter)
        {
            
            
        }
    }
    void ExitAquarium()
    {
        // PLAYER ENTERS AQUARIUM
        if (Input.GetKeyDown(KeyCode.Space) && detect.canExit)
        {
            Debug.Log(" Player has Exited the Aquarium");

            //Teleports player to aquarium
            player.position = outsideAquarium;
            detect.canExit = false;
        }
    }
    void FishingInteract()
    {
        // PLAYER DECISION
        if (Input.GetKeyDown(KeyCode.Space) && detect.canFish && !detect.alreadyHasFish && !fishSpamPrevent)
        {

            ShowFishCaughtText();
            makingDecision = true;
            fishSpamPrevent = true;
        }

        // If the player already has a fish tell them they do
        else if (Input.GetKeyDown(KeyCode.Space) && detect.canFish && detect.alreadyHasFish)
        {
            Debug.Log("You already have a fish.");
            
        }

        // Q and E detection
        if (makingDecision)
        {
            if (detect.canFish)
            {
                //Player Keeps Fish
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CaughtFish();
                }

                //Player Throws Fish Away
                else if (Input.GetKeyDown(KeyCode.Q))
                {

                   ThrowFishAway();

                }
            }

            //If player walks away they keep the fish
            else if (!detect.canFish)
            {
                CaughtFish();
            }
            
        }
    }
    void SellFish()
    {
        // PLAYER SELLS
        if (Input.GetKeyDown(KeyCode.Space) && detect.canSell && !gameOver)
        {
            // Add the fish's base value to the total score
            int finalValue = currentFish.CalculateMaxValue();
            aquaValue.Value += finalValue;

            fishStressText.text = $"";
            fishText.text = $"";

            //Changing times sold by 1 every time
            fishSold++;
            fishSoldText.text = $"Fish Sold: {fishSold}/5";

            aquariumValueText.text = $"Aquarium Value: ${aquaValue.Value}";
            Debug.Log($"Sold {currentFish.fishName} for {currentFish.CalculateMaxValue()}.");
            currentFish.stress = 0;
            sourceToPlayAt.PlayOneShot(fishSoldSound);


            //bug.Log($"The Aquarium is now worth : {aquaValue.Value}!");

            //After fish is sold clear it from inventory
            detect.alreadyHasFish = false;

            if (fishSold >= 5)
            {
                detect.alreadyHasFish = false;
                FinishGame();
                
               
                return;
            }

            for (int i = 0; i < fishSpots.Length; i++)
            {
                if (!spotUsed[i])
                {
                    // Mark this spot as used
                    spotUsed[i] = true;

                    // Instantiate a fish at this spot
                    Instantiate(fishPrefab, fishSpots[i].position, Quaternion.identity);
                    return;
                }
            }
        }

    }
    void ShowFishCaughtText()
    {
        //spawn fish
        currentFish = fishManager.CatchRandomFish();
        caughtFishText.text = $"Fish Caught: {currentFish.fishName}\nEstimated Value: {currentFish.baseValue} \n\nPress 'E' to keep or 'Q' to throw away.";
        caughtFishText.gameObject.SetActive(true);
    }
  
    void FinishGame()
    {
        aquaValue.HighScore();
        Debug.Log("FINISH GAME CALLED!");
        gameOverPanel.SetActive(true);
        finalValueText.text = $"Final Aquarium Value: ${aquaValue.Value}";
        highScoreText.text = $"Highscore: ${aquaValue.highScore}";
        tryAgainText.text = "Press 'E' to play again";

        gameOver = true;
        

       
      
    }
    void CaughtFish()
    {
        

        fishStressText.text = $"Stress: {currentFish.stress}%";
        fishText.text = $"{currentFish.fishName}: ${currentFish.CalculateMaxValue()}";
        sourceToPlayAt.PlayOneShot(fishCaughtSound);

        caughtFishText.gameObject.SetActive(false);

        //Debug.Log("Player has kept the fish.");
        detect.alreadyHasFish = true;
        makingDecision = false;
        fishSpamPrevent = false;
    }
    void ThrowFishAway()
    {
        Debug.Log("You have thrown the fish back into the water.");
        detect.alreadyHasFish = false;
        makingDecision = false;
        fishSpamPrevent = false;
        caughtFishText.gameObject.SetActive(false);
    }
    void ResetGame()
    {
        gameOverPanel.SetActive(false);
        player.position = outsideAquarium;
        aquaValue.Value = 0;
        fishSold = 0;

        finalValueText.text = "";
        highScoreText.text = "";
        tryAgainText.text = "";
        fishSoldText.text = "Fish Sold: 0/5";
        aquariumValueText.text = "Aquarium Value: $0";

        gameOver = false;


    }

}
