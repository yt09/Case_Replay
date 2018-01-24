using UnityEngine;
using System.Collections;
using YT_Replay;
using System;
using System.Collections.Generic;

namespace YT_Replay
{   /// <summary>
    /// ReplayIdentity类生成,相互比较,控制生成唯一 identity 号码
    /// </summary>
    [Serializable]
    public sealed class ReplayIdentity : IEquatable<ReplayIdentity>
    {
        private static List<ReplayIdentity> identities = new List<ReplayIdentity>();

        private static int maxLocateAttempts = 127;

        [SerializeField]
        private short identity = -1;

        public const int unassignedIdentity = -1;

        public static readonly int byteSize = 2;

        public bool IsAssigned
        {
            get
            {
                return this.identity != -1;
            }
        }

        public ReplayIdentity()
        {
            Debug.Log("构造ReplayIdentity");
            Debug.Log(ReplayIdentity.identities.Count);
            if (!ReplayIdentity.identities.Contains(this))
            {
                ReplayIdentity.identities.Add(this);
            }
        }

        internal ReplayIdentity(short id)
        {
            this.identity = id;
        }

        public void Generate()
        {
            if (!Application.isPlaying)
            {
                if (!this.IsAssigned || !ReplayIdentity.IsUnique(this.identity))
                {
                    this.identity = ReplayIdentity.GenerateUnique();
                    return;
                }
            }
            else if (!this.IsAssigned || !ReplayIdentity.IsUnique(this.identity))
            {
                this.identity = ReplayIdentity.GenerateUnique();
            }
        }

        public override int GetHashCode()
        {
            return this.identity.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            ReplayIdentity replayIdentity = obj as ReplayIdentity;
            return !(replayIdentity == null) && this.Equals(replayIdentity);
        }

        public bool Equals(ReplayIdentity obj)
        {
            return !(obj == null) && this.identity == obj.identity;
        }

        public override string ToString()
        {
            return string.Format("{0}", this.identity);
        }

        public static bool operator ==(ReplayIdentity a, ReplayIdentity b)
        {
            return object.Equals(a, b) || a.Equals(b);
        }

        public static bool operator !=(ReplayIdentity a, ReplayIdentity b)
        {
            return !a.Equals(b);
        }

        public static implicit operator short(ReplayIdentity identity)
        {
            return identity.identity;
        }

        public static implicit operator ReplayIdentity(short identity)
        {
            return new ReplayIdentity(identity);
        }

        public static bool IsUnique(ReplayIdentity id)
        {
            return ReplayIdentity.IsUnique((int)id.identity);
        }

        public static bool IsUnique(int id)
        {
            int num = 0;
            using (List<ReplayIdentity>.Enumerator enumerator = ReplayIdentity.identities.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if ((int)enumerator.Current.identity == id)
                    {
                        num++;
                    }
                }
            }
            return num <= 1;
        }

        private static short GenerateUnique()
        {
            short num = 0;
            System.Random random = new System.Random((int)DateTime.Now.Ticks);//int 4字节 | short 2字节 | byte 1字节
            byte[] array = new byte[2];
            while ((int)num <= ReplayIdentity.maxLocateAttempts)
            {
                random.NextBytes(array);
                short num2 = (short)((int)array[0] << 8 | (int)array[1]);
                num += 1;
                if (num2 != -1 && ReplayIdentity.IsUnique(num2))
                {
                    return num2;
                }
            }
            throw new OperationCanceledException("Attempting to find a unique replay id took too long. The operation was canceled to prevent a long or infinite loop");
        }
    }
}