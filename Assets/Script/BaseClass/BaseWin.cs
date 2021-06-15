using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������
/// </summary>
public abstract class BaseWin : MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    protected WinType winType = WinType.NULL;
    /// <summary>
    /// ���������
    /// </summary>
    protected WinOpenType winOpenType = WinOpenType.NULL;
    /// <summary>
    /// ��һ���򿪵Ĵ�������
    /// </summary>
    protected WinType lastWinType = WinType.NULL;

    public WinOpenType WinOpenType{
        get {
            return winOpenType;
        }
    }

    /// <summary>
    /// ��ʼ����������
    /// </summary>
    protected abstract void InitializeWin();

    void Awake()
    {
        InitializeWin(); 
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        print("===================== " + "OPEN WIN ��" + winType + " ======================");
    }

    /// <summary>
    /// �򿪴��崥��
    /// </summary>
    protected abstract void OpenWin();

    /// <summary>
    /// ����¼������������򿪴���󴥷���ִ��˳������OpenWin()
    /// </summary>
    protected abstract void AddEvent();

    private void OnEnable()
    {
        OpenWin();
        AddEvent();
    }

    /// <summary>
    /// �رմ��崥��
    /// </summary>
    protected abstract void CloseWin();

    /// <summary>
    /// �Ƴ��¼������������رմ���ǰ������ִ��˳������CloseWin()
    /// </summary>
    protected abstract void RemoveEvent();

    private void OnDisable()
    {
        RemoveEvent();
        CloseWin();
    }

    void Update()
    {
        
    }
}