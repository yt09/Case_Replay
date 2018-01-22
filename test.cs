using UnityEngine;
using System.Collections;
using YT_Replay;

public class test : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        RecordObjectInfo.GetHashtableRandomNum(10);
        foreach (var item in RecordObjectInfo.ReplayIdentityHashtable.Values)
        {
            Debug.Log(item);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}