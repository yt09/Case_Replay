using UnityEngine;
using System.Collections;
using YT_Replay;
using System.Collections.Generic;
using System.IO;

namespace YT_Replay
{
    /// <summary>
    /// 回放管理器类,控制系统回放
    /// </summary>
    public class ReplayManager : MonoSingleton<ReplayManager>
    {
        public int timePos = 0;
        private float startTimePos = 0;

        public Transform target;

        public float replayInterval;

        public bool isReplay = false;
        private RecordObjectInfo replayEntity;

        //使用队列来动态加载位置数据
        protected Queue<RecordObjectInfo> readyToReplayData;

        private void Start()
        {
            readyToReplayData = new Queue<RecordObjectInfo>();
            readyToReplayData.Clear();
            replayInterval = 0.033f;
        }

        private void Update()
        {
            //按下R键回放之前的操作
            if (Input.GetKeyDown(KeyCode.R))
            {
                StopAllCoroutines();
                readyToReplayData.Clear();
                startTimePos = Time.realtimeSinceStartup;
                StartCoroutine(LoadReolayDataFromFile());
                timePos = 0;
                isReplay = true;
            }
            if (isReplay && Time.realtimeSinceStartup - this.startTimePos >= replayInterval)
            {
                timePos++;
                startTimePos = Time.realtimeSinceStartup;

                if (this.readyToReplayData.Count != 0)
                {
                    RecordObjectInfo curState = this.readyToReplayData.Peek();

                    if (curState.TimePos == timePos)
                    {
                        target.transform.position = curState.VectorPos;
                        curState = this.readyToReplayData.Dequeue();
                    }
                }
                else
                {
                    isReplay = false;
                    return;
                }
            }
        }

        //读取记录位置文件的信息
        private IEnumerator LoadReolayDataFromFile()
        {
            string fileName = Application.dataPath + "/TextFile" + "/File.txt";
            FileInfo fileInfo = new FileInfo(fileName);
            string lineData = string.Empty;
            int dataCount = 0;
            if (fileInfo.Exists)
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while ((lineData = sr.ReadLine()) != null)
                    {
                        dataCount++;
                        replayEntity = new RecordObjectInfo();
                        replayEntity.GetTimeAndPosition(lineData);
                        readyToReplayData.Enqueue(replayEntity);
                    }
                }
            }
            yield return null;
        }
    }
}