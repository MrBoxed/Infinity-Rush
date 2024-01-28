using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    internal enum DriveType
    {
        FrontWheelDrive,
        RearWheelDrive,
        AllWheelDrive
    }


    [SerializeField] private DriveType drive;
    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private float currentBreakForce = 0f;
    private bool isbreaking = false;


    [SerializeField] private WheelCollider[] wheels = null;

    [SerializeField] private Transform[] wheelTransform = null;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject com;

    public float maxSteerAngle = 30f;
    public float motorForce = 50f;
    public float brakeTorque = 50f;
    public float steerWheelsFromMobile;

    private Vector3 pos;
    private Quaternion rot;

    private void Start()
    {
        rb.centerOfMass = com.transform.localPosition;
        rot = rb.rotation;
        pos = rb.position;

    }

    private void FixedUpdate()
    {
        
        UpdateWheelPoses();
        //Handling();

        Accelerate(1);

    }

    private void Handling()
    {
        float steerT = SimpleInput.GetAxis("Horizontal") * maxSteerAngle;

        for (int i = 0; i < 2; i++)
        {
            wheels[i].steerAngle = steerT;
        }
    }

    private void Accelerate(int val)
    {
        /// The reason for it being "5 times less" is because you're not accounting for your gear ratio and final drive. 
        /// wheelcollider.motorTorque should be the FINAL result after getting the engine torque (what you're assuming wheelcollider.motortorque to be), 
        /// multiplying it by the current gear ratio (example, 1st is 4.2, 2nd is 3.4, reverse is -4, neutral is 0, etc.), 
        /// and then multiplying that by the final drive of the differential (example, 3.6). 
        /// So to recap, it should be the desiredMotorTorque * gearRatio * finalDrive = wheelcollider.motorTorque. 
        /// it seem like unity is calculating motor torque incorrectly, when it is indeed infact correct. But I do agree that wheelcollider.motorTorque should be renamed to something like wheelcollider.wheelTorque. EDIT: thanks for pinning my comment Pablo!

        float motor = val * ((motorForce * 5) / 4);
      
           
        if (drive == DriveType.AllWheelDrive)
        {
            foreach (var wheel in wheels)
            {
                wheel.motorTorque = motor;
            }
        }

        if(drive == DriveType.FrontWheelDrive)
        {
            for (int i = 0; i < 2; i++)
            {
                wheels[i].motorTorque = motor;
                
            }
        }

        if (drive == DriveType.RearWheelDrive)
        {
            for (int i = 2; i < 4; i++)
            {
                wheels[i].motorTorque = motor;

            }
        }

    }

    private void UpdateWheelPoses()
    {
        int length = wheels.Length;
        for (int i = 0; i < length; i++)
        {
            UpdateWheelPose(wheels[i], wheelTransform[i]);
        }
        
    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform transform)
    { 
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion quat);
        transform.SetPositionAndRotation(pos, quat);
    }


    private void ApplyBrake()
    {
        currentBreakForce = isbreaking ? brakeTorque : 0f;
        foreach (var wheel in wheels)
        {
            wheel.brakeTorque = currentBreakForce;
        }
    }

    public void isAccelerating(bool val)
    {
        if (val)
            Accelerate(1);

        else { Accelerate(0); }
    }


    public void isBraking(bool val)
    {
        if (val)
        {
            isbreaking = true;
            ApplyBrake();
        }

        else
        { 
            isbreaking = false;
            ApplyBrake();
        }
    }

    public void ReOrient(bool val)
    {
        if (val)
        {
            Quaternion rot = rb.transform.rotation;
            Vector3 pos = rb.transform.localPosition;

            pos.y += 2;
            rot.z = 0;

            rb.transform.SetPositionAndRotation(pos, rot);
        }

        else { return; }
    }

}
