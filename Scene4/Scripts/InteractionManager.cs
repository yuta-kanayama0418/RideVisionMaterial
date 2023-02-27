using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private FishInteraction fishInteraction;

    void Start()
    {
        fishInteraction.setCanInteraction(true);
    }

    private void OnDisable()
    {
        fishInteraction.setCanInteraction(false);
    }
}