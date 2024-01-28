using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SteerType
{
    STEER_WHEEL, 
    STEER_BUTTONS
}
public class SteeringSelector : MonoBehaviour
{
    [SerializeField] private GameObject steeringWheel;
    [SerializeField] private GameObject steerButtons;

    [SerializeField] private SteerType steerSetTo = SteerType.STEER_WHEEL;
    [SerializeField] private SteerType currSelected;
   
    void Start()
    {
        if (steerButtons == null || steeringWheel == null)
        {
            Debug.Log("SteeringSelector not initialized !!");
            return;
        }

        SetSteerType(steerSetTo);
    }

    private void SetSteerType(SteerType type)
    {
        if (SteerType.STEER_WHEEL == type)
        {
            steeringWheel.SetActive(true);
            steerButtons.SetActive(false);

            currSelected = SteerType.STEER_WHEEL;
        }

        else if (SteerType.STEER_BUTTONS == type)
        {
            steeringWheel.SetActive(false);
            steerButtons.SetActive(true);

            currSelected = SteerType.STEER_BUTTONS;
        }

        else { Debug.Log("Error in setting steer type"); }

    }
    private SteerType GetSteerType()
    {
        return currSelected;
    }

}
