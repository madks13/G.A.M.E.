using System;
using System.IO;
using System.Collections.Generic;

namespace GAME.Common.Core.Interfaces.Tools
{
    public interface IFinder
    {
        List<String> Paths { get; }

        Boolean AddPath(String path, Boolean recursive = false);

        Boolean AddPaths(IEnumerable<String> paths, Boolean recursive);

        Boolean AddPaths(IDictionary<String, Boolean> paths);

        Boolean DelPath(String path);

        Boolean DelPaths(IEnumerable<String> paths = null);

        Boolean DelPaths(IDictionary<String, Boolean> paths);

        FileInfo FindFile(String file, String path = null, Boolean recursive = false);

        List<FileInfo> FindFiles(String file, String path = null, Boolean recursive = false);
    }
}
