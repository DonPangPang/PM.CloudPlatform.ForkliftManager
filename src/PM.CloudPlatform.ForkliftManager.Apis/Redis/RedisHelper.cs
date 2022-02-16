using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PM.CloudPlatform.ForkliftManager.Apis.Redis
{
    public class RedisHelper : IDisposable
    {
        private string _connectionString;

        private string _instanceName;

        private int _defaultDb;

        private const string RedisKeyHeader = "ForkLift_";

        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;

        public RedisHelper(string connectionString, string instanceName, int defaultDb = 0)
        {
            _connectionString = connectionString;
            _instanceName = instanceName;
            _defaultDb = defaultDb;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }

        private ConnectionMultiplexer GetConnect()
        {
            return _connections.GetOrAdd(_instanceName, p => ConnectionMultiplexer.Connect(_connectionString));
        }

        public IDatabase GetDatabase()
        {
            return GetConnect().GetDatabase(_defaultDb);
        }

        public IDatabase GetDatabase(int db = 0)
        {
            return GetConnect().GetDatabase(db);
        }

        public IServer GetServer(string? configName = null, int endPointsIndex = 0)
        {
            var confOption = ConfigurationOptions.Parse(_connectionString);
            return GetConnect().GetServer(confOption.EndPoints[endPointsIndex]);
        }

        public ISubscriber GetSubscriber(string? configName = null)
        {
            return GetConnect().GetSubscriber();
        }

        public async Task SetStringAsync(RedisKey key, RedisValue value)
        {
            await GetDatabase().StringSetAsync($"{RedisKeyHeader}{key}", value);
        }

        public async Task SetStringAsync(int db, RedisKey key, RedisValue value)
        {
            await GetDatabase(db).StringSetAsync($"{RedisKeyHeader}{key}", value);
        }

        public async Task<string> GetStringAsync(RedisKey key)
        {
            return await GetDatabase().StringGetAsync($"{RedisKeyHeader}{key}");
        }

        public async Task<string> GetStringAsync(int db, RedisKey key)
        {
            return await GetDatabase(db).StringGetAsync($"{RedisKeyHeader}{key}");
        }

        public async Task<string> SetOrUpdateAsync<T>(RedisKey key, RedisValue value)
        {
            return await GetDatabase().StringGetSetAsync($"{RedisKeyHeader}{key}", value);
        }

        public async Task<string> SetOrUpdateAsync<T>(int db, RedisKey key, RedisValue value)
        {
            return await GetDatabase(db).StringGetSetAsync($"{RedisKeyHeader}{key}", value);
        }

        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach (var item in _connections.Values)
                {
                    item.Close();
                }
            }
        }
    }
}