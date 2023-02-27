using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonMovement : MonoBehaviour
{
    [SerializeField] private float minspeed = 0.3f;
    [SerializeField] private float maxspeed = 0.8f;
    [SerializeField] private float degreeOfRise = 1.0f;


    private float time = 0f;
    private float speed = 0f;
    private float addTime;

    void Start()
    {
        speed = Random.Range(minspeed, maxspeed);
        addTime = Random.Range(0,Mathf.PI*2);
        time += addTime;
    }
    void Update()
    {
        time += Time.deltaTime;
        float sin = Mathf.Sin(time);

        if(sin > 0) {
            sin *= degreeOfRise;
        }


        transform.position += transform.up * speed * sin;
        
    }

}