using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain.BaseModels;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Domain;

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

            var result = CreateRoomBookingObject<RoomBookingResult>(bookingRequest);

            var availableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date);

            if (availableRooms.Any())
            {
                var room = availableRooms.First();
                var roomBooking = CreateRoomBookingObject<RoomBooking>(bookingRequest);
                roomBooking.RoomId = room.Id;
                _roomBookingService.Save(roomBooking);

                result.RoomBookingId = roomBooking.Id;
                result.Flag = Enums.BookingResultFlag.Success;
            } 
            else
            {
                result.Flag = Enums.BookingResultFlag.Failure;
            }

            return result;
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