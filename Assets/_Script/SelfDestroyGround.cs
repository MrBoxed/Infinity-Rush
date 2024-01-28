using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyGround : MonoBehaviour
{
    [Header("Seconds")]
    [SerializeField] private int timeToDestroy = 30;
    [SerializeField] private bool staying = false;


    private void Start()
    {
       StartCoroutine( Delete(this.gameObject, timeToDestroy));
    }

    /*private void OnTriggerExit(Collider other)
    { 
        if (other.CompareTag("Player") )
        {
            staying = false;
            StartCoroutine( Delete(this.transform.root.gameObject, timeToDestroy));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        staying = true;
    }
*/
    private IEnumerator Delete(GameObject thismesh, int timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);

 /*       if (!staying)
        {
            Destroy(thismesh);
        }*/
            Destroy(thismesh);
        
    }
}
