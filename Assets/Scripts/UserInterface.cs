using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserInterface : MonoBehaviour
{
    public Image fishImageUI;
    public CollisionDetect detect;

    //Spawn fish in tank
    public GameObject fishVisualPrefab; 
    public Transform tankSpawnArea;


    void Update()
    {
        if (detect.alreadyHasFish)
        {
            fishImageUI.gameObject.SetActive(true);
        }
        else { fishImageUI.gameObject.SetActive(false); }
    }
}
