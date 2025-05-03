using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumValue : MonoBehaviour
{
    public int Value = 0;
    public int highScore = 0;
  

    public void HighScore()
    {
        
        if (Value >= highScore)
        {
            highScore = Value;
            
        }
    }
}
