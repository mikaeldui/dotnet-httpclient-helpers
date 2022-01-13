using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        [TestMethod]
        public void FromEntryAssembly()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null)
                Assert.Inconclusive();
            else
            {
                var agent = UserAgent.From(entryAssembly);
                Assert.IsNotNull(agent);
            }
        }

        [TestMethod]
        public void FromThisAssembly()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            if (executingAssembly == null)
                Assert.Inconclusive();
            else
            {
                var agent = UserAgent.From(executingAssembly);
                Assert.IsNotNull(agent);
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
