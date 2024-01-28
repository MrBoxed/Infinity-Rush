using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;
using UnityEngine.UI;
using TMPro;

public enum SpeedData { KMPH, MPH };

public class TelemetryInformation : MonoBehaviour
{

    [SerializeField] private VPVehicleController _vehicleController;

    [SerializeField] private TextMeshProUGUI speedText;         // For speed variable ;
    [SerializeField] private TextMeshProUGUI gearNoText;            // For Gear  ;
    [SerializeField] private TextMeshProUGUI metricSystem;      // To select Metric System;
    
    [SerializeField] private Slider rpmMeterSlider;                   // RPM Slider in Speedometer :: set Min Max value in editor 

    [SerializeField] private SpeedData _speedData = new();
    [SerializeField] private SpeedData _choosenData;


    private int speed;          // For calculating the speed ;
    private int gear;          // For calculating the speed ;

    void Start()
    {

        if (_vehicleController == null)
        {
            _vehicleController = FindAnyObjectByType<VPVehicleController>(); // Fetching the car controller component
            if (_vehicleController == null)
                Debug.Log("No Vehicle Controller ");
        }    
        if (speedText == null || gearNoText == null || rpmMeterSlider == null) { 
            Debug.Log("SpeedDataSystem gameObject not specified"); 
        }

        if (_speedData.Equals(SpeedData.KMPH))
            metricSystem.SetText("KM/H");

        else { metricSystem.SetText("MPH"); }

    }

    void Update()
    {
        speed       = ((int)((_vehicleController.speed * 3600) / 1000)); // calculating the speed from VPP controller
        speed = Mathf.Abs(speed);
        speedText.SetText(speed.ToString());

        rpmMeterSlider.value = (int)(_vehicleController.data.Get(Channel.Vehicle, VehicleData.EngineRpm) / 1000.0f);

        gear = (_vehicleController.data.Get(Channel.Vehicle, VehicleData.GearboxGear));



        if (gear < 0)
        {
            gearNoText.SetText("R");
        }
        else if (gear == 0)
        {
            int value = _vehicleController.data.Get(Channel.Vehicle, VehicleData.GearboxMode);
            if (value == 1) gearNoText.SetText("P");
            else gearNoText.SetText("N");
        }
        else gearNoText.SetText(gear.ToString());



    }
}
