using System.Collections.Generic;
using UnityEngine;

public class FishInteraction : MonoBehaviour
{
    public enum State : int
    {
        Swim,
        Approaching,
        Escape
    }

    [SerializeField] private State state = State.Swim;

    [SerializeField] private GameObject bossObj;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform handTarget;
    [SerializeField] private float EscapeTime = 3f;
    [SerializeField] private float detectionTime = 2f;
    [SerializeField] private bool canInteraction = false;

    private List<GameObject> fishes = new List<GameObject>();
    private Vector3 targetPos = Vector3.zero;
    private Vector3 bossPos = Vector3.zero;
    private Vector3 bossTargetPos;
    private FishInstantiation fishInstantiation;

    public Vector3 TargetPos { get { return this.targetPos; } }
    public State InteractionState { get { return this.state; } }

    private float time = 0;
    private float escapeTime = 0;

    private void Start()
    {

        // Debug and Pause
        if (bossObj == null || leftHandTransform == null || rightHandTransform == null || handTarget == null)
            Debug.LogError("Not Attach to " + this.gameObject.name.ToString());


        fishInstantiation = this.GetComponent<FishInstantiation>();
        SetInitBossPos();
        SetBossTargetPos();
        GetChildren();
    }

    private void Update()
    {
        MoveBoss();
        SetTargetPos();

        checkHasHand();
    }

    public void setCanInteraction(bool b)
    {
        this.canInteraction = b;
    }

    private void checkHasHand()
    {
        if (!canInteraction) return;

        bool hasLeftHand = leftHandTransform.GetChild(0).gameObject.activeSelf;
        bool hasRightHand = rightHandTransform.GetChild(0).gameObject.activeSelf;
        bool isInteraction = hasLeftHand || hasRightHand;

        switch (this.state)
        {
        case State.Swim:
            if (isInteraction) SwitchState();
            break;
        case State.Approaching:
            if (!isInteraction)
            {
                time += Time.deltaTime;
                if (time > detectionTime)
                {
                    SwitchState();
                }
            }
            else
            {
                time = 0;
            }
            break;
        case State.Escape:
            time = 0;
            break;
        }
    }

    private void SwitchState()
    {
        switch (this.state)
        {
        case State.Swim:
            this.state = State.Approaching;
            bossPos = handTarget.position;
            SetBossTargetPos();
            break;
        case State.Approaching:
            this.state = State.Escape;
            SetInitBossPos();
            SetBossTargetPos();
            break;
        case State.Escape:
            this.state = State.Swim;
            break;
        }
        Debug.Log(this.state);
    }

    private void SetInitBossPos()
    {
        bossPos = fishInstantiation.TargetPos;
    }

    private void SetBossTargetPos()
    {
        if (this.state == State.Swim || this.state == State.Escape)
        {
            bossTargetPos = fishInstantiation.TargetPos;
        }
        else if (this.state == State.Approaching)
        {
            bossTargetPos = handTarget.position;
        }
    }

    private void MoveBoss()
    {
        if (this.state == State.Escape)
        {
            escapeTime += Time.deltaTime;
            if (escapeTime > EscapeTime)
            {
                escapeTime = 0;
                SwitchState();
            }
        }

        if (Vector3.Distance(bossPos, bossTargetPos) < 0.1f)
        {
            SetBossTargetPos();
        }
        if (this.state != State.Approaching)
        {
            bossPos += (bossTargetPos - bossPos).normalized * fishes[0].GetComponent<FishInteractionSwimmingController>().MaxSpeed * Time.deltaTime;
            bossObj.transform.localPosition = bossPos;
        }
        else
        {
            bossPos = handTarget.position;
            bossObj.transform.position = bossPos;
        }
    }

    private void GetChildren()
    {
        foreach (Transform childTransform in this.gameObject.transform)
        {
            GameObject child = childTransform.gameObject;
            fishes.Add(child);
        }
    }

    private void SetTargetPos()
    {
        Vector3 center = Vector3.zero;

        foreach (GameObject fish in fishes)
        {
            center += fish.transform.localPosition / fishes.Count;
            Rigidbody rb = fish.GetComponent<Rigidbody>();
        }
        targetPos = (center + bossObj.transform.localPosition) / 2;
    }
}