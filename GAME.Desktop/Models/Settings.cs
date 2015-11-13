using GAME.Common.Core.Managers;
using GAME.Common.Core.Models.Settings;
using GAME.Common.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Desktop.Models
{
    public class Settings : Options
    {
        public Settings()
        {
        }

        public Settings(String path) : base(path)
        {
        }

        protected override void BuildDefaultSettings()
        {
            //Add(new Option() { Name = "MuteModule", DisplayName = "Mute", Value = false, Group = "Sound", Info = "This will mute all sounds coming from the main module", ShortInfo = "Mute all sounds"});
            //Add(new Option() { Name = "ReopenModules", DisplayName = "Reopen modules from last session", Value = false, Group = "Session", Info = "This will open any available modules opened in the last session", ShortInfo = "Reopen all modules that were opened in the last session"});
            //Add(new Option() { Name = "ActiveModules", DisplayName = "Modules active in the last session", Value = new List<String>() { "None" }, Group = "Session", Info = "This will keep track of all active modules between sessions", ShortInfo = "Track active modules between sessions", IsReadOnly = true });
            //Add(new Option() { Name = "ModuleFolder", DisplayName = "Module folder", Value = "Modules", Group = "Session", Info = "This is the path to the folder containing all of the modules", ShortInfo = "Path to the module folder", IsReadOnly = true});
            Add(new Option() { Name = "ModuleFolders", DisplayName = "Module folders", Value = new List<String>() { ".\\Modules", "..\\Modules" }, Group = "Session", Info = "This is the list of folders containing all of the modules", ShortInfo = "Paths to the module folders"});
            Add(new Option() { Name = "ModulePattern", DisplayName = "Module pattern", Value = "GAME.Modules.*.dll", Group = "Session", Info = "This is the pattern used to find module assemblies by file name", ShortInfo = "Pattern for module names", IsReadOnly = true});
        }
    }
}
