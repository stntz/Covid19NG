using System;
namespace Covid19NG.Models
{
    public class CovidData
    {
        public string State { get; set; }
        public string ConfirmedCases { get; set; }
        public string AdmittedCases { get; set; }
        public string DischargedNumber { get; set; }
        public string DeathNumber { get; set; }
    }
}
