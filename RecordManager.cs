using UnityEngine;
using System.Collections;
using System.IO;
using YT_Replay;

namespace YT_Replay
{
    /// <summary>
    /// 记录管理类,控制什么时候开始记录
    /// </summary>
    public class RecordManager : MonoBehaviour
    {
        //时间点 记录次数
        public int timePos;

        //记录的目标
        public GameObject target;

        //开始记录的时间
        public float startRecordTime;

        //txt路径名称
        private string txtFileName;

        //记录间隔
        private float recordInterval;

        //判断位置是否改变
        protected Vector3 cachePosition = new Vector3(1000.0f, 0f, 0f);

        //流文件
        private StreamWriter streamWriter;

        private RecordObjectInfo myRecordObjectInfo = new RecordObjectInfo();

        private void Start()
        {
            txtFileName = Application.dataPath + "/TextFile" + "/File.txt";
            CreatTxt();
            recordInterval = 0.033f;
            startRecordTime = 0.0f;
        }

        //生成文件
        private void CreatTxt()
        {
            if (!File.Exists(txtFileName))
            {
                streamWriter = new StreamWriter(txtFileName);
            }
            else
            {
                streamWriter = new StreamWriter(txtFileName);
                Debug.Log("streamWriter Is Exist!");
            }
        }

        //写入文件
        private void WriteData(string saveData)
        {
            Debug.Log(saveData);
            streamWriter.WriteLine(saveData);
        }

        //结束写入
        private void Finished()
        {
            streamWriter.Flush();
            streamWriter.Close();
        }

        private void Update()
        {
            //timePos = Time.realtimeSinceStartup*100;
            string curDataStr = string.Empty;
            if (Time.realtimeSinceStartup - startRecordTime >= recordInterval)
            {
                Debug.Log("记录");
                timePos++;
                curDataStr = RecordObjectInfo.TimePositionToString(timePos, target.transform);
                if (!cachePosition.Equals(target.transform.localPosition))
                {
                    WriteData(curDataStr);
                    this.cachePosition = this.target.transform.localPosition;
                }
                this.startRecordTime = Time.realtimeSinceStartup;
            }
            //按下E键停止记录
            if (Input.GetKeyDown(KeyCode.E))
            {
                Finished();
                enabled = false;
            }
        }
    }
}