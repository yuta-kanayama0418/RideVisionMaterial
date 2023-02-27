using UnityEngine;

public class FishSwimmingController : MonoBehaviour
{
    [SerializeField] private Vector2 rangeOfSpeed;

    public float MaxSpeed { get { return this.rangeOfSpeed.y; } }
    
    private Vector3 targetPos = Vector3.zero;
    private FishInstantiation fishInstantiation;
    private Vector3 direction;
    private bool needDistance = false;
    private Vector3 targetOfSocialDistancePos;
    private const string TagNameSocialDistance = "TargetOfSocialDistance";

    private void Start() {
        fishInstantiation = this.transform.parent.GetComponent<FishInstantiation>();
        SetTarget();
    }

    private void Update() {
        MovePositionToTarget();

        if(Vector3.Distance(this.transform.localPosition, targetPos) < 0.3f) {
            SetTarget();
        }
        else if(needDistance) {
            if(Vector3.Distance(targetOfSocialDistancePos, targetPos) < 1.5f) {
                SetTarget();
            }
        }
    }

    private void MovePositionToTarget() {
        Vector3 preDirection = direction;
        direction = GetDirection(preDirection, this.targetPos);
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
        return Random.Range(rangeOfSpeed.x, rangeOfSpeed.y);
    }

    private void SetTarget() {
        targetPos = fishInstantiation.TargetPos;
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
