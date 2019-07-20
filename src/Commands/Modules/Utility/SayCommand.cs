﻿using System.Threading.Tasks;
using Discord;
using Qmmands;
using Volte.Data;
using Volte.Data.Models.Results;
using Volte.Extensions;

namespace Volte.Commands.Modules.Utility
{
    public partial class UtilityModule : VolteModule
    {
        [Command("Say")]
        [Description("Bot repeats what you tell it to.")]
        [Remarks("Usage: |prefix|say {msg}")]
        public Task<VolteCommandResult> SayAsync([Remainder] string msg)
        {
            return Ok(msg, _ => Context.Message.DeleteAsync());
        }

        [Command("SilentSay", "Ssay")]
        [Description("Runs the say command normally, but doesn't show the author in the message.")]
        [Remarks("Usage: |prefix|silentsay {msg}")]
        public Task<VolteCommandResult> SilentSayAsync([Remainder] string msg)
        {
            return Ok(new EmbedBuilder()
                .WithColor(Config.SuccessColor)
                .WithDescription(msg), _ => Context.Message.DeleteAsync());
        }
    }
}