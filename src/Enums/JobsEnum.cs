using System.ComponentModel;

namespace Hangfire.Jobs.Enums
{
    public enum JobsEnum
    {
        [Description("Job executado a cada hora")]
        Hour,

        [Description("Job executado a cada minuto")]
        Minutely,

        [Description("Job diário")]
        Daily,
    }
}