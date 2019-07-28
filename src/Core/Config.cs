﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Gommon;
using Newtonsoft.Json;
using Volte.Core.Models.BotConfig;

namespace Volte.Core
{
    public sealed class Config
    {
        private const string ConfigFile = "data/config.json";
        private static BotConfig _configuration;

        private static readonly bool IsValidConfig =
            File.Exists(ConfigFile) && !File.ReadAllText(ConfigFile).IsNullOrEmpty();

        static Config()
        {
            CreateIfNotExists();
            if (IsValidConfig)
                _configuration = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText(ConfigFile));
        }

        public static bool CreateIfNotExists()
        {
            if (IsValidConfig) return true;
            _configuration = new BotConfig
            {
                Token = "token here",
                CommandPrefix = "$",
                Owner = 0,
                Game = "in Volte V2 Code!",
                Streamer = "streamer here",
                EnableDebugLogging = false,
                SuccessEmbedColor = 0x7000FB,
                ErrorEmbedColor = 0xFF0000,
                LogAllCommands = true,
                JoinLeaveLog = new JoinLeaveLog(),
                BlacklistedServerOwners = new ulong[] { },
                EnabledFeatures = new EnabledFeatures()
            };
            try
            {
                File.WriteAllText(ConfigFile,
                    JsonConvert.SerializeObject(_configuration, Formatting.Indented));
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public static string Token => _configuration.Token;

        public static string CommandPrefix => _configuration.CommandPrefix;

        public static ulong Owner => _configuration.Owner;

        public static string Game => _configuration.Game;

        public static string Streamer => _configuration.Streamer;

        public static bool EnableDebugLogging => _configuration.EnableDebugLogging;

        public static string FormattedStreamUrl => $"https://twitch.tv/{Streamer}";

        public static uint SuccessColor => _configuration.SuccessEmbedColor;

        public static uint ErrorColor => _configuration.ErrorEmbedColor;

        public static bool LogAllCommands => _configuration.LogAllCommands;

        public static JoinLeaveLog JoinLeaveLog => _configuration.JoinLeaveLog;

        public static IEnumerable<ulong> BlacklistedOwners => _configuration.BlacklistedServerOwners;

        public static EnabledFeatures EnabledFeatures => _configuration.EnabledFeatures;

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private struct BotConfig
        {
            [JsonProperty("discord_token")]
            public string Token { get; internal set; }

            [JsonProperty("command_prefix")]
            public string CommandPrefix { get; internal set; }

            [JsonProperty("bot_owner")]
            public ulong Owner { get; internal set; }

            [JsonProperty("status_game")]
            public string Game { get; internal set; }

            [JsonProperty("status_twitch_streamer")]
            public string Streamer { get; internal set; }

            [JsonProperty("enable_debug_logging")]
            public bool EnableDebugLogging { get; internal set; }

            [JsonProperty("color_success")]
            public uint SuccessEmbedColor { get; internal set; }

            [JsonProperty("color_error")]
            public uint ErrorEmbedColor { get; internal set; }

            [JsonProperty("log_all_commands")]
            public bool LogAllCommands { get; internal set; }

            [JsonProperty("join_leave_log")]
            public JoinLeaveLog JoinLeaveLog { get; internal set; }

            [JsonProperty("blacklisted_guild_owners")]
            public ulong[] BlacklistedServerOwners { get; internal set; }

            [JsonProperty("enabled_features")]
            public EnabledFeatures EnabledFeatures { get; internal set; }
        }
    }
}