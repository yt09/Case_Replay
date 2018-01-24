using UnityEngine;
using System.Collections;

namespace YT_Replay
{
    /// <summary>
    /// 回放前准备接口
    /// </summary>
    public interface IReplayPreparer
    {
        /// <summary>
        /// 回放前准备
        /// </summary>
        void PrepareForPlayback(ReplayObject replayObject);

        /// <summary>
        /// 游戏播放前准备
        /// </summary>
        void PrepareForGameplay(ReplayObject replayObject);
    }
}