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
        [TestMethod]
        public void PostNotesView()
        {
            var controller = new DocumentsController();
            var result = controller.PostNotes("Notes", 1) as ViewResult;
            Assert.AreEqual("Notes", result.ViewName);

        }
        [TestMethod]
        public void PostNotesViewInt()
        {
            var controller = new DocumentsController();
            var result = controller.PostNotes(1, 1) as ViewResult;
            Assert.AreEqual(1, result.ViewName);

        }
    }
}
