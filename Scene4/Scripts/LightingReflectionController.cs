using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;

public class LightingReflectionController : MonoBehaviour
{   
    [SerializeField] private EnvironmentReflectionMode setReflectionMode;
    [SerializeField] private float reflectionIntensity = 1.0f;


    public enum EnvironmentReflectionMode
    {
        Skybox, Custom
    }


    void Start()
    {
        ChangeReflectionMode();
    }

    private void ChangeReflectionMode()
    {
        if(setReflectionMode == EnvironmentReflectionMode.Skybox) RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
        else RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
        
        RenderSettings.reflectionIntensity = reflectionIntensity;

    }
}
