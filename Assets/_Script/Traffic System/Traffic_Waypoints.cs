using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic_Waypoints : MonoBehaviour
{

    [SerializeField] private Color orbColor = Color.cyan;        // COLOR For waypoint orbs ===EDITOR ONLY===
    [SerializeField] private Color lineColor = Color.blue;       // COLOR For waypoint lines ===EDITOR ONLY===

    [Range(0.5f, 5f)]
    [SerializeField] private float orbSize = 1;                 // size of orb ===EDITOR ONLY===
    [SerializeField] private bool Loop = true;                 // Should Waypoint loop 


    /// <summary>
    /// FOR TESTING PURPOSE
    [SerializeField] private GameObject[] path;
    /// </summary>
  
    private void OnDrawGizmos()     // for EDITOR window only 
    {
        int totalCount = this.transform.childCount;
        Transform parent = this.transform;

        if(parent.childCount > 0)
        foreach (Transform orb in parent.transform.GetComponentsInChildren<Transform>())
        {
            // Drawing Waypoint orbs 
            Gizmos.color = orbColor;
            Gizmos.DrawWireSphere(orb.position, orbSize);
        }

        for (int i = 0; i < (totalCount - 1); i++)
        {
            // Drawing lines b/w orbs
            Gizmos.color = lineColor;
            Gizmos.DrawLine(parent.GetChild(i).position, parent.GetChild(i + 1).position);

            if (Loop)
            {
                Gizmos.DrawLine(parent.GetChild(0).position, parent.GetChild(totalCount - 1).position);
            }
        }
    }

    public Transform getNextPosition(Transform nextPos)
    {
        int childIndex = nextPos.GetSiblingIndex();

        //Debug.Log("SIBLING INDEX:" + childIndex);
        return (nextPos);
    }

 
}
