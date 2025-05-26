namespace EventSphere.ViewModels
{
    public class SeatDto
    {
        public int SeatId { get; set; }
        public int EventId { get; set; }
        public string Section { get; set; } = string.Empty;
        public string RowNumber { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public bool IsReserved { get; set; }
        public string? EventName { get; set; } // sadece event'in adını gösterelim
    }
}
