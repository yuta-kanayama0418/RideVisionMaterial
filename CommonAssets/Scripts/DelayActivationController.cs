using System.Collections;
using UnityEngine;

public class DelayActivationController : MonoBehaviour
{
    [SerializeField] private GameObject[] activeOnObjs;
    [SerializeField] private GameObject[] activeOffObjs;
    [SerializeField] private ExecutableBase[] executables;
    [SerializeField] private float delayTime;

    void OnEnable()
    {
        
        StartCoroutine(ActiveOnObjects());
        StartCoroutine(ActiveOffObjects());
        StartCoroutine(ExecExecutables());
    }

    private IEnumerator ActiveOnObjects() {
        yield return new WaitForSeconds(delayTime);

        foreach(GameObject activeOnObj in activeOnObjs){

            // Debug and Pause
            if (activeOnObj == null)
                Debug.LogError("Not Attach to " + this.gameObject.name.ToString());
            activeOnObj.SetActive(true);
        }
        
    }

    private IEnumerator ActiveOffObjects() {
        yield return new WaitForSeconds(delayTime);
        
        foreach(GameObject activeOffObj in activeOffObjs){

            // Debug and Pause
            if (activeOffObj == null)
                Debug.LogError("Not Attach to " + this.gameObject.name.ToString());

            activeOffObj.SetActive(false);
        }
        
    }

    private IEnumerator ExecExecutables() {
        yield return new WaitForSeconds(delayTime);
        foreach (var executable in executables) {
            executable.Exec();
        }
    }
}
