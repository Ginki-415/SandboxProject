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
        //Chunk Test = new Chunk(horizon);
        //Vector3 testStart = Vector3.zero;
        //TestBuildingChunk(Test, testStart);

        //���Զ��������ɣ�9
        Chunk[] Test = new Chunk[9];
        Vector3 testStart = Vector3.zero;
        int i = 0;
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++, i++)
            {
                Test[i] = new Chunk(horizon);
                TestBuildingChunk(Test[i], new Vector3(testStart.x+(x*Section.Length), testStart.y, testStart.z+(z*Section.Length)), i);
            }
        }
    }

    private void BuildingChunk(Chunk chunk, Vector3 start) 
    {

    }

    /// <summary>
    /// �������������ɷ���
    /// </summary>
    /// <param name="chunk">������Ϣ</param>
    /// <param name="start">��ʼλ������</param>
    private void TestBuildingChunk(Chunk chunk, Vector3 start, int n) 
    {
        GameObject chunkRoot = new GameObject();
        chunkRoot.name = "Chunk " + n + " (x:" + start.x +", z:" + start.z + ")";
        Vector3 curStart;
        for (int i = 0; i < Chunk.Height; i++)
        {
            curStart = new Vector3(start.x, i * 16, start.z);
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

    /// <summary>
    /// ���õ�ƽ�߸߶ȣ���Χ[1,255]
    /// </summary>
    /// <param name="n">��ƽ�߸߶�</param>
    /// <returns>�����ɹ���Ϊtrue</returns>
    public bool SetHorizon(int n ) {
        if (n > 0 && n < 256)
        {
            horizon = n;
            return true;
        }
        else 
        {
            return false;
        }
    }

}
