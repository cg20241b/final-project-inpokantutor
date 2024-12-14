using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public WindZone windZone; // Drag your Wind Zone here in the Inspector
    public float transitionSpeed = 1.0f; // Speed of the wind transition
    private float targetWindStrength; // Target wind strength
    private float initialWindStrength; // Initial wind strength of the Wind Zone
    private bool isWindActive = true; // Tracks whether the wind is on or off

    void Start()
    {
        // Save the initial wind strength
        initialWindStrength = windZone.windMain;
        targetWindStrength = initialWindStrength;
    }

    void Update()
    {
        // Check if the Q key is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleWind();
        }

        // Gradually adjust the wind strength towards the target value
        windZone.windMain = Mathf.Lerp(windZone.windMain, targetWindStrength, Time.deltaTime * transitionSpeed);
    }

    public void ToggleWind()
    {
        // Toggle the wind state
        isWindActive = !isWindActive;

        // Set the target wind strength based on the new state
        targetWindStrength = isWindActive ? initialWindStrength : 0.0f;
    }
}
