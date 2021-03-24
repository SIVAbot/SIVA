﻿using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Gommon;
using Qmmands;
using SixLabors.ImageSharp.PixelFormats;
using Volte.Commands;

namespace Volte.Commands.Modules
{
    public sealed partial class UtilityModule
    {
        [Command("Color", "Colour", "C")]
        [Description("Shows the Hex and RGB representation for a given role in the current guild; or just a color.")]
        public async Task<ActionResult> RoleColorAsync([Remainder, Description("The color you want to see, in #hex or RGB, or a role whose color you want to be shown.")] string colorOrRole)
        {
            var roleTypeParse = await CommandService.GetSpecificTypeParser<SocketRole, RoleParser>().ParseAsync(null, colorOrRole, Context);
            if (roleTypeParse.IsSuccessful)
            {
                var role = roleTypeParse.Value;
                if (!role.HasColor()) return BadRequest("Role does not have a color.");

                return Ok(async () =>
                {
                    await using var stream = role.Color.ToRgba32().CreateColorImage();
                    await stream.SendFileToAsync(Context.Channel, "role.png", "", false, new EmbedBuilder()
                            .WithColor(role.Color)
                            .WithTitle($"{role.Name}'s Color")
                            .WithDescription(new StringBuilder()
                                .AppendLine($"**Hex:** {role.Color.ToString().ToUpper()}")
                                .AppendLine($"**RGB:** {role.Color.R}, {role.Color.G}, {role.Color.B}")
                                .ToString())
                            .WithImageUrl("attachment://role.png")
                            .WithCurrentTimestamp()
                            .Build(),
                        reference: new MessageReference(Context.Message.Id));
                });
            }

            var colorTypeParse = await CommandService.GetSpecificTypeParser<Color, ColorParser>().ParseAsync(null, colorOrRole, Context);
            // ReSharper disable once InvertIf
            if (colorTypeParse.IsSuccessful)
            {
                var color = colorTypeParse.Value;
                return Ok(async () =>
                {
                    await using var stream = color.ToRgba32().CreateColorImage();
                    await stream.SendFileToAsync(Context.Channel, "role.png", "", false, new EmbedBuilder()
                            .WithColor(color)
                            .WithDescription(new StringBuilder()
                                .AppendLine($"**Hex:** {color.ToString().ToUpper()}")
                                .AppendLine($"**RGB:** {color.R}, {color.G}, {color.B}")
                                .ToString())
                            .WithImageUrl("attachment://role.png")
                            .WithCurrentTimestamp()
                            .Build(),
                        reference: new MessageReference(Context.Message.Id));
                });
            }

            return BadRequest("You didn't give a valid role or color.");
        }
    }
}