using Crypto.Monitoring.Services;
using Hangfire;
using Hangfire.Jobs.Enums;
using Hangfire.Jobs.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace hangfire.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateRecurringJob(JobsEnum typeJob, string jobId)
        {
            var job = JobsHelper.GetJob(typeJob);
            var cron = JobsHelper.GetCron(typeJob);

            //RecurringJob.AddOrUpdate(
            //    jobId,
            //    job,
            //    cron
            //);

            RecurringJob.AddOrUpdate<CryptoService>(jobId, job => job.ConsultingBalance(), cron);

            return Ok($"Job {jobId} do tipo {typeJob} criado com sucesso.");
        }
    }
}