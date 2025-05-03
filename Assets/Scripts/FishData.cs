using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//DATA BLUEPRINT FOR ALL FISH
[System.Serializable]
public class FishData
{
    public string fishName;
    public int baseValue;
    public float minLength;
    public float maxLength;
    public float generatedLength;
    public int stress = 0;

    //Constructor for name size and value
    public FishData(string name, float minLength, float maxLength, int value)
    {
        fishName = name;
        this.minLength = minLength;
        this.maxLength = maxLength;
        baseValue = value;

    }

    // PUBLIC FUNCTION THAT GENERATES THE LENGTH OF THE FISH
    public void GenerateRandomLength()
    {
        generatedLength = Random.Range(minLength, maxLength);
    }

    //CALCULATOR FOR DETERMINING THE VALUE OF THE FISH
    public int CalculateMaxValue()
    {
        // Value increases with length (relative to max length)
        float lengthMultiplier = generatedLength / maxLength;

        // Value decreases with stress (1% loss per stress point)
        float stressMultiplier = 1f - (stress / 100f);

        // Combine both effects
        float finalMultiplier = lengthMultiplier * stressMultiplier;

        // Calculate and clamp final value
        int finalValue = Mathf.Max(1, Mathf.RoundToInt(baseValue * finalMultiplier));

        return finalValue;
    }
}
