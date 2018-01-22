using UnityEngine;
using System.Collections;

using System.Text;
using System;

namespace YT_Replay
{
    /// <summary>
    /// 先实现,在优化
    /// 记录一个物体在回放系统中,需要存储的所有信息
    /// </summary>
    public class RecordObjectInfo
    {
        /// <summary>
        /// 场景中需要记录的物体的唯一识别码 集合
        /// </summary>
        public static Hashtable ReplayIdentityHashtable = new Hashtable();

        public int TimePos;

        //位置
        public Vector3 VectorPos;

        //时间和位置转化为字符串
        public static string TimePositionToString(int timePos, Transform target)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0};", timePos);
            sb.AppendFormat("{0},{1},{2}|",
                target.transform.localPosition.x,
                target.transform.localPosition.y,
                target.transform.localPosition.z);
            //Debug.Log(sb.ToString());
            return sb.ToString();
        }

        //分割字符串“；”得到时间及位置字符串
        public void GetTimeAndPosition(string Str)
        {
            string[] TimeAndPos = Str.Split(';');
            TimePos = int.Parse(TimeAndPos[0]);
            this.StringToPosition(TimeAndPos[1]);
        }

        //分割字符串根据“|”分割
        private void StringToPosition(string tarString)
        {
            string[] Contans = tarString.Split('|');
            this.StringToPositionXYZ(Contans[0]);
        }

        //分割储存位置的字符串根据“，”分割
        private void StringToPositionXYZ(string tarString)
        {
            if (string.IsNullOrEmpty(tarString))
                return;
            string[] Pos = tarString.Split(',');
            this.VectorPos.x = float.Parse(Pos[0]);
            this.VectorPos.y = float.Parse(Pos[1]);
            this.VectorPos.z = float.Parse(Pos[2]);
        }

        /// <summary>
        /// 生成唯一随机数
        /// </summary>
        /// <param name="num">需要生成多少个唯一识别码</param>
        /// <returns></returns>
        public static Hashtable GetHashtableRandomNum(int num)
        {
            System.Random random = new System.Random((int)DateTime.Now.Ticks);
            for (int i = 0; ReplayIdentityHashtable.Count < num; i++)//如果ReplayIdentityHashtable.Count 不到 num 会永远循环
            {
                short nValue = (short)random.Next();

                if (!ReplayIdentityHashtable.ContainsValue(nValue))
                {
                    ReplayIdentityHashtable.Add(i, nValue);
                }
                if (i - num >= 10000)
                {
                    throw new OperationCanceledException("生成物体唯一识别码发送错误");
                }
            }

            return ReplayIdentityHashtable;
        }
    }
}