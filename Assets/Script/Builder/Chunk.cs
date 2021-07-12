using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 分区结构声明
/// </summary>
public class Section
{
    /// <summary>
    /// 分区方块数据
    /// </summary>
    public CubeType[,,] cubes = new CubeType[Length, Length, Length];
    public Dictionary<string, Transform> cubeDatas = new Dictionary<string, Transform>();

    /// <summary>
    /// 分区边长方块数
    /// </summary>
    public static int Length { get; } = 16;

    public Section() { }

    public bool SectionBuilder(int line)
    {
        //实际的第一遍生成世界由分区的初始化进行完成
        //根据地平线与分区的位置关系进行三种不同的初始化
        if (line == Length)
        {
            //地平线不在分区内且高于分区
            for (int y = 0; y < Length; y++)
            {
                for (int x = 0; x < Length; x++)
                {
                    for (int z = 0; z < Length; z++)
                    {
                        cubes[x, z, y] = CubeType.Stone;
                    }
                }
            }
            return true;
        }
        else if (line == 0) 
        {
            //地平线不在分区内且低于分区
            for (int y = 0; y < Length; y++)
            {
                for (int x = 0; x < Length; x++)
                {
                    for (int z = 0; z < Length; z++)
                    {
                        cubes[x, z, y] = CubeType.Air;
                    }
                }
            }
            return false;
        }
        else{
            //地平线位于分区中
            for (int y = 0; y < Length; y++)
            {
                for (int x = 0; x < Length; x++)
                {
                    for (int z = 0; z < Length; z++)
                    {
                        if (line - 3 > 0)
                        {
                            if (y <= line - 3)
                            {
                                cubes[x, y, z] = CubeType.Stone;
                            }
                            else if (y > line)
                            {
                                cubes[x, y, z] = CubeType.Air;
                            }
                            else
                            {
                                cubes[x, y, z] = CubeType.Grass;
                            }
                        }
                        else 
                        {
                            if (y <= line)
                            {
                                cubes[x, y, z] = CubeType.Grass;
                            }
                            else 
                            {
                                cubes[x, y, z] = CubeType.Air;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }

}

/// <summary>
/// 区块结构声明
/// </summary>
public class Chunk
{
    /// <summary>
    /// 当前区块中分区的信息
    /// </summary>
    public Section[] sectionsList = new Section[Height];
    /// <summary>
    /// 标记内容分区，true为非空分区，false为空分区
    /// </summary>
    private bool[] activeList = new bool[Height];

    /// <summary>
    /// 一个区块中的分区数
    /// </summary>
    public static int Height { get; private set; } = 16;

    public bool[] ActiveList 
    {
        get {
            return activeList;
        }
    }

    public Chunk(int horizon)
    {
        //在第一遍的世界生成中区块只负责标记分区状态以及向分区传入地平线信息
        for (int i = 0; i < Height; i++)
        {
            if ((i + 1) * Section.Length < horizon - Section.Length)
            {
                sectionsList[i] = new Section();
                activeList[i] = sectionsList[i].SectionBuilder(Section.Length);
            }
            else if ((i + 1) * Section.Length <= horizon)
            {
                sectionsList[i] = new Section();
                activeList[i] = sectionsList[i].SectionBuilder(horizon - (Section.Length * (i + 1)));
            }
            else 
            {
                sectionsList[i] = new Section();
                activeList[i] = sectionsList[i].SectionBuilder(0);
            }
        }
    }


}