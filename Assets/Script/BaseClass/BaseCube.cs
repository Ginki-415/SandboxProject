using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方块基类
/// </summary>
public abstract class BaseCube : MonoBehaviour
{
    //====================== 基础属性 ====================
    /// <summary>
    /// 是否可见
    /// </summary>
    public bool IsVisible { get; private set; } = false;

    /// <summary>
    /// 是否透明
    /// </summary>
    public bool IsTransparent { get; private set; } = false;

    /// <summary>
    /// 能否发生碰撞
    /// </summary>
    public bool IsCollision { get; private set; } = true;

    /// <summary>
    /// 能否破坏
    /// </summary>
    public bool IsDamage { get; private set; } = true;

    /// <summary>
    /// 是否受重力影响
    /// </summary>
    public bool IsGravity { get; private set; } = false;

    /// <summary>
    /// 方块硬度
    /// </summary>
    public int Hardness { get; private set; } = 1;


    private void Awake()
    {
        InitCube();
    }

    /// <summary>
    /// 初始化方块
    /// </summary>
    protected abstract void InitCube();

    /// <summary>
    /// 设置可见性
    /// </summary>
    /// <param name="f"></param>
    public virtual void SetVisible(bool f) {
        IsVisible = f;
        transform.GetComponent<MeshRenderer>().enabled = f;
    }

    /// <summary>
    /// 设置硬度，硬度为0时方块不可破坏
    /// </summary>
    /// <param name="value">大于或等于0的整数</param>
    public virtual void SetHardness(int value) {
        if (value < 0) {
            value = Mathf.Abs(value);
            Debug.LogWarning("在设置方块硬度时使用了负数！");
        }

        Hardness = value;
        IsDamage = Hardness == 0 ? false : true;
    }

    private void OnEnable()
    {
        ResetCube();
    }

    /// <summary>
    /// 重置方块
    /// </summary>
    public abstract void ResetCube();

    /// <summary>
    /// 破坏方块
    /// </summary>
    public virtual void CubeDestroy() {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        OnCubeDestroy();
    }

    /// <summary>
    /// 方块被破坏前会调用的方法
    /// </summary>
    protected virtual void OnCubeDestroy() {
        
    }

}
