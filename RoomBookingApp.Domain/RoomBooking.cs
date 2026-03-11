using RoomBookingApp.Domain.BaseModels;

namespace RoomBookingApp.Domain
{
    public class RoomBooking: RoomBookingBase
    {
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public int Id { get; set; }
    }
}