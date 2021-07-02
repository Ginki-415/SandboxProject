using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������
/// </summary>
public abstract class BaseCube : MonoBehaviour
{
    //====================== �������� ====================
    /// <summary>
    /// �Ƿ�ɼ�
    /// </summary>
    public bool IsVisible { get; private set; } = false;

    /// <summary>
    /// �Ƿ�͸��
    /// </summary>
    public bool IsTransparent { get; private set; } = false;

    /// <summary>
    /// �ܷ�����ײ
    /// </summary>
    public bool IsCollision { get; private set; } = true;

    /// <summary>
    /// �ܷ��ƻ�
    /// </summary>
    public bool IsDamage { get; private set; } = true;

    /// <summary>
    /// �Ƿ�������Ӱ��
    /// </summary>
    public bool IsGravity { get; private set; } = false;

    /// <summary>
    /// ����Ӳ��
    /// </summary>
    public int Hardness { get; private set; } = 1;


    private void Awake()
    {
        InitCube();
    }

    /// <summary>
    /// ��ʼ������
    /// </summary>
    protected abstract void InitCube();

    /// <summary>
    /// ���ÿɼ���
    /// </summary>
    /// <param name="f"></param>
    public virtual void SetVisible(bool f) {
        IsVisible = f;
        transform.GetComponent<MeshRenderer>().enabled = f;
    }

    /// <summary>
    /// ����Ӳ�ȣ�Ӳ��Ϊ0ʱ���鲻���ƻ�
    /// </summary>
    /// <param name="value">���ڻ����0������</param>
    public virtual void SetHardness(int value) {
        if (value < 0) {
            value = Mathf.Abs(value);
            Debug.LogWarning("�����÷���Ӳ��ʱʹ���˸�����");
        }

        Hardness = value;
        IsDamage = Hardness == 0 ? false : true;
    }

    private void OnEnable()
    {
        ResetCube();
    }

    /// <summary>
    /// ���÷���
    /// </summary>
    public abstract void ResetCube();

    /// <summary>
    /// �ƻ�����
    /// </summary>
    public virtual void CubeDestroy() {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        OnCubeDestroy();
    }

    /// <summary>
    /// ���鱻�ƻ�ǰ����õķ���
    /// </summary>
    protected virtual void OnCubeDestroy() {
        
    }

}
