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
            _client = new DiscordSocketClient();
            
            //config token
            var reader = new JSONReader();
            await reader.ReadJSON();


            await _client.LoginAsync(TokenType.Bot, reader.Token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }
        
    }
}