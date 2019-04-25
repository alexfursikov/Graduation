namespace GraduationProject.Models
{
    public class DataModel
    {
        public int Id { get; set; }

        //hv
        public double? HorizontalDistance { get; set; }

        public double? Azimuth { get; set; }

        //Vertical
        public double? Bias { get; set; }

        public double? SlopeDistance { get; set; }

        public int DiameterOne { get; set; }

        public int DiameterTwo { get; set; }

        public string Species { get; set; }

        public double? F { get; set; }

        //ML
        public double? NotAvailableDinstance { get; set; }

        public double? Hight { get; set; }

        public override string ToString()
        {
            return Id + "," + HorizontalDistance + "," + Azimuth + "," + DiameterOne + "," +
                DiameterTwo + "," + Species + "," + SlopeDistance + "," + F + "," + NotAvailableDinstance + "," + Hight;
        }

    }
}