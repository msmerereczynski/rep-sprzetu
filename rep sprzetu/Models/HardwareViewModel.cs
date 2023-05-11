namespace rep_sprzetu.Models
{
    public class HardwareViewModel
    {
        public int Id {  get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List <string> Tag { get; set; }
        public string Owner { get; set; }
        public DateTime DateOfAssigment { get; set; } = DateTime.Now;
        public DateTime DateOfPurchase { get; set; } = DateTime.Now;
        public string? PreviousOwner { get; set; }
        public string? Filia { get; set; }
        public string? SerwisTag{ get; set; }
        public string? IdOfHardware { get; set; }
        public int DateOfProduction { get; set; }
    }
}
