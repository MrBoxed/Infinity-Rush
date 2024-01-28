using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VehiclePhysics;


enum GearType
{
    AUTOMATIC = InputData.AutomaticGear,
    MANUAL = InputData.ManualGear
}

public class PlayerControlInputUI : MonoBehaviour
{
    // For controlling Vehicle Refernce at Runtime
    [Header("VPP controller")]
    [SerializeField] private VPVehicleController controller;

    // Refernece to button should be Configure in the Editor 
    [Header("Input Buttons")]
    [SerializeField] private Button _accelerateBtn;
    [SerializeField] private Button _brakeBtn, _handBrakeBtn, _engineKeyStartBtn;

    // Refernece to toggles should be Configure in the Editor 
    [Header("Features"), Space]
    [SerializeField] private Toggle _toggleABS;
    [SerializeField] private Toggle _toggleTCS, _toggleESC, _toggleASR ;

    [SerializeField, Header("GearBox")]
    private Slider _gearSlider;

    [SerializeField] private GearType _gearType = GearType.AUTOMATIC;

    /// <summary>
    ///  Value to 
    /// </summary>
    private readonly int[] keyState = { -1, 0, 1 }; // State of ignition key (-1 = off, 0 = acc-on, 1 = Start) 
    private readonly int[] autoGearMode = { 1, 2, 3, 4 }; // 0, 1, 2, 3, 4 = M, P, R, N, D 1,
                                                          // 5, 6, 7, 8, 9 = D1, D2, D3, D4, D5 1
    private readonly int[] valueState = { 0, 1, 2 }; // State of (0 = no override, 1 = force enabled, 2 = force disabled )

    private int _key = 0; // variable for looping in keyState
    
    private int handBrakeValue = 0, throttleValue=0, brakeValue=0; 
    private bool _throttle, _brakes, _handBrake = false;
    
    void Start()
    {
        if(_accelerateBtn == null || _brakeBtn == null|| _handBrakeBtn == null) { return; }
        
        // change later with pooling or settig from player
        controller = FindObjectOfType<VPVehicleController>();
        if (controller == null)
            Debug.Log("Vehicle Not Found!!");

    }
    
    void FixedUpdate()
    {
        // Setting data Values
        throttleValue   = (_throttle == true)   ? 10000 : 0;
        brakeValue      = (_brakes == true)     ? 10000 : 0;
        handBrakeValue  = (_handBrake == true)  ? 10000 : 0;


        // Setting vehicles Data in VPP Controller

        controller.data.Set(Channel.Input, InputData.Steer, 
                                (int)(SimpleInput.GetAxis("Horizontal") * 10000));  // For Steerting Input 
        controller.data.Set(Channel.Input, InputData.Key        , keyState[_key]);  // Key State
        controller.data.Set(Channel.Input, InputData.Throttle   , throttleValue);   // for Throttle/ Accelertion 
        controller.data.Set(Channel.Input, InputData.Brake      , brakeValue);      // for brakes 
        controller.data.Set(Channel.Input, InputData.Handbrake  , handBrakeValue);  // for handBrake
        
    }

    public void SetThrottle(bool click)
    {
        _throttle = click;
    }

    public void SetBrake(bool click)
    {
        _brakes = click;
    }

    public void KeyStartFunc()
    {
        _key++;
        _key %=  3;

    }

    public void SetGear()
    {
       controller.data.Set(Channel.Input, ((int)_gearType), (int)_gearSlider.value);
    }

    public void SetHandBrake(bool value)
    {
        _handBrake = value;
    }
}
