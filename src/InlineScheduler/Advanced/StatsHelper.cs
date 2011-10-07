﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InlineScheduler.Advanced
{
    public static class StatsHelper
    {
        public static SchedulerStats GatherOveralStatistics(IEnumerable<WorkDef> _work)
        {
            var stats = new SchedulerStats();

            stats.CurrentJobs = _work
                .Select(x => new SchedulerJobStats
               {
                    WorkKey = x.WorkKey,
                    CurrentStatus = (SchedulerJobStatus)x.Status,
                    LastRunStarted = x.LastStart,
                    LastRunCompleted = x.LastComplete,
                    PreviousRuns = x.PreviousRuns.Select(xx => new SchedulerJobRunStats 
                    {
                        Started = xx.Started,
                        Completed = xx.Completed
                    }).ToList()
                }).ToList();

            stats.PendingJobs = stats.CurrentJobs.Count(x => x.CurrentStatus == SchedulerJobStatus.Pending);
            stats.RunningJobs = stats.CurrentJobs.Count(x => x.CurrentStatus == SchedulerJobStatus.Running);

            return stats;
        }
    }
}
