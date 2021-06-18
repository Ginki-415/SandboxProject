using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������
/// </summary>
public class WorldBuilder : Singleton<WorldBuilder>
{
    /// <summary>
    /// ��ƽ�߸߶�
    /// </summary>
    private int horizon = 62;
    
    /// <summary>
    /// ��ƽ�߸߶�
    /// </summary>
    public int Horizon 
    {
        get {
            return horizon;
        }
    }

    private void Awake()
    {
        // ��������������
        Chunk Test = new Chunk(horizon);
        Vector3 testStart = Vector3.zero;
        TestBuildingChunk(Test, testStart);
    }

    private void BuildingChunk(Chunk chunk, Vector3 start) 
    {

    }

    /// <summary>
    /// �������������ɷ���
    /// </summary>
    /// <param name="chunk">�����������Ϣ</param>
    /// <param name="start">��ʼλ������</param>
    private void TestBuildingChunk(Chunk chunk, Vector3 start) 
    {
        GameObject chunkRoot = new GameObject();
        chunkRoot.name = "Chunk 0 (x:0, y:0)";
        Vector3 curStart = Vector3.zero;
        for (int i = 0; i < Chunk.Height; i++)
        {
            curStart = new Vector3(curStart.x, i * 16, curStart.z);
            print("=================��ʼ�����Է����� " + (i + 1) + "��=================");
            print("��������Ϊ��x-" + curStart.x + "��y-" + curStart.y + "��z-" + curStart.z);
            if (!chunk.ActiveList[i])
            {
                print("��ǰ����Ϊ�շ�����ִ������");
                continue;
            }

            GameObject sectionRoot = new GameObject();
            sectionRoot.transform.SetParent(chunkRoot.transform);
            sectionRoot.name = "Section " + i;
            CubeType[,,] curCubeList = chunk.sectionsList[i].cube;
            for (int y = 0; y < Section.Length; y++)
            {
                GameObject planeRoot = new GameObject();
                planeRoot.transform.SetParent(sectionRoot.transform);
                planeRoot.name = "Plane " + y;
                for (int x = 0; x < Section.Length; x++)
                {
                    for (int z = 0; z < Section.Length; z++)
                    {
                        CubeType curCubeType = curCubeList[x, y, z];
                        if (curCubeType == CubeType.Air)
                        {
                            continue;
                        }
                        string curPath = CubePath.Instance.GetPath(curCubeType);
                        GameObject curObj = Resources.Load<GameObject>(curPath);
                        Transform transform = Instantiate(curObj).transform;
                        transform.position = new Vector3(curStart.x + x, curStart.y + y, curStart.z + z);
                        transform.SetParent(planeRoot.transform);
                    }
                }
            }
        }
    }
}
