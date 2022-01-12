using PM.CloudPlatform.ForkliftManager.Apis.Managers;
using System.Threading.Tasks;
using Xunit;

namespace PM.CloudPlatform.ForkliftManager.Test.SessionManager
{
    public class SessionManagerTest
    {
        [Fact]
        public async void Test1()
        {
            SessionManager<TestSession> sessionManager = new();

            for (var i = 0; i < 10; i++)
            {
                await sessionManager.TryAddOrUpdate(i.ToString(), new TestSession());
            }
            Task.WaitAll();
            Assert.True(sessionManager.Count == 10);

            await sessionManager.TryRemove("1");
            Task.WaitAll();
            Assert.True(sessionManager.Count == 9);

            await sessionManager.TryAddOrUpdate("2", new TestSession());
            Task.WaitAll();
            Assert.True(sessionManager.Count == 9);

            await sessionManager.TryAddOrUpdate("1", new TestSession());
            Task.WaitAll();
            Assert.True(sessionManager.Count == 10);

            var session = await sessionManager.TryGet("1");
            Task.WaitAll();
            Assert.Equal(session, sessionManager.Sessions["1"]);

            await sessionManager.TryRemoveAll();
            Task.WaitAll();
            Assert.True(sessionManager.Count == 0);
        }
    }
}