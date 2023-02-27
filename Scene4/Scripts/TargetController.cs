using UnityEngine;

public class TargetController : MonoBehaviour
{
    private GameObject[] targets;
    public Vector3 TargetPos { get { return GetTargetPos(); } }
    public Vector3 TargetWorldPos { get { return GetTargetWorldPos(); }}

    [SerializeField] private Vector3 maxLocalPos = new Vector3(-9999,-9999,-9999);
    [SerializeField] private Vector3 minLocalPos = new Vector3( 9999, 9999, 9999);

    private void Start() {
        int childCount = this.transform.childCount;
        targets = new GameObject[childCount];
        for(int i = 0; i < childCount; i++) {
            targets[i] = this.transform.GetChild(i).gameObject;
        }

        foreach(GameObject target in targets) {
            maxLocalPos = Vector3.Max(maxLocalPos, target.transform.localPosition);
            minLocalPos = Vector3.Min(minLocalPos, target.transform.localPosition);
        }
    }

    private Vector3 GetTargetPos() {
        if(maxLocalPos == new Vector3(-9999, -9999, -9999)) Start();
        float targetPosX = Random.Range(minLocalPos.x, maxLocalPos.x);
        float targetPosY = Random.Range(minLocalPos.y, maxLocalPos.y);
        float targetPosZ = Random.Range(minLocalPos.z, maxLocalPos.z);
        return new Vector3(targetPosX, targetPosY, targetPosZ);
    }

    private Vector3 GetTargetWorldPos() {
        if(maxLocalPos == new Vector3(-9999, -9999, -9999)) Start();
        float targetPosX = Random.Range(minLocalPos.x, maxLocalPos.x) + this.transform.position.x;
        float targetPosY = Random.Range(minLocalPos.y, maxLocalPos.y) + this.transform.position.y;
        float targetPosZ = Random.Range(minLocalPos.z, maxLocalPos.z) + this.transform.position.z;
        return new Vector3(targetPosX, targetPosY, targetPosZ);
    }
}