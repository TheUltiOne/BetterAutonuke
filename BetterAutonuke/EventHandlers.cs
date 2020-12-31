using MEC;

using System;
using System.Collections.Generic;

using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace BetterAutonuke
{
    class EventHandlers
    {
        public string FilterString(int i)
        {
            if (BetterAutonuke.Instance.Config.Announcement.Contains("{minutes}"))
            {
                var message = BetterAutonuke.Instance.Config.Announcement.Replace("{minutes}", ((int)(BetterAutonuke.Instance.Config.Time / BetterAutonuke.Instance.Config.Intervals * i / 60)).ToString());
                return message;
            }
            else
            {
                return BetterAutonuke.Instance.Config.Announcement;
            }
        }

        public IEnumerator<float> Autonuke()
        {
            for (var i = 0; i < BetterAutonuke.Instance.Config.Intervals; i++)
            {
                Cassie.Message(FilterString(i));

                Map.Broadcast(5, $"<color=red><b>The Alpha Warhead</b></color> will detonate in <color=red><b>{(int)BetterAutonuke.Instance.Config.Time / BetterAutonuke.Instance.Config.Intervals * i / 60}</b></color> minutes.");
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
