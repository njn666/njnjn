using System;
using SteamKit2;

namespace SteamManager
{
    public class TradeManager
    {
        private readonly SteamClient _client = new SteamClient();
        private CallbackManager _manager;
        private SteamUser _user;
        private SteamUser.LogOnDetails _logOnDetails;

        public TradeManager(Account account)
        {
            _manager = new CallbackManager(_client);
            _user = _client.GetHandler<SteamUser>();
            _logOnDetails = new SteamUser.LogOnDetails
            {
                Username = account.Username,
                Password = account.Password
            };
        }

        public void LoginAndSendOffer()
        {
            _client.Connect();
            _manager.RunCallbacks();
            _user.LogOn(_logOnDetails);
            // TODO: handle callbacks and send trade offer
        }
    }
}
