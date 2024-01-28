using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct RoadMesh
{
    public GameObject mesh;
    public Vector3 position;
 
}

public class RandomSpawnGenerator : MonoBehaviour
{
    [SerializeField] private RoadMesh[] element;

    [SerializeField] public int meshToKeep = 4;
    [SerializeField] public int count = 0;
    
    private Vector3 jointPosition;

    void Start()
    {
        jointPosition = this.gameObject.transform.localPosition;
        Generate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W ))
        {
            Generate(); 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(jointPosition, Vector3.one);
    }
    void Generate()
    {
        int choose = Random.Range(0, element.Length);

        //meshDimension = (element[choose].mesh.transform);

        GameObject newMesh = Instantiate(element[choose].mesh, jointPosition, element[choose].mesh.transform.rotation);

        jointPosition = newMesh.transform.position + element[choose].position;

        count++;
    }


}
