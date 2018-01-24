using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

/// <summary>
/// 单例模板类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : Singleton<T>
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //检查类中是否存在公有构造函数信息数组
                ConstructorInfo[] cons = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public);
                if (cons.Length > 0)
                    throw new Exception(typeof(T).Name + "Can Find Public Constructor");
                //在类中查找非公有的构造函数信息数组
                cons = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                //查找无参数的构造函数
                ConstructorInfo con = Array.Find(cons, ct => ct.GetParameters().Length == 0);
                if (con == null)
                    throw new Exception("Cannot find Non-public ctor()");
                _instance = con.Invoke(null) as T;
                _instance.Init();
            }
            return _instance;
        }
    }

    public virtual void Init()
    {
    }

    private void OnApplicationQuit()
    {
        _instance = null;
    }
}