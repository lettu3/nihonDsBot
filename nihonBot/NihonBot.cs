﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NihonBot.config;
using NihonBot.Commands;

namespace NihonBot
{
    public class Program
    {
        private static DiscordSocketClient? _client;
        private static CommandService? _commands;
        private static IServiceProvider? _services;

        static void Main(string[] args) => RunAsync().GetAwaiter().GetResult();

        public static async Task RunAsync()
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.Guilds 
                               | GatewayIntents.GuildMessages 
                               | GatewayIntents.DirectMessages 
                               | GatewayIntents.MessageContent
            };

            //client
            _client = new DiscordSocketClient(config);
            _commands = new CommandService();
            _services = new ServiceCollection()
                        .AddSingleton(_client)
                        .AddSingleton(_commands)
                        .AddSingleton<LoggingService>()
                        .AddSingleton<CommandHandlerService>()
                        .BuildServiceProvider();

            var loggingService = _services.GetRequiredService<LoggingService>();
            var commandHandler = _services.GetRequiredService<CommandHandlerService>();
            await commandHandler.InitializeAsync();
            // Config token
            var reader = new JSONReader();
            await reader.ReadJSON();

            await _client.LoginAsync(TokenType.Bot, reader.Token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }        
    }
}