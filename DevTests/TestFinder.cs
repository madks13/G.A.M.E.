using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GAME.Common.Core.Interfaces.Tools;
using GAME.Common.Core.Tools.Finder;
using System.IO;
using System.Collections.Generic;

namespace DevTests
{
    [TestClass]
    public class TestFinder
    {
        private IFinder _finder;

        [TestMethod]
        public void NonRecursivePath()
        {
            _finder = new Finder("..\\..\\..\\Modules\\Debug");
            Assert.AreEqual("..\\..\\..\\Modules\\Debug", _finder.Paths[0], "Path comparison failed : \r\n Tested : {0} \r\n Finder : {1}\r\n", "..\\..\\..\\Modules\\Debug", _finder.Paths[0]);
        }

        [TestMethod]
        public void NonRecursiveFile()
        {
            _finder = new Finder("..\\..\\..\\Modules\\Debug");
            FileInfo t = new FileInfo("E:\\Projects\\VS\\Warframe Activity Manager\\Modules\\Debug\\WAM.Modules.AlertScanner.dll");
            FileInfo f = _finder.FindFile("WAM.Modules.*.dll");
            Assert.AreEqual(t.FullName, f.FullName, "FileInfo comparison failed : \r\n Tested : {0} \r\n Finder : {1}\r\n", t.FullName, f.FullName);
        }
        [TestMethod]
        public void NonRecursiveFiles()
        {
            _finder = new Finder("..\\..\\..\\Modules\\Debug");
            List<String> lt = new List<String>(Directory.GetFiles("E:\\Projects\\VS\\Warframe Activity Manager\\Modules\\Debug", "WAM.Modules.*.dll"));
            List<FileInfo> lf = _finder.FindFiles("WAM.Modules.*.dll");
            Assert.AreEqual(lt.Count, lf.Count, "File List comparison failed : \r\n Tested : {0} \r\n Finder : {1}\r\n", lt.Count, lf.Count);
        }

        [TestMethod]
        public void RecursivePath()
        {
            _finder = new Finder("..\\..\\..\\Modules\\Debug", true);
            Assert.AreEqual("..\\..\\..\\Modules\\Debug", _finder.Paths[0], "Path comparison failed : \r\n Tested : {0} \r\n Finder : {1}\r\n", "..\\..\\..\\Modules\\Debug", _finder.Paths[0]);
        }

        [TestMethod]
        public void RecursiveFile()
        {
            _finder = new Finder("..\\..\\..\\Modules\\Debug", true);
            FileInfo t = new FileInfo("E:\\Projects\\VS\\Warframe Activity Manager\\Modules\\Debug\\WAM.Modules.AlertScanner.dll");
            FileInfo f = _finder.FindFile("WAM.Modules.*.dll");
            Assert.AreEqual(t.FullName, f.FullName, "FileInfo comparison failed : \r\n Tested : {0} \r\n Finder : {1}\r\n", t.FullName, f.FullName);
        }
        [TestMethod]
        public void RecursiveFiles()
        {
            _finder = new Finder("..\\..\\..\\Modules\\Debug", true);
            List<String> lt = new List<String>(Directory.GetFiles("E:\\Projects\\VS\\Warframe Activity Manager\\Modules\\Debug", "WAM.Modules.*.dll", SearchOption.AllDirectories));
            List<FileInfo> lf = _finder.FindFiles("WAM.Modules.*.dll");
            Assert.AreEqual(lt.Count, lf.Count, "File List comparison failed : \r\n Tested : {0} \r\n Finder : {1}\r\n", lt.Count, lf.Count);
        }
    }
}
