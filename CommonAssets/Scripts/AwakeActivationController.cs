using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeActivationController : MonoBehaviour
{
    [SerializeField] private GameObject[] activeOnObjects;
    [SerializeField] private GameObject[] activeOffObjects;

    void Awake(){

        foreach(GameObject activeOnObject in this.activeOnObjects) {
            // Debug and Pause
            if (activeOnObject == null)
                Debug.LogError("Not Attach to " + this.gameObject.name.ToString());

            activeOnObject.SetActive(true);
        }

        foreach(GameObject activeOffObject in this.activeOffObjects) {
            // Debug and Pause
            if (activeOffObject == null)
                Debug.LogError("Not Attach to " + this.gameObject.name.ToString());

            activeOffObject.SetActive(false);
        }
    }
}
