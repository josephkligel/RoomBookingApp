using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingRequestProcessor(DataServices.IRoomBookingService roomBookingService)
        {
            this._roomBookingService = roomBookingService;
        }

        public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
        {
            ArgumentNullException.ThrowIfNull(bookingRequest);

            var availableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date);

            if (availableRooms.Any())
            {
                _roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingRequest));
            }

            return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
        }

        private TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest)
            where TRoomBooking : RoomBookingBase, new()
        {
            return new TRoomBooking
            {
                FullName = bookingRequest.FullName,
                Date = bookingRequest.Date,
                Email = bookingRequest.Email,
            };
        }
    }
}