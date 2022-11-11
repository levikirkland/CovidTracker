using System.ComponentModel.DataAnnotations;

namespace CovidTracker.Models
{
    public class CovidStateModel
    {
        public DateTime dateModified { get; set; }
        public string? state { get; set; }
        public int positive { get; set; }
        public int negative { get; set; } 
        public int total { get; set; }
        public int hospitalizedCurrently { get; set; }
    }
}
