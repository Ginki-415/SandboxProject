using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ṹ����
/// </summary>
public class Section
{
    private static int length = 16;
    public CubeType[,,] cube = new CubeType[length, length, length];

    public static int Length 
    {
        get {
            return length;
        }
    }

    public Section() { }

    public bool SectionBuilder(int line)
    {
        //ʵ�ʵĵ�һ�����������ɷ����ĳ�ʼ���������
        //���ݵ�ƽ���������λ�ù�ϵ�������ֲ�ͬ�ĳ�ʼ��
        if (line == length)
        {
            //��ƽ�߲��ڷ������Ҹ��ڷ���
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    for (int z = 0; z < length; z++)
                    {
                        cube[x, z, y] = CubeType.Stone;
                    }
                }
            }
            return true;
        }
        else if (line == 0) 
        {
            //��ƽ�߲��ڷ������ҵ��ڷ���
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    for (int z = 0; z < length; z++)
                    {
                        cube[x, z, y] = CubeType.Air;
                    }
                }
            }
            return false;
        }
        else{
            //��ƽ��λ�ڷ�����
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    for (int z = 0; z < length; z++)
                    {
                        if (line - 3 > 0)
                        {
                            if (y <= line - 3)
                            {
                                cube[x, y, z] = CubeType.Stone;
                            }
                            else if (y > line)
                            {
                                cube[x, y, z] = CubeType.Air;
                            }
                            else
                            {
                                cube[x, y, z] = CubeType.Grass;
                            }
                        }
                        else 
                        {
                            if (y <= line)
                            {
                                cube[x, y, z] = CubeType.Grass;
                            }
                            else 
                            {
                                cube[x, y, z] = CubeType.Air;
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
    /// �����к��еķ�����
    /// </summary>
    private static int height = 16;
    /// <summary>
    /// ��ǰ�����з�������Ϣ
    /// </summary>
    public Section[] sectionsList = new Section[height];
    /// <summary>
    /// ������ݷ�����trueΪ�ǿշ�����falseΪ�շ���
    /// </summary>
    private bool[] activeList = new bool[height];

    public static int Height 
    {
        get {
            return height;
        }
    }

    public bool[] ActiveList 
    {
        get {
            return activeList;
        }
    }

    public Chunk(int horizon)
    {
        //�ڵ�һ�����������������ֻ�����Ƿ���״̬�Լ�����������ƽ����Ϣ
        for (int i = 0; i < height; i++)
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