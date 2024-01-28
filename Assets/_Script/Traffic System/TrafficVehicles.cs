using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficVehicles : MonoBehaviour
{
    [SerializeField] private GameObject thisVehicle;    // traffic object that going to follow 

    [SerializeField] private Traffic_Waypoints trafficSys;    // to get next position from waypoints 

    [SerializeField] private Transform toFollow;

    [SerializeField] private float moveSpeed = 5f;
    
    void Start()
    {
        thisVehicle = this.gameObject;
        trafficSys = FindAnyObjectByType<Traffic_Waypoints>();
    }


    private void Update()
    {
        setPosition();
    }

    void setPosition()
    {
        toFollow = trafficSys.getNextPosition(thisVehicle.transform);

        this.transform.position = Vector3.MoveTowards(thisVehicle.transform.position, toFollow.position, moveSpeed * Time.deltaTime);
    }




}
