using UnityEngine;
using System;

namespace YT_Replay
{
    /// <summary>
    /// 绑定在需要记录的物体上,记录外在表现
    /// </summary>
    public class ReplayObject : MonoBehaviour
    {
        [SerializeField]
        private ReplayIdentity replayIdentity = new ReplayIdentity();

        public ReplayIdentity ReplayIdentity
        {
            get
            {
                return this.replayIdentity;
            }
            internal set
            {
                this.replayIdentity = value;
            }
        }

        private void Awake()
        {
            RecordManager.Instance.ReplayObjectTargetList.Add(gameObject);
        }

        /// <summary>
        /// 在Inspector中修改参数值，就会自动调用这个方法
        /// </summary>
        public void OnValidate()
        {
            Reset();
        }

        public void Reset()
        {
            if (!Application.isPlaying)
            {
                Debug.Log("执行 Reset");
                this.replayIdentity.Generate();
            }
        }
    }
}