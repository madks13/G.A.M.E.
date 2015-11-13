using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GAME.Common.Core.Interfaces.Tools;
using System.Text;

namespace GAME.Common.Core.Tools.Finder
{
    public class Finder : IFinder
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private class Folder
        {
            public Folder(String path, Boolean recursive = false)
            {
                if (String.IsNullOrEmpty(path))
                    throw new ArgumentNullException("Path", "Path null or empty");
                Path = path;
                Recursive = recursive;
            }

            public String Path { get; set; }
            public Boolean Recursive { get; set; }
        }

        private List<Folder> _paths = new List<Folder>();

        public Finder(String path, Boolean recursive = false)
        {
            AddPath(path, recursive);
        }

        public Finder(IEnumerable<String> paths, Boolean recursive = false)
        {
            AddPaths(paths, recursive);
        }

        public Finder(IDictionary<String, Boolean> paths)
        {
            AddPaths(paths);
        }

        public List<String> Paths
        {
            get 
            { 
                List<String> l = new List<String>();
                foreach (Folder p in _paths)
                    l.Add(p.Path);
                return l; 
            }
        }

        private FileInfo FindFirstFileInFolders(String searchStr, String path, Boolean recursive)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    String[] files = Directory.GetFiles(path, searchStr, (recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
                    if (files.Length > 0)
                    {
                        log.Info("Found " + files.Length + " files :");
                        foreach (var f in files)
                        {
                            log.Info("\t\t" + f);
                        }
                        return new FileInfo(files[0]);
                    }
                    log.Error("No files found in the path : " + path + ", full path = " + Path.GetFullPath(path));
                }
                catch (System.Exception excpt)
                {
                    log.Fatal("Exception caught. Exception message : " + excpt.Message);
                    Console.WriteLine(excpt.Message);
                }
            }
            log.Error("Path doesn't exist : " + path + ", full path = " + Path.GetFullPath(path));
            return null;
        }

        /// <summary>
        /// Returns the first file found whose name matches the regular expression in sFile
        /// </summary>
        /// <param name="sFile">Regular expression representing the name of files to find</param>
        /// <param name="path">Path to a folder</param>
        /// <param name="recursive">Should subfolders be searched. This will be ignored if "path" is null</param>
        /// <returns></returns>
        public FileInfo FindFile(String sFile, String path = null, Boolean recursive = false)
        {
            if (!String.IsNullOrEmpty(path))
                return FindFirstFileInFolders(sFile, path, recursive);
            foreach(Folder p in _paths)
            {
                FileInfo fi = FindFirstFileInFolders(sFile, p.Path, p.Recursive);
                if (fi != null)
                    return fi;
            }
            return null;
        }

        private List<FileInfo> FindFilesInFolder(String searchStr, String path, Boolean recursive)
        {
            List<FileInfo> l = new List<FileInfo>();
            try
            {
                String[] files = Directory.GetFiles(path, searchStr, (recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
                if (files.Length > 0)
                {
                    log.Info("Found " + files.Length + " files :");
                    foreach (String file in files)
                    {
                        if (file.Contains("cache") || file.Contains("backup"))
                        {
                            log.Info("[Ignored]\t\t" + file);
                        }
                        else
                        {
                            log.Info("[Found]\t\t" + file);
                            l.Add(new FileInfo(file));
                        }
                    }
                }
                else
                    log.Error("No files found in the path : " + path + ", full path = " + Path.GetFullPath(path));
            }
            catch (System.Exception excpt)
            {
                log.Fatal("Exception caught. Exception message : " + excpt.Message);
                Console.WriteLine(excpt.Message);
            }
            return l;
        }

        /// <summary>
        /// Returns all files whose names match the regular expression in sFile
        /// </summary>
        /// <param name="sFile">Regular expression representing the name of files to find</param>
        /// <param name="path">Path to a folder</param>
        /// <param name="recursive">Should subfolders be searched. This will be ignored if "path" is null</param>
        /// <returns></returns>
        public List<FileInfo> FindFiles(String sFile, String path = null, Boolean recursive = false)
        {
            if (!String.IsNullOrEmpty(path))
                return FindFilesInFolder(sFile, path, recursive);
            List<FileInfo> l = new List<FileInfo>();
            foreach(Folder p in _paths)
            {
                l.AddRange(FindFilesInFolder(sFile, p.Path, p.Recursive));
            }
            return l;
        }

        public Boolean AddPath(String path, Boolean recursive = false)
        {
            if (String.IsNullOrEmpty(path))
                return false;
            Folder s = _paths.Find(p => p.Path == path);
            if (s != null)
                s.Recursive = recursive;
            else
                _paths.Add(new Folder(path, recursive));
            log.Info("Path added = " + path);
            return true;
        }

        public Boolean AddPaths(IEnumerable<String> paths, Boolean recursive)
        {
            if (paths == null || paths.Count() == 0)
                return false;
            foreach (var p in paths)
            {
                AddPath(p, recursive);
            }
            return true;
        }

        public Boolean AddPaths(IDictionary<String, Boolean> paths)
        {
            if (paths == null || paths.Count == 0)
                return false;
            foreach (var p in paths)
            {
                AddPath(p.Key, p.Value);
            }
            return true;
        }

        public Boolean DelPath(String path)
        {
            if (String.IsNullOrEmpty(path))
                return false;
            Folder s = _paths.Find(p => p.Path == path);
            if (s == null)
                return false;
            _paths.Remove(s);
            return true;
        }

        public Boolean DelPaths(IEnumerable<String> paths = null)
        {
            if (paths == null)
            {
                _paths.Clear();
                return true;
            }
            if (paths.Count() == 0)
                return false;
            foreach (var p in paths)
            {
                DelPath(p);
            }
            return true;
        }

        public Boolean DelPaths(IDictionary<String, Boolean> paths)
        {
            if (paths == null || paths.Count == 0)
                return false;
            foreach (var p in paths)
            {
                DelPath(p.Key);
            }
            return true;
        }
    }
}
