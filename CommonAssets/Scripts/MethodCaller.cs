using UnityEngine;
using UnityEngine.Events;

public class MethodCaller : MonoBehaviour {
    public UnityEvent call;

#if !UNITY_EDITOR
	void Awake()
	{
		Destroy( this );
	}
#endif
}