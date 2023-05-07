using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class GenerateMesh_1 : MonoBehaviour
{
    public int xGridCount, yGridCount, zGridCount;
    private Vector3[] _vertices;
    private int[] _triangles;
    private int t = 0, v = 0;
    public float _r;
    private Vector3[] _normals;
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
        mesh.name = "Mesh01";
        StartCoroutine(GenerateMesh(mesh));
    }

    private IEnumerator GenerateMesh(Mesh mesh)
    {
        yield return StartCoroutine(GenerateVertex());
        _normals = new Vector3[_vertices.Length];
        Vector3 inner;
        for(int i=0;i<_vertices.Length;i++)
        {
            inner = GetNormal(i);
            GetVertexPos(i, inner);
        }
        mesh.vertices = _vertices;

        yield return StartCoroutine(GenerateTriangle(mesh));
        
    }

    private int GenerateVertexCount()
    {
        int connerVertexCount = 8;
        int edgeVertexCount = (xGridCount + yGridCount + zGridCount - 3) * 4;
        int faceVertexCount = (xGridCount - 1) * (yGridCount - 1) * 2
                            + (zGridCount - 1) * (yGridCount - 1) * 2
                            + (xGridCount - 1) * (zGridCount - 1) * 2;
        return connerVertexCount+edgeVertexCount+faceVertexCount;
    }

    private IEnumerator GenerateVertex()
    {
        _vertices = new Vector3[GenerateVertexCount()];
        int index = 0;
        for(int y=0;y<yGridCount+1; y++)
        {
            for (int x = 0; x < xGridCount + 1; x++, index++)
            {
                _vertices[index] = new Vector3(x, y, 0);
                yield return null;
            }
            for (int z = 1; z < zGridCount + 1; z++, index++)
            {
                _vertices[index] = new Vector3(xGridCount, y, z);
                yield return null;
            }
            for (int x = xGridCount - 1; x >= 0; x--, index++)
            {
                _vertices[index] = new Vector3(x, y, zGridCount);
                yield return null;
            }
            for (int z = zGridCount - 1; z > 0; z--, index++)
            {
                _vertices[index] = new Vector3(0, y, z);
                yield return null;
            }
        }
        for (int z = 1; z <zGridCount; z++)
        {
            for (int x = 1; x < xGridCount; x++, index++)
            {
                _vertices[index] = new Vector3(x, yGridCount, z);
                yield return null;
            }
            yield return null;
        }

        for (int z = 1; z < zGridCount; z++)
        {
            for (int x = 1; x < xGridCount; x++, index++)
            {
                _vertices[index] = new Vector3(x, 0, z);
                yield return null;
            }
            yield return null;
        }
    }
    private int GetTriangleCount()
    {
        return xGridCount*yGridCount*4+xGridCount*zGridCount*4+yGridCount*zGridCount*4;
    }
    private IEnumerator GenerateTriangle(Mesh mesh)
    {
        _triangles = new int[GetTriangleCount()*3];
        int circleVertexCount = 2 * xGridCount + 2 * zGridCount;
    
        yield return StartCoroutine(GenerateSide(mesh,circleVertexCount));
        yield return StartCoroutine(GenerateTop(mesh, circleVertexCount));
        yield return StartCoroutine(GenerateBottom(mesh, circleVertexCount));
    }

    private IEnumerator GenerateSide(Mesh mesh,int circleVertexCount)
    {
        for (int y = 0; y < yGridCount; y++)
        {
            for (int i = 0; i < circleVertexCount; i++, v++, t += 6)
            {
                SetQuad(_triangles, t, v, v + 1, v + circleVertexCount, v+1+circleVertexCount,circleVertexCount);
                mesh.triangles = _triangles;
                yield return null;
            }
        }
    }

    private IEnumerator GenerateTop(Mesh mesh, int circleVertexCount)
    {
        // 第一行前三个面
        for(int x=0;x<xGridCount-1;x++, v++, t += 6)
        {
            SetQuad(_triangles, t, v, v + 1, v + circleVertexCount-1, v + circleVertexCount);
            mesh.triangles = _triangles;
            yield return null;
        }
        int vMin = circleVertexCount * (yGridCount + 1) - 1;
        int vMid = vMin + 1;
        int vMax = v + 2;

        // 第一行第四个面
        SetQuad(_triangles, t, v, v + 1, v + circleVertexCount - 1, v + 2);
        mesh.triangles = _triangles;
        t += 6;
        yield return null;

        // 中间行所有面
        for (int z=0;z<zGridCount-2;z++,vMin--,vMid++,vMax++)
        {
            // 第一个面
            SetQuad(_triangles, t, vMin, vMid, vMin - 1, vMid + xGridCount);
            mesh.triangles = _triangles;
            t += 6;
            yield return null;

            // 中间面片
            
            for (int i = 0; i < xGridCount - 2; i++, t += 6, vMid++)
            {
                SetQuad(_triangles, t, vMid, vMid + 1, vMid + xGridCount - 1, vMid + xGridCount);
                mesh.triangles = _triangles;
                yield return null;
            }
            // 最后一个面
            SetQuad(_triangles, t, vMid, vMax, vMid + xGridCount - 1, vMax+1);
            mesh.triangles = _triangles;
            t += 6;
            yield return null;
        }

        int vTop = vMin - 2;
        // 第一个面
        SetQuad(_triangles, t, vMin, vMid, vMin - 1, vTop);
        mesh.triangles = _triangles;
        t += 6;
        yield return null;

        for(int i=0;i<xGridCount-2;i++,vMid++,vTop--)
        {        
            // 中间面
            SetQuad(_triangles, t, vMid, vMid+1, vTop, vTop - 1);
            mesh.triangles = _triangles;
            t += 6;
            yield return null;
        }
        // 最后 面
        SetQuad(_triangles, t, vMid, vTop-2, vTop, vTop-1);
        mesh.triangles = _triangles;
        t += 6;
        yield return null;
    }

    private IEnumerator GenerateBottom(Mesh mesh, int circleVertexCount)
    {
        int vMin = circleVertexCount - 1;
        int vMid = _vertices.Length - (xGridCount - 1) * (zGridCount - 1);
        // 第一行 第一面
        SetQuad(_triangles, t, vMin, vMid, 0, 1);
        t += 6;
        mesh.triangles = _triangles;
        yield return null;

        int vMax = 1;
        // 第一行 中间面
        for (int i = 0; i < xGridCount - 2;i++,t+=6,vMax++,vMid++)
        {
            SetQuad(_triangles, t, vMid, vMid + 1, vMax, vMax + 1);
            mesh.triangles = _triangles;
            yield return null;
        }

        //第一行 最后面
        SetQuad(_triangles, t, vMid, vMax + 2, vMax, vMax + 1);
        t+= 6;
        mesh.triangles = _triangles;
        yield return null;

        vMid++;
        vMax += 2;

        for (int z = 0; z < zGridCount - 2; z++, vMin--, vMid++, vMax++)
        {
            // 第一面
            SetQuad(_triangles, t, vMin - 1, vMid, vMin, vMid - xGridCount + 1);
            t += 6;
            mesh.triangles = _triangles;
            yield return null;

            // 中间面
            for (int i = 0; i < xGridCount - 2; i++, t += 6, vMid++)
            {
                SetQuad(_triangles, t, vMid, vMid + 1, vMid - xGridCount + 1, vMid - xGridCount + 2);
                mesh.triangles = _triangles;
                yield return null;
            }

            // 最后面
            SetQuad(_triangles, t, vMid, vMax + 1, vMid - xGridCount + 1, vMax);
            t += 6;
            mesh.triangles = _triangles;
            yield return null;
        }
         
        vMid = vMid - xGridCount + 1;
        // 最后行 第一面
        SetQuad(_triangles, t, vMin - 1, vMin - 2, vMin, 1);
        t += 6;
        mesh.triangles = _triangles;
        yield return null;

        int vBottom = vMin - 2;
        // 第一行 中间面
        for (int i = 0; i < xGridCount - 2; i++, t += 6, vBottom--, vMid++)
        {
            SetQuad(_triangles, t, vBottom, vBottom -  1, vMid, vMid + 1);
            mesh.triangles = _triangles;
            yield return null;
        }

        // 最后行 最后面
        SetQuad(_triangles, t, vBottom, vBottom - 1, vMid, vBottom - 2);
        t += 6;
        mesh.triangles = _triangles;
        yield return null;
    }

    private void SetQuad(int[] triangles,int i,int v00,int v10,int v01,int v11,int circleVertexCount)
    {
        v10 = (v00 / circleVertexCount)*circleVertexCount+v10%circleVertexCount;
        v11 = (v01 / circleVertexCount)*circleVertexCount+ v11 % circleVertexCount;

        SetQuad(triangles, i, v00, v10, v01, v11);
    }


    private void SetQuad(int[] _triangles, int i, int v00, int v10, int v01, int v11)
    {
        _triangles[i] = v00;
        _triangles[i + 1] = v01;
        _triangles[i + 2] = v10;

        _triangles[i + 3] = v01;
        _triangles[i + 4] = v11;
        _triangles[i + 5] = v10;
    }


    private void OnDrawGizmos()
    {
        if (_vertices == null) return;
        Vector3 point;
        for(int i=0;i<_vertices.Length;i++)
        {
            point = transform.TransformPoint(_vertices[i]);
            Gizmos.DrawSphere(transform.TransformPoint(_vertices[i]), 0.1f);
            if (_normals != null && i < _normals.Length)
                Gizmos.DrawRay(point, _normals[i]);
        }
    }

    // 圆角四边形 部分逻辑
    private Vector3 GetNormal(int i)
    {
        var vertex= _vertices[i];
        var inner = vertex;
        if (vertex.x < _r)
        {
            inner.x = _r;
        }
        else if (vertex.x>xGridCount-_r)
        {
            inner.x=xGridCount-_r;
        }

        if (vertex.y < _r)
        {
            inner.y = _r;
        }
        else if (vertex.y > yGridCount - _r)
        {
            inner.y = yGridCount - _r;
        }

        if (vertex.z < _r)
        {
            inner.z = _r;
        }
        else if (vertex.z > zGridCount - _r)
        {
            inner.z = zGridCount - _r;
        }
        _normals[i] = (vertex - inner).normalized;
        return inner;
    }

    private void GetVertexPos(int i, Vector3 inner)
    {
        _vertices[i]=_normals[i] * _r + inner;
    }

}
