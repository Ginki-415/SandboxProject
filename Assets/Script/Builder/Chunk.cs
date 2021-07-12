using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ṹ����
/// </summary>
public class Section
{
    /// <summary>
    /// ������������
    /// </summary>
    public CubeType[,,] cubes = new CubeType[Length, Length, Length];
    public Dictionary<string, Transform> cubeDatas = new Dictionary<string, Transform>();

    /// <summary>
    /// �����߳�������
    /// </summary>
    public static int Length { get; } = 16;

    public Section() { }

    public bool SectionBuilder(int line)
    {
        //ʵ�ʵĵ�һ�����������ɷ����ĳ�ʼ���������
        //���ݵ�ƽ���������λ�ù�ϵ�������ֲ�ͬ�ĳ�ʼ��
        if (line == Length)
        {
            //��ƽ�߲��ڷ������Ҹ��ڷ���
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
            //��ƽ�߲��ڷ������ҵ��ڷ���
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
            //��ƽ��λ�ڷ�����
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
/// ����ṹ����
/// </summary>
public class Chunk
{
    /// <summary>
    /// ��ǰ�����з�������Ϣ
    /// </summary>
    public Section[] sectionsList = new Section[Height];
    /// <summary>
    /// ������ݷ�����trueΪ�ǿշ�����falseΪ�շ���
    /// </summary>
    private bool[] activeList = new bool[Height];

    /// <summary>
    /// һ�������еķ�����
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
        //�ڵ�һ�����������������ֻ�����Ƿ���״̬�Լ�����������ƽ����Ϣ
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