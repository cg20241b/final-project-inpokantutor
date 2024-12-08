using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField, Range(0,24)] private float TimeOfDay;
    
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;
    
    private bool isTransitioning = false;
    private Coroutine currentTransition = null;
    private int lastFlooredTime = -1;

    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        if(isTransitioning) yield break;
        
        isTransitioning = true;
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        RenderSettings.skybox.SetFloat("_Blend", 0);

        float t = 0;
        while(t < time)
        {
            t += Time.deltaTime;
            Debug.Log("t: " + t);
            float blend = Mathf.Clamp01(t / time);
            RenderSettings.skybox.SetFloat("_Blend", blend);
            yield return null;
        }

        // Ensure we end at exactly 1
        RenderSettings.skybox.SetFloat("_Blend", 1);
        RenderSettings.skybox.SetTexture("_Texture1", b);
        isTransitioning = false;
        currentTransition = null;
    }

    void Update()
    {
        if(Application.isPlaying)
        {
            float currentTime = GlobalTimeSystem.TimeOfDay;
            int flooredTime = Mathf.FloorToInt(currentTime);
            
            // Only trigger transition if time has changed
            if(flooredTime != lastFlooredTime)
            {
                // Stop any existing transition
                if(currentTransition != null)
                {
                    StopCoroutine(currentTransition);
                    isTransitioning = false;
                }

                if(flooredTime == 0) {
                    currentTransition = StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 1f));
                } else if(flooredTime == 4) {
                    currentTransition = StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 1f));
                } else if(flooredTime == 14) {
                    currentTransition = StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 1f));
                } else if(flooredTime == 18) {
                    currentTransition = StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 1f));
                }
                
                lastFlooredTime = flooredTime;
            }
        }
    }
}