using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 世界生成器
/// </summary>
public class WorldBuilder : Singleton<WorldBuilder>
{
    // 世界区块数据缓存
    private Dictionary<string, Chunk> chunks = new Dictionary<string, Chunk>();
    // 实际生成世界边区块数
    private int mapLength = 0;

    /// <summary>
    /// 世界大小
    /// </summary>
    public int WorldSize = 2;

    /// <summary>
    /// 地平线高度
    /// </summary>
    public int Horizon { get; private set; } = 62;

    private void Awake()
    {
        InitMapLength();

        //测试多区块生成
        BuildingChunk();
    }

    /// <summary>
    /// 检查区块中的各方块可见性进行剔除
    /// </summary>
    /// <param name="chunk">目标区块</param>
    /// <param name="x">在区块map上的x坐标</param>
    /// <param name="y">在区块map上的y坐标</param>
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
    /// 测试用区块生成方法
    /// </summary>
    /// <param name="chunk">区块信息</param>
    /// <param name="start">起始位置坐标</param>
    private void TestBuildingChunk(Chunk chunk, Vector2 start, int n) 
    {
        GameObject chunkRoot = new GameObject();
        chunkRoot.name = "Chunk " + n + " (x:" + start.x +", y:" + start.y + ")";
        Vector3 curStart;
        for (int i = 0; i < Chunk.Height; i++)
        {
            curStart = new Vector3(start.x, i * 16, start.y);
            print("=================初始化测试分区： " + (i + 1) + "号=================");
            print("分区坐标为：x-" + curStart.x + "，y-" + (i * 16) + "，z-" + curStart.y);
            if (!chunk.ActiveList[i])
            {
                print("当前分区为空分区不执行生成");
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
    /// 设置地平线高度，范围[1,255]
    /// </summary>
    /// <param name="n">地平线高度</param>
    /// <returns>操作成功则为true</returns>
    public bool SetHorizon(int n ) {
        if (n > 0 && n < 256)
        {
            Horizon = n;
            return true;
        }
        else 
        {
            Debug.Log("高度值不在限制范围内");
            return false;
        }
    }

}
