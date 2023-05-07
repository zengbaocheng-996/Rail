using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GetSplinePoints : MonoBehaviour
{
    private Mesh mesh;
    public Vector3[] vertices;
    public Transform[] controlPointsList;
    [Tooltip("是否成环")]
    public bool isLooping = false;
    //[Tooltip("是否连接首尾控制点")]
    //public bool isConnecting = false;
    public Vector3[] samplePoints;
    //private Transform artiFirst;
    //private Transform artiLast;
    [Tooltip("步长")]
    public float resolution = 0.2f;

    void Start()
    {
        GetSamplePoints();
        //CreateGO();
    }
    void CreateGO(int i)
    {
        //Mesh newMesh = new Mesh();
        GameObject simpleMesh = new GameObject();
        //simpleMesh.transform.parent = GetComponent<Transform>();
        simpleMesh.transform.position = samplePoints[i];
        simpleMesh.transform.localScale = new Vector3(10,10,10);
        if(i!=0)
        {
            simpleMesh.transform.forward =  samplePoints[i - 1]-samplePoints[i] ;
        }
        simpleMesh.AddComponent<MeshFilter>();
        simpleMesh.AddComponent<MeshRenderer>();
        simpleMesh.GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;
        simpleMesh.GetComponent<MeshRenderer>().material= GetComponent<MeshRenderer>().material;

        vertices = simpleMesh.GetComponent<MeshFilter>().mesh.vertices;
        //mesh = GetComponent<MeshFilter>().mesh;
        //vertices = mesh.vertices;
        //Vector3 delta = samplePoints[0] - vertices[0];
        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    vertices[i] = vertices[i] + delta;
        //}
        //newMesh.vertices = vertices;
    }
 
    void GetSamplePoints()
    {
        int loops = Mathf.FloorToInt(1f / resolution);
        samplePoints = new Vector3[loops];
        //Gizmos.color = Color.white;
        Debug.Log(controlPointsList.Length);
        for (int i = 0; i < controlPointsList.Length; i++)
        {
            // 不循环才有可能执行
            if ((i == 0 || i == controlPointsList.Length - 2 || i == controlPointsList.Length - 1) )
            {
                Debug.Log("shit");
                continue;
            }
            GetCatmullRomSpline(i);
        }
    }
    void GetCatmullRomSpline(int pos)
    {

        Vector3 p0 = controlPointsList[ClampListPos(pos - 1)].position;
        Vector3 p1 = controlPointsList[pos].position;
        Vector3 p2 = controlPointsList[ClampListPos(pos + 1)].position;
        Vector3 p3 = controlPointsList[ClampListPos(pos + 2)].position;
        // 开始点
        Vector3 lastPos = p1;
        int loops = Mathf.FloorToInt(1f / resolution);
        for (int i = 1; i <= loops; i++)
        {
            Debug.Log(lastPos);
            samplePoints[i - 1] = lastPos;
            CreateGO(i - 1);
            float t = i * resolution;
            Vector3 newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);
            lastPos = newPos;
        }
    }

    int ClampListPos(int pos)
    {
        if (pos < 0)
        {
            pos = controlPointsList.Length - 1;
        }
        if (pos > controlPointsList.Length)
        {
            pos = 1;
        }
        else if (pos > controlPointsList.Length - 1)
        {
            pos = 0;
        }
        return pos;
    }

    Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
        return pos;
    }

}
