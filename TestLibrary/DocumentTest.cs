using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteSharingApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestLibrary
{
    [TestClass]
    public class DocumentTest
    {
        [TestMethod]
        public bool DownloadAttachmentInt(int id)
        {
            Assert.AreEqual(id, 2);
            return true;
        }
        [TestMethod]
        public bool DownloadAttachmentString(int id)
        {
            Assert.AreEqual(id, "aaaaaaaaaa");
            return true;
        }
        [TestMethod]
        public bool Task(int taskId)
        {
            Assert.AreEqual(taskId, 1);
            return true;
        }
    }
}
