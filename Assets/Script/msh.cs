using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class msh : MonoBehaviour
{
    public Mesh mesh;
    public Vector3[] vertices;
   
    private void Read()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices=mesh.vertices;
        Debug.Log(mesh.vertices);
    }
    // Start is called before the first frame update
    void Start()
    {
        Read();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
