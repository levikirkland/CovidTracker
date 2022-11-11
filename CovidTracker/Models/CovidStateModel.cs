
namespace CovidTracker.Models
{
    public class CovidStateModel
    {
        public DateTime dateModified { get; set; }
        public string? state { get; set; }
        public int? positive { get; set; }
        public int? negative { get; set; }
        public int? total { get; set; }
        public int? hospitalizedCurrently { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            if (object.ReferenceEquals(this, obj)) { return true; }

            var model = obj as CovidStateModel;
            return this.dateModified == model.dateModified
                && this.state == model.state
                && this.positive == model.positive
                && this.negative == model.negative
                && this.total == model.total
                && this.hospitalizedCurrently == model.hospitalizedCurrently;
        }

        public override int GetHashCode()
        {
            return $"{this.dateModified}{this.state}{this.positive}{this.negative}{this.total}{this.hospitalizedCurrently}".GetHashCode();
        }
    }
}

