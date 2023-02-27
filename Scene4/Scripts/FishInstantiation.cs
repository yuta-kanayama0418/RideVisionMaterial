using UnityEngine;

public class FishInstantiation : MonoBehaviour
{
    [SerializeField] private GameObject fishObj;
    [SerializeField] private int initCount;
    [SerializeField] TargetController targetController;
    [SerializeField] private Vector2 multiScale = new Vector2(0.9f, 1.1f);

    public Vector3 TargetPos { get { return targetController.TargetPos; } }

    private void Awake( ) {
        
        // Debug and Pause
        if(fishObj == null || targetController == null)
            Debug.LogError("Not Attach to " + this.gameObject.name.ToString()); 

        InstantiateFishes();
    }

    private void InstantiateFishes() {
        for(int i = 0; i < initCount; i++) {
            Vector3 initPos = targetController.TargetWorldPos;
            GameObject fish = Instantiate(fishObj, initPos, Quaternion.identity);
            fish.transform.localScale *= Random.Range(multiScale.x, multiScale.y);
            fish.transform.parent = this.transform;

            Animator animator = fish.GetComponent<Animator>();
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, Random.Range(0f, 1f));
        }
    }
}