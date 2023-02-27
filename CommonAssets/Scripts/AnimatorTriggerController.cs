using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTriggerController : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField] private string animatorTriggerName;

    void OnEnable()
    {
        // Debug and Pause
        if (animator == null || animatorTriggerName == "")
            Debug.LogError("Not Attach to " + this.gameObject.name.ToString());


        // GameObject(Animator)がactiveoffの時はSetTriggerしないように
        if (animator.gameObject.activeSelf){
            animator.SetTrigger(animatorTriggerName);
        }

    }
    
}
