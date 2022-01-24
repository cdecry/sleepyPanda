using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace sleepyPanda.Services
{
    public class StartUpService
    {
        public static IServiceProvider _provider;
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;

        public StartUpService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands, IConfiguration config)
        {
            _provider = provider;
            _discord = discord;
            _commands = commands;
            _config = config;
        }

        public async Task StartAsync()
        {
            string token = _config["tokens:discord"];
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Please provide Discord token in _config.yml");
                return;
            }

            await _discord.LoginAsync(TokenType.Bot, token);
            await _discord.StartAsync();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }
    }
}
