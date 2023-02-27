using UnityEngine;

public class FishInteractionSwimmingController : MonoBehaviour
{
    [SerializeField] private Vector2 rangeOfSpped;

    public float MaxSpeed { get { return this.rangeOfSpped.y; } }

    private Vector3 direction;
    private bool needDistance = false;
    private Vector3 targetOfSocialDistancePos;

    private FishInteraction fishInteraction;
    private const string TagNameSocialDistance = "TargetOfSocialDistance";

    private void Start() {
        this.fishInteraction = this.transform.parent.GetComponent<FishInteraction>();
    }

    private void Update() {
        MovePositionToTarget();
    }

    private void MovePositionToTarget() {
        Vector3 targetPos = GetTargetPos();
        Vector3 preDirection = direction;
        direction = GetDirection(preDirection, targetPos);
        float speed = GetSpeed();

        this.transform.localPosition += direction * speed * Time.deltaTime;
        this.transform.localRotation = Quaternion.LookRotation(direction);
    }

    private Vector3 GetDirection(Vector3 preDirection, Vector3 targetPos) {
        Vector3 directionToTarget = (targetPos - this.transform.localPosition).normalized;
        Vector3 direction = Vector3.Slerp(preDirection, directionToTarget, Time.deltaTime);
        if(needDistance) {
            Vector3 diff = (this.transform.position - targetOfSocialDistancePos).normalized;
            direction = Vector3.Slerp(direction, diff, Time.deltaTime);
        }

        return direction;
    }

    private float GetSpeed() {
        float speed = Random.Range(rangeOfSpped.x, rangeOfSpped.y);
        if(fishInteraction.InteractionState == FishInteraction.State.Escape) speed *= 2f;
        return speed;
    }

    private Vector3 GetTargetPos() {
        return this.fishInteraction.TargetPos;
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag(TagNameSocialDistance)) {
            needDistance = true;
            targetOfSocialDistancePos = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag(TagNameSocialDistance)) {
            needDistance = false;
        }    
    }
}