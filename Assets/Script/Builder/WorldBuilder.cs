using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������
/// </summary>
public class WorldBuilder : Singleton<WorldBuilder>
{
    // �����������ݻ���
    private Dictionary<string, Chunk> chunks = new Dictionary<string, Chunk>();
    // ʵ�����������������
    private int mapLength = 0;

    /// <summary>
    /// �����С
    /// </summary>
    public int WorldSize = 2;

    /// <summary>
    /// ��ƽ�߸߶�
    /// </summary>
    public int Horizon { get; private set; } = 62;

    private void Awake()
    {
        InitMapLength();

        //���Զ���������
        BuildingChunk();
    }

    /// <summary>
    /// ��������еĸ�����ɼ��Խ����޳�
    /// </summary>
    /// <param name="chunk">Ŀ������</param>
    /// <param name="x">������map�ϵ�x����</param>
    /// <param name="y">������map�ϵ�y����</param>
    private void CheckVisible(Chunk chunk, int x, int y)
    {
        for (int n = 0; n < Chunk.Height; n++) {
            Section curSection = chunk.sectionsList[n];

            for (int i = 0; i < Section.Length; i++) {
                for (int j = 0; j < Section.Length; j++)
                {
                    for (int k = 0; k < Section.Length; k++)
                    {
                        CubeType curCube = curSection.cubes[i, j, k];
                        if (curCube == CubeType.Air) continue;
                        //transform.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }

        }
    }


    private void BuildingChunk() 
    {
        int n = 0;
        for (int i = 0; i < mapLength; i++) {
            for (int j = 0; j < mapLength; j++) {
                Chunk chunk = new Chunk(Horizon);
                chunks.Add(i+","+j, chunk);
                Vector2 start = new Vector2(i*Section.Length, j*Section.Length);
                TestBuildingChunk(chunk, start, ++n);
                CheckVisible(chunk, i, j);
            }
        }
    }

    /// <summary>
    /// �������������ɷ���
    /// </summary>
    /// <param name="chunk">������Ϣ</param>
    /// <param name="start">��ʼλ������</param>
    private void TestBuildingChunk(Chunk chunk, Vector2 start, int n) 
    {
        GameObject chunkRoot = new GameObject();
        chunkRoot.name = "Chunk " + n + " (x:" + start.x +", y:" + start.y + ")";
        Vector3 curStart;
        for (int i = 0; i < Chunk.Height; i++)
        {
            curStart = new Vector3(start.x, i * 16, start.y);
            print("=================��ʼ�����Է����� " + (i + 1) + "��=================");
            print("��������Ϊ��x-" + curStart.x + "��y-" + (i * 16) + "��z-" + curStart.y);
            if (!chunk.ActiveList[i])
            {
                print("��ǰ����Ϊ�շ�����ִ������");
                continue;
            }

            GameObject sectionRoot = new GameObject();
            sectionRoot.transform.SetParent(chunkRoot.transform);
            sectionRoot.name = "Section " + i;
            CubeType[,,] curCubeList = chunk.sectionsList[i].cubes;
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

                        if (curCubeType == CubeType.Air)continue;

                        string curPath = CubePath.Instance.GetPath(curCubeType);
                        GameObject curObj = Resources.Load<GameObject>(curPath);
                        Transform curTransform = Instantiate(curObj).transform;
                        curTransform.position = new Vector3(curStart.x + x, curStart.y + y, curStart.z + z);
                        curTransform.SetParent(planeRoot.transform);
                    }
                }
            }
        }
    }

    private void InitMapLength() {
        mapLength = 1 + ((WorldSize-1) * 2);
    }

    /// <summary>
    /// ���õ�ƽ�߸߶ȣ���Χ[1,255]
    /// </summary>
    /// <param name="n">��ƽ�߸߶�</param>
    /// <returns>�����ɹ���Ϊtrue</returns>
    public bool SetHorizon(int n ) {
        if (n > 0 && n < 256)
        {
            Horizon = n;
            return true;
        }
        else 
        {
            Debug.Log("�߶�ֵ�������Ʒ�Χ��");
            return false;
        }
    }

}
