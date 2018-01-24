using UnityEngine;
using System.Collections;
using System;
using YT_Replay;

/// <summary>
///  单例模板类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    _instance = new GameObject("Singleton of " + typeof(T).Name, typeof(T)).GetComponent<T>();
                    _instance.Init();
                }
            }
            return _instance;
        }
    }

    public virtual void Init()
    {
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
    }

    /// <summary>
    /// 在应用退出之前发送给所有的游戏物体。
    ///当用户停止运行模式时在编辑器中调用。当web被关闭时在网络播放器中被调用
    /// iOS程序通常暂停并不退出。
    /// 你应该在Player settings中的iOS编译设置中选择Exit on Suspend（暂停时退出），这会使游戏退出并不会暂停，否则会看不到这个调用。
    /// 如果Exit on Suspend不选择，应该使用OnApplicationPause替代。
    /// </summary>
    private void OnApplicationQuit()
    {
        _instance = null;
    }
}