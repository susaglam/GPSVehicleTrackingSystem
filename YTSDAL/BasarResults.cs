namespace YTSDAL
{
    public class BasarResults
    {
        //    b.Status = poi.Count == 0 ? br.Street : poi[0].Name.ToString(); // tam adres
        //    b.Quarter = br.Order9 == null ? "" : br.Order9; //ay < 10 ? "0" +ay.ToString() : ay.ToString();//mahalle
        //    b.Street = br.Street == null ? "" : br.Street; //gelendata;//sokak
        //    b.District = br.Order8 == null ? "" : br.Order8;//ilçe
        //    b.City = br.Order1 == null ? "" : br.Order1;//il
        //    b.Country = br.Order0 == null ? "" : br.Order0;//ülke
        public string Order0
        {
            get;
            set;
        }

        public string Order1
        {
            get;
            set;
        }

        public string Order8
        {
            get;
            set;
        }

        public string Order9
        {
            get;
            set;
        }

        public string Street
        {
            get;
            set;
        }

        public BasarResults()
        {
        }
    }
}