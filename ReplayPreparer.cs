using UnityEngine;
using System.Collections;
using YT_Replay;
using System;

namespace YT_Replay
{
    /// <summary>
    /// 回放前准备类,根据当前模式(gameplay or replay)
    /// 将其子物体和自身上的脚本开启和关闭,或改变属性（Rigidbody）
    /// </summary>
    public class ReplayPreparer : IReplayPreparer
    {
        private static readonly Type[] skipTypes = new Type[]
             {
            typeof(AudioSource),
            typeof(ParticleSystem)
             };

        private static readonly Type[] disableTypes = new Type[]
        {
            typeof(Collider),
            typeof(Collider2D),
            typeof(Behaviour)
        };

        public virtual void PrepareForPlayback(ReplayObject replayObject)
        {
            Component[] componentsInChildren = replayObject.GetComponentsInChildren<Component>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                Component component = componentsInChildren[i];
                if (!(component is MonoBehaviour))
                {
                    this.PrepareComponentForPlayback(component);
                }
            }
        }

        public virtual void PrepareForGameplay(ReplayObject replayObject)
        {
            Component[] componentsInChildren = replayObject.GetComponentsInChildren<Component>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                Component component = componentsInChildren[i];
                if (!(component is MonoBehaviour))
                {
                    this.PrepareComponentForGameplay(component);
                }
            }
        }

        public virtual void PrepareComponentForPlayback(Component component)
        {
            Type type = component.GetType();
            Type[] array = ReplayPreparer.skipTypes;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == type)
                {
                    return;
                }
            }
            array = ReplayPreparer.disableTypes;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == type && component is Behaviour)
                {
                    (component as Behaviour).enabled = false;
                }
            }
            if (component is Rigidbody)
            {
                (component as Rigidbody).isKinematic = true;
                return;
            }
            if (component is Rigidbody2D)
            {
                (component as Rigidbody2D).isKinematic = true;
            }
        }

        public virtual void PrepareComponentForGameplay(Component component)
        {
            Type type = component.GetType();
            Type[] array = ReplayPreparer.skipTypes;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == type)
                {
                    return;
                }
            }
            array = ReplayPreparer.disableTypes;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == type && component is Behaviour)
                {
                    (component as Behaviour).enabled = true;
                }
            }
            if (component is Rigidbody)
            {
                (component as Rigidbody).isKinematic = false;
                return;
            }
            if (component is Rigidbody2D)
            {
                (component as Rigidbody2D).isKinematic = false;
            }
        }
    }
}