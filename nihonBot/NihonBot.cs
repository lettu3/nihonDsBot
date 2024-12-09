using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using NihonBot.config;

namespace NihonBot
{
    public class Program
    {
        private static DiscordSocketClient _client;

        public static async Task Main ()
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.DirectMessages
            };
            _client = new DiscordSocketClient(config);

            _client.Log += Log;
            _client.MessageReceived += HandleMessageAsync;
            // Config token
            var reader = new JSONReader();
            await reader.ReadJSON();

            await _client.LoginAsync(TokenType.Bot, reader.Token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }
        
        private static Task Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }


        private static async Task HandleMessageAsync(SocketMessage message)
        {
            if (message.Author.IsBot) return;

            if (message.Content.ToLower() == "!ping")
            {
                await message.Channel.SendMessageAsync("Pong!");
            }
        }
    }
}