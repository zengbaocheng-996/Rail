using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRailAndPlaceSleeper : MonoBehaviour
{	public GameObject Sleeper;

    public GameObject Rail;
    public GameObject OtherRail;
    public Vector3[] TempMeshVertex;
    public Vector3[] SamplePoints;
    public Vector3[] OtherSamplePoints;
    public Vector3[] SectionVertex;
    public Mesh mesh;
    
    private bool isFirstOperate = true;

    private void OnDrawGizmos()
    {
        int offset = SectionVertex.Length;

        Gizmos.color = Color.black;
        for (int i = 0; i < SamplePoints.Length; i++)
        {
            Gizmos.DrawSphere(TempMeshVertex[0 + i * offset], 0.1f);

        }
        

    }

    void Update()
    {
        if (TempMeshVertex.Length == 0)
        {
            TempMeshVertex=OtherRail.GetComponent<MeshFilter>().mesh.vertices;
        }
        else if (TempMeshVertex.Length != 0 && isFirstOperate == true)
        {
            isFirstOperate = false;
            // for (int i = 0; i < TempMeshVertex.Length; i++)
            // {
            //     if (i < TempMeshVertex.Length - SectionVertex.Length * 2)
            //         TempMeshVertex[i] = TempMeshVertex[i] + 0.6f * ((TempMeshVertex[i % SectionVertex.Length*SectionVertex.Length + 1] -
            //                                                          TempMeshVertex[i % SectionVertex.Length*SectionVertex.Length])
            //             .normalized);
            //     else if (i < TempMeshVertex.Length - SectionVertex.Length)
            //     {
            //         TempMeshVertex[i] = TempMeshVertex[i] + 0.6f * ((TempMeshVertex[1] - TempMeshVertex[0]).normalized);
            //     }
            //     else
            //     {
            //         TempMeshVertex[i] = TempMeshVertex[i] + 0.6f * ((TempMeshVertex[SectionVertex.Length*(SamplePoints.Length-1)+1] - TempMeshVertex[SectionVertex.Length*(SamplePoints.Length-1)]).normalized);
            //     }
            // }
            //
            // OtherRail.GetComponent<MeshFilter>().mesh.vertices = TempMeshVertex;
            
            //OtherRail.transform.position = Rail.transform.position + new Vector3(0.6f,0,0);
            OtherSamplePoints = new Vector3[SamplePoints.Length];
            int offset = SectionVertex.Length;
            for (int i = 0; i < SamplePoints.Length; i++)
            {
                OtherSamplePoints[i] = TempMeshVertex[0 + i * offset];
                GameObject SleeperClone = Instantiate(Sleeper);
                Vector3 tempVector = TempMeshVertex[1+i*offset]-TempMeshVertex[0+i*offset];
                SleeperClone.transform.position = (OtherSamplePoints[i] + SamplePoints[i]) / 2;
                SleeperClone.transform.forward = new Vector3(tempVector.z,0,-tempVector.x);
            }
            // OtherRail.transform.position = Rail.transform.position + 0.6f*((TempMeshVertex[1]-TempMeshVertex[0]).normalized);
            // for (int i = 0; i < SamplePoints.Length; i++)
            // {
            //     GameObject SleeperClone = Instantiate(Sleeper);
            //     Vector3 tempVector = TempMeshVertex[SectionVertex.Length*i+1]-TempMeshVertex[SectionVertex.Length*i];
            //     SleeperClone.transform.position = TempMeshVertex[SectionVertex.Length*i]+0.3f*tempVector.normalized;
            //     SleeperClone.transform.forward = new Vector3(tempVector.z,0,-tempVector.x);
            // }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetSamplePoints();
        GetSectionVertex();
        Rail = GameObject.Find("Rail");
        OtherRail = GameObject.Find("OtherRail");

        //OtherRail = Instantiate(Rail);
        //OtherRail.name = "OtherRail";



        // mesh=OtherRail.GetComponent<MeshFilter>().mesh;



        // OtherRail = GameObject.Find("Rail(Clone)");



        // for (int i = 0; i < OtherRail.MeshVertex.Length; i++)
        // {
        //     OtherRail.MeshVertex[i] = OtherRail.MeshVertex[i] + 3 * ((SectionVertex[1] - SectionVertex[0]).normalized);
        // }


        // mesh = OtherRail.GetComponent<MeshFilter>().mesh;
        // TempMeshVertex = mesh.vertices;
        // mesh.vertices = OtherRail.MeshVertex;
    }
    private void GetSamplePoints()
    {
        GameObject go = GameObject.Find("ControlPoints");
        CatmullRomSpline other = (CatmullRomSpline)go.GetComponent(typeof(CatmullRomSpline));
        SamplePoints = other.SamplePoints;
    }

    private void GetSectionVertex()
    {
        string path = "guidao08";
        GameObject go = Resources.Load(path) as GameObject;
        Mesh mesh = go.GetComponent<MeshFilter>().sharedMesh;
        SectionVertex = mesh.vertices;
    }
    
    
    
}
