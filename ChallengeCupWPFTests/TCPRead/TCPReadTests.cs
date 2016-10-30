using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChallengeCupWPF.TCPRead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ChallengeCupWPF.TCPRead.Tests
{
    [TestClass()]
    public class TCPReadTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            // Notice: Start a server first
            // Server sample is in LearnWPF/TCPServer
            TCPRead.Read();
            Assert.AreEqual(TCPRead.data, 1500.0f);
        }
    }

}