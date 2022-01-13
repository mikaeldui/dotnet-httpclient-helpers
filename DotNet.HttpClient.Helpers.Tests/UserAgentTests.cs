using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Net.Http.Tests
{
    [TestClass]
    public class UserAgentTests
    {
        [TestMethod]
        public void Construct()
        {
            _ = _getUserAgents();
        }

        [TestMethod]
        public void AddToUserAgent()
        {
            var agents = _getUserAgents();

            foreach(var agent in agents)
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add(agent);
            }
        }

        [TestMethod]
        public void CheckAddedUserAgents()
        {
            var agents = _getUserAgents();

            foreach (var agent in agents)
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add(agent);

                Assert.AreEqual(client.DefaultRequestHeaders.UserAgent.ToString(), agent.ToString());
            }
        }


        private static UserAgent[] _getUserAgents() => new UserAgent[]
        {
            new("hello"),
            new("hello", "1.2.3.4+1a2b3c"),
            new("hello", "1.2.3.4+1a2b3c", new string[] { "world", "cat" }),
            new("hello", "1.2.3.4+1a2b3c", new string[] { "world", "cat" }, new("hello"))
        };
    }
}
