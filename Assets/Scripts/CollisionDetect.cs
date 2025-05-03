using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    public Interact interact;
    public int stressPenalty = 25;

    //JETTY BOOLS
    bool isTouchingFishSpot = false;
    public bool alreadyHasFish = false;
    public bool canFish;

    //OUTSIDE AQUARIUM BOOLS
    bool isAtDoor = false;
    public bool canEnter;

    //INSIDE AQUARIUM BOOLS
    bool isAtExit = false;
    public bool canExit;

    //COUNTER BOOLS
    bool isAtCounter;
    public bool canSell;

    //----------------------------------------------

    //==ENTER TRIGGER==//
    void OnTriggerEnter2D(Collider2D other)
    {
        //Detections in practice
        Jetty();
        AquaEnter();
        AquaExit();
        Counter();
        


        //Collision Detections
        void Jetty()
        {
            //If Player is touching fishing spot enable the touching spot flag
            if (other.CompareTag("Jetty"))
            {
                Debug.Log(" Player entered fishing spot.");
                isTouchingFishSpot = true;
            }
            //If Player doesnt already have a fish they can fish
            if (isTouchingFishSpot && !alreadyHasFish)
            {
                canFish = true;
            }
            else
            {
                canFish = false;
            }
        }
        void AquaEnter()
        {
            if (other.CompareTag("AquaEnter"))
            {
                Debug.Log(" Player is at the Door.");
                isAtDoor = true;
       
            }

            // If player isnt at door keep flag false
            if (isAtDoor)
            {
                canEnter = true;
            }
            else
            {
                canEnter = false;
            }
        }
        void AquaExit()
        {
            if (other.CompareTag("AquaExit"))
            {
                Debug.Log(" Player is at the Exit.");
                isAtExit = true;
             
            }
            // Same but for exit
            if (isAtExit)
            {
                canExit = true;
            }
            else
            {
                canExit = false;
            }
        }
        void Counter()
        {
            if (other.CompareTag("Counter"))
            {
                Debug.Log(" Player is at Counter.");
                isAtCounter = true;
            }
            //IF PLAYER IS AT COUNTER AND HAS A FISH PLAYER CAN SELL WOWOWOWW
            if (isAtCounter && alreadyHasFish)
            {
                canSell = true;
            }
            else
            {
                canSell = false;
            }
        }
      
    }
    //==EXIT TRIGGER==//
    void OnTriggerExit2D(Collider2D other)
    {
        //Detections in practice
        JettyLeave();
        AquaEnterLeave();
        AquaExitLeave();
        CounterLeave();
       

        //Collision Leave Detections
        void JettyLeave()
        {
            // AT FISHING SPOT FLAG
            if (other.CompareTag("Jetty"))
            {
                Debug.Log("Player left the fishing spot.");
                isTouchingFishSpot = false;
                canFish = false;  // Ensure canFish is reset
            }
        }
        void AquaEnterLeave()
        {
            // AT AQUARIUM DOOR FLAG
            if (other.CompareTag("AquaEnter"))
            {
                Debug.Log("Player is not at the Door.");
                isAtDoor = false;
                canEnter = false;  // Ensure canEnter is reset
            }
        }
        void AquaExitLeave()
        {

            // INNER AQUARIUM DOOR FLAG
            if (other.CompareTag("AquaExit"))
            {
                Debug.Log("Player is not at the Exit.");
                isAtExit = false;
                canExit = false;  // Ensure canExit is reset
            }
        }
        void CounterLeave()
        {

            // COUNTER FLAG
            if (other.CompareTag("Counter"))
            {
                Debug.Log("Player is not at the Counter.");
                isAtCounter = false;
                canSell = false;  // Ensure canSell is reset
            }
        }
    }

    //==STONECOLLISION==//
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            if (alreadyHasFish && interact.currentFish != null)
            {
                // Increase fish stress
                interact.currentFish.stress += stressPenalty;
                if (interact.currentFish.stress > 100)
                    interact.currentFish.stress = 100;

                // Update UI
                if (interact.fishStressText != null)
                    interact.fishStressText.text = $"Stress: {interact.currentFish.stress}%";

                if (interact.fishText != null)
                {
                    int updatedValue = interact.currentFish.CalculateMaxValue();
                    interact.fishText.text = $"{interact.currentFish.fishName}: ${updatedValue}";
                }
            }
        }
    }






}
