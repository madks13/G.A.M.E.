using GAME.Common.Core.Interfaces;
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
            BuildDefaultSettings();
        }

        public Settings(String path) : base(path)
        {
            BuildDefaultSettings();
        }

        protected override void BuildDefaultSettings()
        {
            if (this["MuteModule"] == null)
                Add(new Option() { Name = "MuteModule", DisplayName = "Mute", Value = false, Group = "Sound", Info = "This will mute all sounds coming from the main module", ShortInfo = "Mute all sounds" });
            if (this["ReopenModules"] == null)
                Add(new Option() { Name = "ReopenModules", DisplayName = "Reopen modules from last session", Value = false, Group = "Session", Info = "This will open any available modules opened in the last session", ShortInfo = "Reopen all modules that were opened in the last session" });
            if (this["ActiveModules"] == null)
                Add(new Option() { Name = "ActiveModules", DisplayName = "Modules active in the last session", Value = new List<String>() { "None" }, Group = "Session", Info = "This will keep track of all active modules between sessions", ShortInfo = "Track active modules between sessions", IsReadOnly = true });
            if (this["ModuleFolders"] == null)
                Add(new Option() { Name = "ModuleFolders", DisplayName = "Module folders", Value = new List<String>() { ".\\Modules", "..\\Modules" }, Group = "Session", Info = "This is the list of folders containing all of the modules", ShortInfo = "Paths to the module folders" });
            if (this["ModulePattern"] == null)
                Add(new Option() { Name = "ModulePattern", DisplayName = "Module pattern", Value = "GAME.Modules.*.dll", Group = "Session", Info = "This is the pattern used to find module assemblies by file name", ShortInfo = "Pattern for module names", IsReadOnly = true});
        }
    }
}
