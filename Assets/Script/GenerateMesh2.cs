using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using math3d;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GenerateMesh2 : MonoBehaviour
{
    //public List<Vector3> 
    public Vector3[] MeshVertex;
    public int[] MeshTriangle;
    public Vector3[] SamplePoints;
    public Vector2[] UV;
    private Vector3[] SectionVertex;
    private Angle[] AngleList;
    private Vector3 right1;
    private zVector2 right2;
    private Vector3 right3;
    private float theta;
    public float testValue;
    public Vector3[] DebugVector2;
    public Vector3[] DebugVector;
    public Vector3[] DebugVector3;
    void Start()
    {
        GetSamplePoints();
        GetSectionVertex();
        SortSectionVertex();
        GetMeshVertex();
        GetMeshTriangle();
        SetUV();
        SetMesh();
    }

    private void SetMesh()
    {
        Mesh mesh;
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "rail";
        mesh.vertices = MeshVertex;
        mesh.triangles = MeshTriangle;
        mesh.uv = UV;
        // mesh.RecalculateNormals();
    }
    private void GetMeshTriangle()
    {
        MeshTriangle = new int[6 * (SectionVertex.Length - 2) * (SamplePoints.Length - 1) + 12 * 6];
        int offset = SectionVertex.Length;
        int index = 0;
        for (int i = 0; i < SamplePoints.Length - 1; i++)
        {
            for (int j = 0; j < SectionVertex.Length; j++)
            {
                if (j == 0 || j == 7)
                {
                    continue;
                }

                if (j == 6)
                {
                    SetQuad(index, j + offset * i, j + 2 + offset * i, j + offset * (i + 1), j + 2 + offset * (i + 1));
                    index += 6;
                    continue;
                }
                if (j == 13)
                {
                    SetQuad(index, j + offset * i, 1 + offset * i, j + offset * (i + 1), 1 + offset * (i + 1));
                    index += 6;
                    continue;
                }

                SetQuad(index, j + offset * i, j + 1 + offset * i, j + offset * (i + 1), j + 1 + offset * (i + 1));
                index += 6;
            }
        }

        offset = SectionVertex.Length * (SamplePoints.Length + 1);
        int offset_start = SectionVertex.Length * SamplePoints.Length;
        SetTriangle(index, 0 + offset_start, 2 + offset_start, 1 + offset_start);
        index += 3;
        SetTriangle(index, 1 + offset, 2 + offset, 0 + offset);
        index += 3;

        SetTriangle(index, 0 + offset_start, 3 + offset_start, 2 + offset_start);
        index += 3;
        SetTriangle(index, 2 + offset, 3 + offset, 0 + offset);
        index += 3;

        SetTriangle(index, 0 + offset_start, 4 + offset_start, 3 + offset_start);
        index += 3;
        SetTriangle(index, 3 + offset, 4 + offset, 0 + offset);
        index += 3;

        SetTriangle(index, 0 + offset_start, 7 + offset_start, 4 + offset_start);
        index += 3;
        SetTriangle(index, 4 + offset, 7 + offset, 0 + offset);
        index += 3;

        SetTriangle(index, 4 + offset_start, 6 + offset_start, 5 + offset_start);
        index += 3;
        SetTriangle(index, 5 + offset, 6 + offset, 4 + offset);
        index += 3;

        SetTriangle(index, 4 + offset_start, 7 + offset_start, 6 + offset_start);
        index += 3;
        SetTriangle(index, 6 + offset, 7 + offset, 4 + offset);
        index += 3;

        SetTriangle(index, 0 + offset_start, 10 + offset_start, 7 + offset_start);
        index += 3;
        SetTriangle(index, 7 + offset, 10 + offset, 0 + offset);
        index += 3;

        SetTriangle(index, 10 + offset_start, 9 + offset_start, 7 + offset_start);
        index += 3;
        SetTriangle(index, 7 + offset, 9 + offset, 10 + offset);
        index += 3;

        SetTriangle(index, 9 + offset_start, 8 + offset_start, 7 + offset_start);
        index += 3;
        SetTriangle(index, 7 + offset, 8 + offset, 9 + offset);
        index += 3;

        SetTriangle(index, 0 + offset_start, 11 + offset_start, 10 + offset_start);
        index += 3;
        SetTriangle(index, 10 + offset, 11 + offset, 0 + offset);
        index += 3;

        SetTriangle(index, 0 + offset_start, 13 + offset_start, 11 + offset_start);
        index += 3;
        SetTriangle(index, 11 + offset, 13 + offset, 0 + offset);
        index += 3;

        SetTriangle(index, 13 + offset_start, 12 + offset_start, 11 + offset_start);
        index += 3;
        SetTriangle(index, 11 + offset, 12 + offset, 13 + offset);
    }

    private void SetUV()
    {
        int offset = SectionVertex.Length;
        float segmentValue = (float)1 / (SamplePoints.Length - 1);
        float xValue;

        for (int i = 0; i < SectionVertex.Length; i++)
        {
            for (int j = 0; j < SamplePoints.Length; j++)
            {
                xValue = segmentValue * j;

                if (i == 1)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 195 / 512f);
                }
                if (i == 2)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 509 / 512f);
                }

                if (i == 3)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 500 / 512f);
                }
                if (i == 4)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 490 / 512f);
                }
                if (i == 5)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 479 / 512f);
                }
                if (i == 6)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 466 / 512f);
                }

                if (i == 8)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 432 / 512f);
                }
                if (i == 9)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 424 / 512f);
                }
                if (i == 10)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 410 / 512f);
                }
                if (i == 11)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 400 / 512f);
                }
                if (i == 12)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 392 / 512f);
                }
                if (i == 13)
                {
                    UV[i + j * offset] = new Vector2(xValue, 1 - 390 / 512f);
                }
            }
        }

        for (int i = 0; i < 2; i++)
        {
            offset = SectionVertex.Length * (SamplePoints.Length + i);
            UV[offset + 5] = new Vector2(36 / 512f, 1 - 10 / 512f);
            UV[offset + 6] = new Vector2(36 / 512f, 1 - 2 / 512f);
            UV[offset + 7] = new Vector2(22 / 512f, 1 - 2 / 512f);
            UV[offset + 8] = new Vector2(9 / 512f, 1 - 2 / 512f);
            UV[offset + 9] = new Vector2(9 / 512f, 1 - 10 / 512f);

            UV[offset + 10] = new Vector2(16 / 512f, 1 - 13 / 512f);
            UV[offset + 4] = new Vector2(29 / 512f, 1 - 13 / 512f);

            UV[offset + 11] = new Vector2(16 / 512f, 1 - 24 / 512f);
            UV[offset + 3] = new Vector2(29 / 512f, 1 - 24 / 512f);

            UV[offset + 12] = new Vector2(10 / 512f, 1 - 28 / 512f);
            UV[offset + 2] = new Vector2(31 / 512f, 1 - 28 / 512f);

            UV[offset + 13] = new Vector2(10 / 512f, 1 - 31 / 512f);
            UV[offset + 0] = new Vector2(22 / 512f, 1 - 31 / 512f);
            UV[offset + 1] = new Vector2(35 / 512f, 1 - 31 / 512f);
        }
    }

    private void SetTriangle(int index, int v0, int v2, int v1)
    {
        MeshTriangle[index] = v0;
        MeshTriangle[index + 1] = v1;
        MeshTriangle[index + 2] = v2;

    }
    private void SetQuad(int index, int v00, int v01, int v10, int v11)
    {
        MeshTriangle[index] = v01;
        MeshTriangle[index + 1] = v00;
        MeshTriangle[index + 2] = v11;
        MeshTriangle[index + 3] = v11;
        MeshTriangle[index + 4] = v00;
        MeshTriangle[index + 5] = v10;
    }

    private void SortSectionVertex()
    {
        Swap(1, 2);
        Swap(5, 7);
        Swap(8, 10);
        Swap(12, 13);
    }

    private void Swap(int indexA, int indexB)
    {
        Vector3 temp = new Vector3();
        temp = SectionVertex[indexA];
        SectionVertex[indexA] = SectionVertex[indexB];
        SectionVertex[indexB] = temp;
    }

    private void GetMeshVertex()
    {
        MeshVertex = new Vector3[SamplePoints.Length * SectionVertex.Length + 2 * SectionVertex.Length];
        UV = new Vector2[SamplePoints.Length * SectionVertex.Length + 2 * SectionVertex.Length];
        AngleList = new Angle[SamplePoints.Length];
        RotateAndTransform();
        for (int j = 0; j < SectionVertex.Length; j++)
        {
            MeshVertex[MeshVertex.Length - 2 * SectionVertex.Length + j] = MeshVertex[j];
        }
        int offset = SectionVertex.Length * (SamplePoints.Length - 1);
        for (int j = 0; j < SectionVertex.Length; j++)
        {
            MeshVertex[MeshVertex.Length - SectionVertex.Length + j] = MeshVertex[j + offset];
        }

    }

    private void RotateAndTransform()
    {
        Vector3 temp;
        Vector3 delta;
        int index = 0;
        DebugVector = new Vector3[SamplePoints.Length];
        DebugVector2 = new Vector3[SamplePoints.Length];

        for (int i = 0; i < SamplePoints.Length; i++)
        {
            temp = SectionVertex[0];
            delta = SamplePoints[i] - temp;
            // right1 是沿铁轨垂直方向
            if (i + 1 != SamplePoints.Length)
                right1 = SamplePoints[i + 1] - SamplePoints[i];
            else
                right1 = SamplePoints[i] - SamplePoints[i - 1];
            Vector3 right2 = new Vector3(-right1.z, 0, right1.x).normalized;
            DebugVector2[i] = right1.normalized;
            DebugVector[i] = right2;

            Vector3 d1 = SectionVertex[1] - SectionVertex[0];
            Vector3 d2 = right2;
            float angle = Vector3.Angle(d1, d2);
            Vector3 axis = SectionVertex[7] - SectionVertex[0];
            //反向检测
            Vector3 temp0 = SectionVertex[0];
            Vector3 temp1 = SectionVertex[1];
            Quaternion rotation = Quaternion.AngleAxis(angle, axis);
            temp0 = rotation * temp0;
            temp1 = rotation * temp1;
            float angleDebug = Vector3.Angle(temp1 - temp0, d2);
            if (angleDebug != 0)
            {
                angle = -angle;
            }
            for (int j = 0; j < SectionVertex.Length; j++, index++)
            {
                temp = SectionVertex[j];
                //绕axis轴旋转angle角度
                rotation = Quaternion.AngleAxis(angle, axis);
                //四元数 * 向量(不能调换位置, 否则发生编译错误)
                temp = rotation * temp;//旋转后的向量
                temp = temp + delta;
                MeshVertex[index] = temp;

                if (j == SectionVertex.Length - 1)
                {
                    float afterAngleDebug = Vector3.Angle(MeshVertex[index - SectionVertex.Length + 2] - MeshVertex[index - SectionVertex.Length + 1], d2);
                    if (afterAngleDebug > 1f)
                    {
                        axis = MeshVertex[index - SectionVertex.Length + 8] - MeshVertex[index - SectionVertex.Length + 1];

                        //反向检测
                        temp0 = MeshVertex[index - SectionVertex.Length + 1];
                        temp1 = MeshVertex[index - SectionVertex.Length + 2];

                        rotation = Quaternion.AngleAxis(afterAngleDebug, axis);
                        temp0 = rotation * temp0;
                        temp1 = rotation * temp1;

                        angleDebug = Vector3.Angle(temp1 - temp0, d2);

                        if (angleDebug > 1f)
                        {
                            afterAngleDebug = -afterAngleDebug;
                        }

                        rotation = Quaternion.AngleAxis(afterAngleDebug, axis);
                        for (int k = 1; k <= SectionVertex.Length; k++)
                        {
                            MeshVertex[index - SectionVertex.Length + k] = rotation * MeshVertex[index - SectionVertex.Length + k];

                        }
                        temp = MeshVertex[index - SectionVertex.Length + 1];
                        delta = SamplePoints[i] - temp;
                        for (int k = 1; k <= SectionVertex.Length; k++)
                        {
                            MeshVertex[index - SectionVertex.Length + k] = MeshVertex[index - SectionVertex.Length + k] + delta;
                        }
                    }
                    Vector3 transformVector = 0.5f * (MeshVertex[index - SectionVertex.Length + 2] - MeshVertex[index - SectionVertex.Length + 1]).normalized;
                    for (int k = 1; k <= SectionVertex.Length; k++)
                    {
                        MeshVertex[index - SectionVertex.Length + k] = MeshVertex[index - SectionVertex.Length + k]+ transformVector;
                    }
                }
            }


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector3 point;
        if(MeshVertex.Length!=0)
        {
            for (int i = 0; i < MeshVertex.Length; i++)
            {
                point = transform.TransformPoint(MeshVertex[i]);
                Gizmos.DrawSphere(transform.TransformPoint(MeshVertex[i]), 0.001f);

            }
            Vector3 lastPoint = transform.TransformPoint(MeshVertex[0]);
            for (int i = 1; i < MeshVertex.Length; i++)
            {
                Vector3 newPoint = transform.TransformPoint(MeshVertex[i]);
                Gizmos.DrawLine(lastPoint, newPoint);
                lastPoint = newPoint;
            }

            Gizmos.color = Color.black;
            for (int i = 0; i < SamplePoints.Length; i++)
            {
                Vector3 newPoint = transform.TransformPoint(SamplePoints[i]);
                Gizmos.DrawLine(newPoint, newPoint + DebugVector[i]);
            }

            Gizmos.color = Color.blue;
            for (int i = 0; i < SamplePoints.Length; i++)
            {
                Vector3 newPoint = transform.TransformPoint(SamplePoints[i]);
                Gizmos.DrawLine(newPoint, newPoint + DebugVector2[i]);
            }

        }
        

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
