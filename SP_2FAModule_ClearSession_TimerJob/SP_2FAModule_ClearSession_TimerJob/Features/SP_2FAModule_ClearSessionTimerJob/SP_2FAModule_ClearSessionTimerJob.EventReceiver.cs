using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SP_2FAModule_ClearSessionTimerJob.Common;
using System;
using System.Runtime.InteropServices;

namespace SP_2FAModule_ClearSession_TimerJob.Features.SP_2FAModule_ClearSessionTimerJob
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("83082c18-aa67-46ac-bcb5-3603353b16fb")]
    public class SP_2FAModule_ClearSessionTimerJobEventReceiver : SPFeatureReceiver
    {
        const string JobName = "SP 2FAModule Clear Session Timer Job";

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    SPWebApplication parentWebApp = (SPWebApplication)properties.Feature.Parent;
                    SPSite site = properties.Feature.Parent as SPSite;
                    DeleteJob(JobName, parentWebApp);
                    CreateJob(parentWebApp);
                });
            }
            catch (Exception ex)
            {
                UlsLoggingService.LogError(UlsLoggingService.ClearSessionTimerJob_Error, ex.Message);
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            lock (this)
            {
                try
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        SPWebApplication parentWebApp = (SPWebApplication)properties.Feature.Parent;
                        DeleteJob(JobName, parentWebApp);
                    });

                }
                catch (Exception ex)
                {
                    UlsLoggingService.LogError(UlsLoggingService.ClearSessionTimerJob_Error, ex.Message);
                }
            }
        }

        public bool CreateJob(SPWebApplication site)
        {
            bool jobCreated = false;
            try
            {
                SP_2FAModule_ClearSession_TimerJob job = new SP_2FAModule_ClearSession_TimerJob(JobName, site);
                SPDailySchedule schedule = new SPDailySchedule();
                schedule.BeginHour = 0;
                schedule.EndHour = 1;
                job.Schedule = schedule;
                job.Update();
            }
            catch (Exception ex)
            {
                UlsLoggingService.LogError(UlsLoggingService.ClearSessionTimerJob_Error, ex.Message);
                return jobCreated;
            }
            return jobCreated;
        }

        public bool DeleteJob(string jobName, SPWebApplication site)
        {
            bool jobdeleted = false;
            try
            {
                foreach (SPJobDefinition job in site.JobDefinitions)
                {
                    if (job.Name == jobName)
                    {
                        job.Delete();
                        jobdeleted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                UlsLoggingService.LogError(UlsLoggingService.ClearSessionTimerJob_Error, ex.Message);
                return jobdeleted;
            }
            return jobdeleted;
        }
    }
}