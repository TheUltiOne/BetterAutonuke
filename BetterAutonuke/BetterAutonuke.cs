using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;

using Server = Exiled.Events.Handlers.Server;

using System.Collections.Generic;
using System;

namespace BetterAutonuke
{
    public class BetterAutonuke : Plugin<Config>
    {
        private static readonly Lazy<BetterAutonuke> LazyInstance = new Lazy<BetterAutonuke>(valueFactory: () => new BetterAutonuke());
        public static BetterAutonuke Instance => LazyInstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        private EventHandlers events;

        public static List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();

        private BetterAutonuke()
        {
        }

        public override void OnEnabled()
        {
            RegisterEvents();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
        }
        public void RegisterEvents()
        {
            events = new EventHandlers();
            Server.RoundStarted += events.OnRoundStarted;
            Server.RoundEnded += events.OnRoundEnded;
        }

        public void UnregisterEvents()
        {
            Server.RoundStarted -= events.OnRoundStarted;
            Server.RoundEnded -= events.OnRoundEnded;
            events = null;
        }
    }
}
