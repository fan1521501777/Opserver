﻿using Microsoft.Extensions.Options;

namespace Opserver.Data.PagerDuty
{
    public class PagerDutyModule : StatusModule<PagerDutySettings>
    {
        public override string Name => "PagerDuty";
        public override bool Enabled => Settings.Enabled;

        public PagerDutyAPI API { get; }

        public PagerDutyModule(IOptions<PagerDutySettings> settings, PollingService poller) : base(settings, poller)
        {
            if (settings.Value.Enabled)
            {
                API = new PagerDutyAPI(this);
                API.TryAddToGlobalPollers();
            }
        }

        public override MonitorStatus MonitorStatus => API.MonitorStatus;
        public override bool IsMember(string node) => false;
    }
}
