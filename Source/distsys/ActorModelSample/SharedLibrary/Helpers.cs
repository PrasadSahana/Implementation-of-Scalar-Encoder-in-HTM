using AkkaSb.Net;
using System;

namespace SharedLibrary
{
    public class Helpers
    {
        private const string sbConnStr = "Endpoint=sb://bastasample.servicebus.windows.net/;SharedAccessKeyName=demo;SharedAccessKey=MvwVbrrJdsMQyhO/0uwaB5mVbuXyvYa3WRNpalHi0LQ=";

        public static ActorSbConfig GetClientConfig(string rcvQueueName = "distsys/actorsystem/rcv1")
        {
            ActorSbConfig cfg = new ActorSbConfig();
            cfg.SbConnStr = sbConnStr;
            cfg.ReplyMsgQueue = rcvQueueName;
            cfg.RequestMsgQueue = "distsys/actorsystem/actorqueue";

            return cfg;
        }

        public static ActorSbConfig GetHostConfig()
        {
            ActorSbConfig cfg = new ActorSbConfig();
            cfg.SbConnStr = sbConnStr;
            cfg.RequestMsgQueue = "distsys/actorsystem/actorqueue";
            cfg.ReplyMsgQueue = null;

            return cfg;
        }
    }
}
