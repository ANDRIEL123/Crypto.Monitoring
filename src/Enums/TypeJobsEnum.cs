using System.ComponentModel;

namespace Crypto.Monitoring.Enums
{
    public enum TypeJobsEnum
    {
        [Description("Job executado a cada hora")]
        Hour,

        [Description("Job executado a cada minuto")]
        Minutely,

        [Description("Job diário")]
        Daily,
    }
}