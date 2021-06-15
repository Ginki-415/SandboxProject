using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������
/// </summary>
public class WinsManager : Singleton<WinsManager>
{
    /// <summary>
    /// ������
    /// </summary>
    private Stack<WinType> showedWins = new Stack<WinType>();
    /// <summary>
    /// �򿪹��Ĵ��建��
    /// </summary>
    private Dictionary<WinType, GameObject> allShowedWins = new Dictionary<WinType, GameObject>();
    /// <summary>
    /// ��ǰ������ʾ�Ĵ���
    /// </summary>
    private Dictionary<WinType, GameObject> showingWins = new Dictionary<WinType, GameObject>();

    //��ʼ������layer���ڵ�
    private Transform Layer_Full_Root;
    private Transform Layer_Fly_Root;
    private Transform Layer_Top_Root;

    private void Awake()
    {
        Layer_Full_Root = GameObject.Find("Layer_Full").transform;
        Layer_Fly_Root = GameObject.Find("Layer_Fly").transform;
        Layer_Top_Root = GameObject.Find("Layer_Top").transform;
    }

    public void OpenWin(WinType winType) {
        OpenWin(winType, WinOpenType.FULL);
    }

    public void OpenWin(WinType winType, WinOpenType winOpenType) {
        if (CheckIsShowing(winType))
        {
            //����������ʾ�����
            return;
        }
        else if (CheckIsShowed(winType))
        {
            //����򿪹������
            GameObject curWin = null;
            allShowedWins.TryGetValue(winType, out curWin);
            if (curWin != null)
            {
                curWin.gameObject.SetActive(true);
                showingWins.Add(winType, curWin);
            }
        }
        else {
            //����δ�򿪹������
            string winPath = WinsPrefabPath.Instance.GetPath(winType);
            GameObject curWin = Resources.Load<GameObject> (winPath);
            Transform curWinRoot = GetWinRoot(winOpenType);
            Instantiate(curWin, curWinRoot);
            showingWins.Add(winType, curWin);
            allShowedWins.Add(winType, curWin);
        }
    }

    public void CloseWin(WinType winType) 
    {
        if (CheckIsShowing(winType))
        {
            GameObject curWin = null;
            showingWins.TryGetValue(winType, out curWin);
            if (curWin != null) {
                curWin.SetActive(false);
                showingWins.Remove(winType);
            }
        }
        else 
        {
            return;
        }
    }

    /// <summary>
    /// ��鴰���Ƿ�������ʾ
    /// </summary>
    /// <param name="winType">Ŀ�괰������</param>
    /// <returns></returns>
    public bool CheckIsShowing(WinType winType) {
        return showingWins.ContainsKey(winType);
    }

    /// <summary>
    /// ��鴰���Ƿ���ʾ��
    /// </summary>
    /// <param name="winType">Ŀ�괰������</param>
    /// <returns></returns>
    public bool CheckIsShowed(WinType winType)
    {
        return allShowedWins.ContainsKey(winType);
    }

    public Transform GetWinRoot(WinOpenType winOpenType) {
        switch (winOpenType)
        {
            case WinOpenType.FULL:
                return Layer_Full_Root;
            case WinOpenType.FLY:
                return Layer_Fly_Root;
            case WinOpenType.TOP:
                return Layer_Top_Root;
            default:
                break;
        }
        Debug.LogError("========== δ�ҵ���Ӧ��������͵ĸ���� ===========");
        return null;
    }

}

