using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationByDistanceController : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObjects;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private bool containsBack;


    private enum DistanceType
    {
        Close, Far
    }

    [SerializeField] private DistanceType distanceType;

    private GameObject activeOnObject;


    // Start is called before the first frame update
    void Start()
    {
        if (distanceType == DistanceType.Close) activeOnObject = GetClosestDistanceObject();
        else activeOnObject = GetFarthestDistanceObject();

        activeOnObject.SetActive(true);

    }


    private GameObject GetClosestDistanceObject()
    {
        float closestDistance = 0;
        GameObject closestDistanceObject = null;
        Vector3 myPosition = transform.position;

        foreach (GameObject targetObj in this.targetObjects)
        {
            // Debug and Pause
            if (targetObj == null)
                Debug.LogError("Not Attach to " + this.gameObject.name.ToString());

            Vector3 targetPosition = targetObj.transform.position;
            if (targetPosition.z < myPosition.z && !containsBack) continue;
            else
            {
                float distanceToTarget = Vector3.Distance(myPosition, targetPosition);
                if (closestDistanceObject == null)
                {
                    closestDistanceObject = targetObj;
                    closestDistance = distanceToTarget;
                }
                else if (distanceToTarget < closestDistance && distanceToTarget >= minDistance && distanceToTarget <= maxDistance)
                {
                    closestDistanceObject = targetObj;
                    closestDistance = distanceToTarget;
                }
            }
        }

        return closestDistanceObject;
    }

    private GameObject GetFarthestDistanceObject()
    {
        float farthestDistance = 0;
        GameObject farthestDistanceObject = null;
        Vector3 myPosition = transform.position;

        foreach (GameObject targetObj in this.targetObjects)
        {
            // Debug and Pause
            if (targetObj == null)
                Debug.LogError("Not Attach to " + this.gameObject.name.ToString());
            
            Vector3 targetPosition = targetObj.transform.position;
            if (targetPosition.z < myPosition.z && !containsBack) continue;
            else
            {
                float distanceToTarget = Vector3.Distance(myPosition, targetPosition);
                if (farthestDistanceObject == null)
                {
                    farthestDistanceObject = targetObj;
                    farthestDistance = distanceToTarget;
                }
                else if (distanceToTarget > farthestDistance && distanceToTarget >= minDistance && distanceToTarget <= maxDistance)
                {
                    farthestDistanceObject = targetObj;
                    farthestDistance = distanceToTarget;
                }
            }
        }

        return farthestDistanceObject;
    }
}
