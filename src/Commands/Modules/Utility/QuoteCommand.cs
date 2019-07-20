﻿using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Humanizer;
using Qmmands;
using Volte.Data.Models.Results;
using Volte.Extensions;

namespace Volte.Commands.Modules.Utility
{
    public partial class UtilityModule : VolteModule
    {
        [Command("Quote"), Priority(0)]
        [Description("Quotes a user from a given message's ID.")]
        [Remarks("Usage: |prefix|quote {messageId}")]
        public async Task<VolteCommandResult> QuoteAsync(ulong messageId)
        {
            var m = await Context.Channel.GetMessageAsync(messageId);
            if (m is null)
                return BadRequest("A message with that ID doesn't exist in this channel.");

            var shouldHaveImage = m.Attachments.Count > 0;

            var e = Context.CreateEmbedBuilder($"{m.Content}\n\n[Jump!]({m.GetJumpUrl()})")
                .WithAuthor($"{m.Author.Username}#{m.Author.Discriminator}, in #{m.Channel.Name}",
                    m.Author.GetAvatarUrl())
                .WithFooter(m.Timestamp.Humanize());
            if (shouldHaveImage)
            {
                e.WithImageUrl(m.Attachments.ElementAt(0).Url);
            }

            return Ok(e);
        }

        [Command("Quote"), Priority(1)]
        [Description("Quotes a user in a different chanel from a given message's ID.")]
        [Remarks("Usage: |prefix|quote {messageId}")]
        public async Task<VolteCommandResult> QuoteAsync(SocketTextChannel channel, ulong messageId)
        {
            var m = await channel.GetMessageAsync(messageId);
            if (m is null)
                return BadRequest("A message with that ID doesn't exist in the given channel.");

            var shouldHaveImage = m.Attachments.Count > 0;

            var e = Context.CreateEmbedBuilder($"{m.Content}\n\n[Jump!]({m.GetJumpUrl()})")
                .WithAuthor($"{m.Author.Username}#{m.Author.Discriminator}, in #{m.Channel.Name}",
                    m.Author.GetAvatarUrl())
                .WithFooter(m.Timestamp.Humanize());
            if (shouldHaveImage)
            {
                e.WithImageUrl(m.Attachments.ElementAt(0).Url);
            }

            return Ok(e);
        }
    }
}