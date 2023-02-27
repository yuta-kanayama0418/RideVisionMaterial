using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FishSwimOnPath : MonoBehaviour
{
    [SerializeField] private GameObject bus;
    [SerializeField] private float fishSpeed;
    [SerializeField] private Vector2 fromRange = new Vector2(0, 1);
    [SerializeField] private Vector2 toRange = new Vector2(0, 1);
    private CinemachineDollyCart cinemachineDollyCart;
    private Vector3 preBusPosition;
    private float speed= 1;

    private void Start() {
        
        // Debug and Pause
        if(bus == null)
            Debug.LogError("Not Attach to " + this.gameObject.name.ToString());

        cinemachineDollyCart = this.GetComponent<CinemachineDollyCart>();
        preBusPosition = this.bus.transform.position;
    }

    private void Update() {
        SetSpeed();
    }

    private void SetSpeed() {
        float preSpeed = this.speed;
        float busSpeed = (this.bus.transform.position - this.preBusPosition).magnitude / Time.deltaTime;
        this.speed = MathRange(busSpeed, fromRange, toRange) * fishSpeed;
        this.speed = Mathf.Lerp(preSpeed, this.speed, Time.deltaTime);
        cinemachineDollyCart.m_Speed = this.speed;
        preBusPosition = this.bus.transform.position;

        if(speed > 0 && cinemachineDollyCart.m_Position == cinemachineDollyCart.m_Path.PathLength) {
            cinemachineDollyCart.m_Position = 0;
        }

        if(speed < 0 && cinemachineDollyCart.m_Position == 0) {
            cinemachineDollyCart.m_Position = cinemachineDollyCart.m_Path.PathLength;
        }
    }

    private float MathRange(float value, Vector2 fromRange, Vector2 toRange) {
        value = (toRange.y - toRange.x) / (fromRange.y - fromRange.x) * (value - fromRange.x) + toRange.x;
        return value;
    }
}