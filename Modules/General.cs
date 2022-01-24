using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace sleepyPanda.Modules
{
    public class General : ModuleBase
    {
        [Command("ping")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("zzz..");
        }

        [Command("owner")]
        public async Task Owner(SocketGuildUser user = null)
        {
            if (user == null)
            {
                var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
                .WithDescription("my owner's info!")
                .WithColor(new Color(177, 156, 217))
                .AddField("name:", Context.User.Username, true)
                .AddField("born:", Context.User.CreatedAt.ToString("MM/dd/yyyy"), true)
                .AddField("joined server on:", (Context.User as SocketGuildUser).JoinedAt.Value.ToString("MM/dd/yyyy"), true)
                .AddField("roles:", string.Join(" ", (Context.User as SocketGuildUser).Roles.Select(x => x.Mention)))
                .WithCurrentTimestamp();

                var embed = builder.Build();
                await Context.Channel.SendMessageAsync(null, false, embed);
            }
            else
            {
                var builder = new EmbedBuilder()
                .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithDescription("my owner's info!")
                .WithColor(new Color(177, 156, 217))
                .AddField("name:", user.Username, true)
                .AddField("born:", user.CreatedAt.ToString("MM/dd/yyyy"), true)
                .AddField("joined server on:", user.JoinedAt.Value.ToString("MM/dd/yyyy"), true)
                .AddField("roles:", string.Join(" ", user.Roles.Select(x => x.Mention)))
                .WithCurrentTimestamp();

                var embed = builder.Build();
                await Context.Channel.SendMessageAsync(null, false, embed);
            }

        }

        [Command("server")]
        public async Task Server()
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithTitle($"about {Context.Guild.Name}...")
                .WithColor(new Color(177, 156, 217))
                .WithImageUrl(Context.User.GetAvatarUrl().ToString())
                .AddField("member count:", (Context.Guild as SocketGuild).MemberCount + " members", true)
                .AddField("online:", (Context.Guild as SocketGuild).Users.Where(x => x.Status != UserStatus.Offline).Count() + " members", true);

            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}
