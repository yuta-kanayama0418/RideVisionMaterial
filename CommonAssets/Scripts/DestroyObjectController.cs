using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectController : MonoBehaviour
{
 
    [SerializeField] private GameObject[] destroyObjects;
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private float waitTime = 1f;

    private void Start() {
        
        StartCoroutine(DestroyObjects());

    }

    private IEnumerator DestroyObjects() {
        yield return new WaitForSeconds(delayTime);

        foreach(GameObject destroyObject in destroyObjects){

            // Debug and Pause
            if (destroyObject == null) Debug.LogError("Not Attach to " + this.gameObject.name.ToString());

            Destroy(destroyObject);

            yield return new WaitForSeconds(waitTime);
        }
    }
}
