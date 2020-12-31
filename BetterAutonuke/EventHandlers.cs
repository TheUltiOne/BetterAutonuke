using MEC;

using System.Collections.Generic;

using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace BetterAutonuke
{
    class EventHandlers
    {
        public IEnumerator<float> Autonuke()
        {
            for (var i = 0; i < BetterAutonuke.Instance.Config.Intervals; i++)
            {
                Map.Broadcast(5, $"<color=red><b>The Alpha Warhead</b></color> will detonate in <color=red><b>{BetterAutonuke.Instance.Config.Time / BetterAutonuke.Instance.Config.Intervals * i / 60}</b></color> minutes.");
                yield return Timing.WaitForSeconds(BetterAutonuke.Instance.Config.Time / BetterAutonuke.Instance.Config.Intervals);
            }

            Warhead.Start();
            Warhead.IsLocked = true;

            Map.Broadcast(5, "<color=red><b>The Alpha Warhead</b></color> was <color=red><b>automatically enabled. It can't be turned off.</b></color>");
        }

        public void OnRoundStarted()
        {
            if (!Warhead.IsDetonated)
            {
                Timing.RunCoroutine(Autonuke());
            }
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(BetterAutonuke.Coroutines.ToArray());
        }
    }
}
