using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace DiscordBotSurvivor.Commands
{
    public class DebugCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("returns pong")]
        public async Task Ping(CommandContext context)
        {
            await context.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [Command("add")]
        [Description("returns sub of two numbers")]
        [RequireRoles(RoleCheckMode.All, "Survivor")]
        public async Task AddTwoNumbers(CommandContext context,
            [Description("number 1")] int number1, [Description("number 2")] int number2)
        {
            string sum = $"{number1 + number2}";
            await context.Channel.SendMessageAsync(sum).ConfigureAwait(false);
        }

        [Command("respondmessage")]
        public async Task ResponseMessage(CommandContext context)
        {
            InteractivityExtension interactivity = context.Client.GetInteractivity();

            var message =
                await interactivity.WaitForMessageAsync(x => x.Channel == context.Channel).ConfigureAwait(false);

            await context.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("respondreaction")]
        public async Task ResponseReaction(CommandContext context)
        {
            InteractivityExtension interactivity = context.Client.GetInteractivity();

            var message =
                await interactivity.WaitForReactionAsync(x => x.Channel == context.Channel).ConfigureAwait(false);

            await context.Channel.SendMessageAsync(message.Result.Emoji);
        }

        [Command("join")]
        public async Task Join(CommandContext context)
        {
            DiscordEmbedBuilder.EmbedThumbnail thumbnail = new DiscordEmbedBuilder.EmbedThumbnail();
            thumbnail.Url = context.Client.CurrentUser.AvatarUrl;
            thumbnail.Height = 100;
            thumbnail.Width = 100;

            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Would you like to join?",
                Thumbnail = thumbnail,
                Color = DiscordColor.Green
            };

            var joinMessage = await context.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

            var thumbsUpEmoji = DiscordEmoji.FromName(context.Client, ":+1:");
            var thumbsDownEmoji = DiscordEmoji.FromName(context.Client, ":-1:");

            await joinMessage.CreateReactionAsync(thumbsUpEmoji).ConfigureAwait(false);
            await joinMessage.CreateReactionAsync(thumbsDownEmoji).ConfigureAwait(false);

            var interactivity = context.Client.GetInteractivity();

            var reactionResult = await interactivity.WaitForReactionAsync(
                x => x.Message == joinMessage &&
                x.User == context.User &&
                (x.Emoji == thumbsUpEmoji || x.Emoji == thumbsDownEmoji)).ConfigureAwait(false);

            if (reactionResult.Result.Emoji == thumbsUpEmoji)
            {
                var role = context.Guild.GetRole(909331568881983538);
                await context.Member.GrantRoleAsync(role).ConfigureAwait(false);
            }
            else if (reactionResult.Result.Emoji == thumbsUpEmoji)
            {
                var role = context.Guild.GetRole(909331568881983538);
                await context.Member.RevokeRoleAsync(role).ConfigureAwait(false); // FIXME: this doesn't seem to work
            }
            else
            {
                // something went wrong
            }

            await joinMessage.DeleteAsync().ConfigureAwait(false);
        }

        [Command("poll")]
        public async Task Poll(CommandContext context, TimeSpan duration, params DiscordEmoji[] emoijiOptions)
        {
            var interactivity = context.Client.GetInteractivity();

            var options = emoijiOptions.Select(x => x.ToString());

            var embed = new DiscordEmbedBuilder
            {
                Title = "Poll",
                Description = string.Join(" ", options)
            };

            var pollMessage = await context.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);

            foreach (var option in emoijiOptions)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
            var distinctResult = result.Distinct();

            var results = distinctResult.Select(x =>$"{x.Emoji}: {x.Total}");

            await context.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);
        }


        [Command("messagerole")]
        public async Task MessageRole(CommandContext context)
        {
            var interactivity = context.Client.GetInteractivity();

            var role = context.Guild.GetRole(909331568881983538);

            await context.Member.SendMessageAsync("asdasd").ConfigureAwait(false);

            foreach (var member in context.Guild.Members)
            {
                // 
            }
        }
    }
}
