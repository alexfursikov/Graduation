namespace GraduationProject.Models
{
    public class DataModel
    {
        public int Id { get; set; }

        public double? HorizontalDistance { get; set; }

        public double? Azimuth { get; set; }

        public double? Bias { get; set; }

        public double? SlopeDistance { get; set; }

        public int DiameterOne { get; set; }

        public int DiameterTwo { get; set; }

        public string Species { get; set; }

        public double? F { get; set; }

        public double? NotAvailableDinstance { get; set; }

        public double? Hight { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                Id, HorizontalDistance, Azimuth, DiameterOne, DiameterTwo,
                Species, SlopeDistance, F, NotAvailableDinstance, Hight);
        }
    }
}