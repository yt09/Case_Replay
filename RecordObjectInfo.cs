using UnityEngine;
using System.Collections;

using System.Text;

namespace YT_Replay
{
    /// <summary>
    /// 先实现,在优化
    ///
    ///
    /// 记录一个物体在回放系统中,需要存储的所有信息
    /// </summary>
    public class RecordObjectInfo
    {
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
    }
}