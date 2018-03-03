using System;
using System.Collections.Generic;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("WipeTimer", "531", "1.0.1", ResourceId = 0)]
    [Description("You can check when is wipe")]

    class WipeTimer : CovalencePlugin
    {
        DefaultConfig config;

        #region Configuration
        class DefaultConfig
        {
            public int year = 2017;
            public int month = 1;
            public int day = 13;
            public int hour = 16;
            public int min = 30;
            public int sec = 0;
        }

        protected override void LoadDefaultConfig()
        {
            PrintWarning("Creating a new configuration file");
            Config.Clear();
            config = new DefaultConfig();
            Config.WriteObject(config, true);
            SaveConfig();
        }


        void LoadDefaultMessages()
        {
            //English
            lang.RegisterMessages(new Dictionary<string, string>
            {
                ["CommandWipe"] = "Time to wipe:"
            }, this);
        }

    #endregion

    void Init()
        {
            try
            {
                config = Config.ReadObject<DefaultConfig>();
            }
            catch
            {
                PrintWarning("Could not read config, creating new default config");
                LoadDefaultConfig();
            }
            LoadDefaultMessages();
        }

        [Command("wipe")]
        void cmdWipe(IPlayer p, string command, string[] args)
        {
            DateTime date1 = new DateTime(config.year, config.month, config.day, config.hour, config.min, config.sec);
            System.TimeSpan diff1 = date1.Subtract(DateTime.Now);

            string time = string.Format("{0}d:{1}h:{2}m", diff1.Days, diff1.Hours, diff1.Minutes);
            p.Reply("<color=#ff0000ff>" + Lang("CommandWipe", null) + "</color>");
            p.Reply(time);
        }

        T GetConfig<T>(string name, T value) => Config[name] == null ? value : (T)Convert.ChangeType(Config[name], typeof(T));

        private string Lang(string key, string id = null, params object[] args) => string.Format(lang.GetMessage(key, this, id), args);
    }
}

