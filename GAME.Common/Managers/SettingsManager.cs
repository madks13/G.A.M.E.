using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using GAME.Common.Core.Models.Settings;
using GAME.Common.Core.Tools.Serializer;
using System.IO;

namespace GAME.Common.Core.Managers
{
    class OptionsMapModel
    {
        public Options Options { get; set; }
        public String Path { get; set; }
    }

    public class SettingsManager
    {
        #region Fields

        private String _propertiesFilePath = String.Empty;
        private Options _optionsAll = new Options();
        private Options _orphans = new Options();
        private List<OptionsMapModel> _optionsMap = new List<OptionsMapModel>();

        #endregion

        #region Properties

        public Options Settings
        {
            get
            {
                return _optionsAll;
            }
        }

        #endregion

        #region Methods

        #region C/Dtor

        public SettingsManager()
        {
        }

        public SettingsManager(IEnumerable<Option> options, String propertiesFilePath)
        {
            _optionsAll.AddRange(options);
        }

        #endregion

        #region Indexer

        public Option this[String key]
        {
            get
            {
                return this._optionsAll[key];
            }
            set
            {
                this._optionsAll[key] = value;
            }
        }

        #endregion

        #region Loading

        public void Load(String propertiesFilePath)
        {
            LoadList(propertiesFilePath);
        }

        private void LoadList(String path)
        {
            if (path != null && File.Exists(path))
            {
                var map = new OptionsMapModel() { Path = path, Options = Serializer<Options>.Deserialize(_propertiesFilePath) };
                _optionsMap.Add(map);
                _optionsAll.AddRange(map.Options);
            }
        }

        #endregion

        #region Saving

        public void Save()
        {
            foreach(var list in _optionsMap)
            {
                Serializer<Options>.Serialize(list.Path, list.Options);
            }
        }

        public void Save(String propertiesFilePath, Boolean includeNonOrphans = false)
        {

            if (!String.IsNullOrEmpty(propertiesFilePath))
            {
                if (includeNonOrphans)
                    Serializer<Options>.Serialize(propertiesFilePath, _optionsAll);
                else
                    Serializer<Options>.Serialize(propertiesFilePath, _orphans);
            }
        }

        #endregion

        #region Adding

        private void AddList(IEnumerable<Option> options, String path)
        {
            _optionsAll.AddRange(options);
            
            var map = new OptionsMapModel() { Path = path, Options = new Options(options) };

            _optionsMap.Add(map);
        }

        public SettingsManager Add(Option option)
        {
            if (option != null)
            {
                _optionsAll[option.Name] = option;
            }

            return this;
        }

        #endregion

        #region Removing

        public SettingsManager Remove(String key)
        {
            if (!String.IsNullOrEmpty(key))
            {
                _optionsAll.Remove(_optionsAll[key]);
            }

            return this;
        }

        public SettingsManager Remove(Option option)
        {
            return this;
        }

        #endregion

        #region Modification

        public void ResetAll()
        {
            this._optionsAll.ResetAll();
        }

        #endregion

        #endregion
    }
}
