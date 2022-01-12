using PM.CloudPlatform.ForkliftManager.Apis.Sessions;
using SuperSocket;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace PM.CloudPlatform.ForkliftManager.Apis.Managers
{
    /// <summary>
    /// Session管理器
    /// </summary>
    public class SessionManager<TSession> where TSession : IAppSession
    {
        /// <summary>
        /// 存储的Session
        /// </summary>
        public ConcurrentDictionary<string, TSession> Sessions { get; private set; } = new();

        /// <summary>
        /// Session的数量
        /// </summary>
        public int Count => Sessions.Count;

        /// <summary>
        /// </summary>
        public SessionManager()
        {
        }

        public ConcurrentDictionary<string, TSession> GetAllSessions()
        {
            return Sessions;
        }

        /// <summary>
        /// 获取一个Session
        /// </summary>
        /// <param name="key"> </param>
        /// <returns> </returns>
        public virtual async Task<TSession> TryGet(string key)
        {
            return await Task.Run(() =>
            {
                Sessions.TryGetValue(key, out var session);
                return session;
            });
        }

        /// <summary>
        /// 添加或者更新一个Session
        /// </summary>
        /// <param name="key">     </param>
        /// <param name="session"> </param>
        /// <returns> </returns>
        public virtual async Task TryAddOrUpdate(string key, TSession session)
        {
            await Task.Run(() =>
            {
                if (Sessions.TryGetValue(key, out var oldSession))
                {
                    Sessions.TryUpdate(key, session, oldSession);
                }
                else
                {
                    Sessions.TryAdd(key, session);
                }
            });
        }

        /// <summary>
        /// 移除一个Session
        /// </summary>
        /// <param name="key"> </param>
        /// <returns> </returns>
        public virtual async Task TryRemove(string key)
        {
            await Task.Run(() =>
            {
                if (Sessions.TryRemove(key, out var session))
                {
                }
                else
                {
                }
            });
        }

        /// <summary>
        /// 通过Session移除Session
        /// </summary>
        /// <param name="sessionId"> </param>
        /// <returns> </returns>
        public virtual async Task TryRemoveBySessionId(string sessionId)
        {
            await Task.Run(() =>
            {
                foreach (var session in Sessions)
                {
                    if (session.Value.SessionID == sessionId)
                    {
                        Sessions.TryRemove(session);
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// 删除僵尸链接
        /// </summary>
        /// <returns> </returns>
        [Obsolete("该方法丢弃", true)]
        public virtual async Task TryRemoveZombieSessions()
        {
            await Task.Run(() =>
            {
            });
        }

        /// <summary>
        /// 移除所有Session
        /// </summary>
        /// <returns> </returns>
        public virtual async Task TryRemoveAll()
        {
            await Task.Run(() =>
            {
                Sessions.Clear();
            });
        }

        /// <summary>
        /// </summary>
        /// <param name="session"> </param>
        /// <param name="buffer">  </param>
        /// <returns> </returns>
        public virtual async Task SendAsync(TSession session, ReadOnlyMemory<byte> buffer)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            await session.SendAsync(buffer);
        }

        /// <summary>
        /// </summary>
        /// <param name="session"> </param>
        /// <param name="message"> </param>
        /// <returns> </returns>
        public virtual async Task SendAsync(ClientSession session, string message)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            // ReSharper disable once PossibleNullReferenceException
            await session.SendAsync(message);
        }

        /// <summary>
        /// </summary>
        /// <param name="session"> </param>
        /// <returns> </returns>
        public virtual async Task<Guid> FindIdBySession(TSession session)
        {
            return await Task.Run(() =>
            {
                return Guid.Parse(Sessions.First(x => x.Value.SessionID.Equals(session.SessionID)).Key);
            });
        }
    }
}