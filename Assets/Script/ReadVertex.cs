using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ReadVertex : MonoBehaviour
{
    public Vector3[] vertex;
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh;
        mesh=GetComponent<MeshFilter>().mesh;
        vertex=mesh.vertices;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
