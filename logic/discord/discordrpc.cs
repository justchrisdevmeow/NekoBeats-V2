using DiscordRPC;

namespace NekoBeats
{
    public class DiscordRPC
    {
        private DiscordRpcClient client;
        
        public DiscordRPC()
        {
            // Application ID
            client = new DiscordRpcClient("1483867520665387178");
            
            client.OnReady += (user) =>
            {
                Console.WriteLine("Discord RPC Connected!");
            };
            
            client.Initialize();
        }
        
        public void UpdateStatus()
        {
            client.SetPresence(new RichPresence()
            {
                Details = "Visualizing Audio",
                State = "Playing with NekoBeats v2.3.2",
                Assets = new Assets()
                {
                    LargeImageKey = "nekobeats_logo",
                    LargeImageText = "NekoBeats Audio Visualizer"
                }
            });
        }
        
        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
