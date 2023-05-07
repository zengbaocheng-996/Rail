using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshOperate : MonoBehaviour
{
    void Start()
    {
        GameObject obj =GameObject.Find("meshObject");
        MeshFilter mf = obj.GetComponent<MeshFilter>();
        MeshRenderer mr = obj.GetComponent<MeshRenderer>();

        Vector3[] ptsArr1 = new Vector3[5];
        ptsArr1[0].Set(0.0f, 0.0f, 0.0f);
        ptsArr1[1].Set(0.0f, 1.0f, 0.0f);
        ptsArr1[2].Set(3.0f, 1.0f, 0.0f);
        ptsArr1[3].Set(3.0f, 0.0f, 0.0f);
        ptsArr1[4].Set(0.0f, 0.0f, 0.0f);

        Vector3[] ptsArr2 = new Vector3[6];
        ptsArr2[0].Set(4.0f, 0.0f, 0.0f);
        ptsArr2[1].Set(4.0f, 2.0f, 0.0f);
        ptsArr2[2].Set(6.0f, 2.0f, 0.0f);
        ptsArr2[3].Set(10.0f, -1.0f, 0.0f);
        ptsArr2[4].Set(8.0f, -0.5f, 0.0f);
        ptsArr2[5].Set(4.0f, 0.0f, 0.0f);

        List<int> indices1 = new List<int>();
        CalIndices(ptsArr1, 0, indices1);
        Debug.Log(indices1);

        List<int> indices2 = new List<int>();
        CalIndices(ptsArr2, ptsArr1.Length, indices2);
        Debug.Log(indices2);

        List<int> indicesTotal = new List<int>();
        indicesTotal.AddRange(indices1);
        indicesTotal.AddRange(indices2);

        List<Vector3> ptsTotal = new List<Vector3>();
        ptsTotal.AddRange(ptsArr1);
        ptsTotal.AddRange(ptsArr2);

        mf.mesh.vertices = ptsTotal.ToArray();
        mf.mesh.SetIndices(indicesTotal.ToArray(), MeshTopology.Lines, 0);
    }

    void CalIndices(Vector3[] ptsArr, int startIndex, List<int> indiceArr)
    {
        //int[] indiceArr1 = new int[2 * ptsArr.Length];
        //int k = 0;
        for (int i = startIndex; i < startIndex + ptsArr.Length - 1; i++)
        {
            indiceArr.Add(i);
            indiceArr.Add(i + 1);
        }

        indiceArr.Add(startIndex + ptsArr.Length - 1);
        indiceArr.Add(startIndex);
    }

}
//// 输入：需要合并 mesh 的 GameObjects 列表
//public static Mesh Combine(List<GameObject> meshes)
//{
//    // 获取需要合并的 mesh
//    CombineInstance[] combine=new CombineInstance[meshes.Count];
//    for(int i=0;i<meshes.Count;i++) 
//    {
//        MeshFilter filter = meshes[i].GetComponent<MeshFilter>();
//        combine[i].mesh = filter.sharedMesh;
//        combine[i].transform=filter.transform.localToWorldMatrix;
//    }
//    // 合并为一个新的 mesh
//    Mesh newMesh = new Mesh();
//    newMesh.CombineMeshes(combine);
//    return newMesh;
//}    void Start()