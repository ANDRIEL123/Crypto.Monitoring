using Crypto.Monitoring.Enums;
using Crypto.Monitoring.Jobs;
using Crypto.Monitoring.src.Enums;
using Crypto.Monitoring.src.Jobs.Factories;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.Monitoring.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateRecurringJob(TypeJobsEnum typeJob, JobsEnum job)
        {
            var jobId = Guid.NewGuid().ToString();
            var cron = JobsHelper.GetCron(typeJob);

            JobsFactory.CreateJob(jobId, cron, job);

            return Ok($"Job {jobId} do tipo {typeJob} criado com sucesso.");
        }

        [HttpDelete]
        public IActionResult DeleteRecurringJob(string jobId)
        {
            RecurringJob.RemoveIfExists(jobId);

            return Ok($"Job {jobId} foi deletado se ele existia.");
        }
    }
}