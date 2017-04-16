// Reference: Oxide.Core.MySql

using System;
using System.IO;
using System.Collections.Generic;
using System.Timers;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core;

namespace Oxide.Plugins
{
    [Info("WipeTimer", "531devv", "1.0.1", ResourceId = 531)]
    [Description("You can check the time to wipe")]

    class WipeTimer : HurtworldPlugin
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
        }

        [ChatCommand("wipe")]
        void cmdWipe(PlayerSession p, string command, string[] args)
        {
            DateTime date1 = new DateTime(config.year, config.month, config.day, config.hour, config.min, config.sec);
            System.TimeSpan diff1 = date1.Subtract(DateTime.Now);

            string time = string.Format("{0}d:{1}h:{2}m", diff1.Days, diff1.Hours, diff1.Minutes);

            hurt.SendChatMessage(p, "<color=#FF0000>Do konca wipe zostalo:</color>");
            hurt.SendChatMessage(p, time);

        }
        T GetConfig<T>(string name, T value) => Config[name] == null ? value : (T)Convert.ChangeType(Config[name], typeof(T));
    }
}

