namespace Part2_HotelReservationSystem;

public class Reservation
{

    public enum ReservationStatus { Pending = 1, Confirmed = 2, CheckedIn = 3, CheckedOut = 4, Cancelled = 5 }

    public int ReservationId { get; }

    public DateTime CheckInDate { get; }

    public DateTime CheckOutDate { get;}

    public int NumberOfNights => (CheckOutDate - CheckInDate).Days;

    public Room Room { get; init; }

    public decimal TotalCost => NumberOfNights * Room.NightlyRate;

    public ReservationStatus Status { get; private set; }

    public Reservation(int reservationId, DateTime checkInDate, DateTime checkOutDate, Room room)
    {
        if (checkInDate > checkOutDate)
            throw new ArgumentException("Checkin date cannot be after Checkout date");

        if (room is null)
            throw new NullReferenceException();

        if(room.IsUnderMaintenance)
            throw new ArgumentException("Reservation cannot be created for a room that is currently under maintenance");

        ReservationId = reservationId;  
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Room = room;
        Status = ReservationStatus.Pending;
    }


    public void CheckingIn()
    {
        if(Status != ReservationStatus.Confirmed)
            throw new ArgumentException("Cannot check-in without confirmation");

        Status = ReservationStatus.CheckedIn;
        Room.ChangeAvailability(Status);
    }

    public void CheckingOut()
    {
        if (Status != ReservationStatus.CheckedIn)
            throw new ArgumentException("Cannot check-out before check-in");

        Status = ReservationStatus.CheckedOut;
        Room.ChangeAvailability(Status);
    }

    public void Cancelling()
    {
        if (Status != ReservationStatus.Pending || Status != ReservationStatus.Confirmed)
        {
            if (Status == ReservationStatus.CheckedOut)
                throw new ArgumentException("Cannot cancelling an already-checked-out reservation");
            else
                throw new ArgumentException("Cannot cancelling the reservation");
        }

        Status = ReservationStatus.Cancelled;
        Room.ChangeAvailability(Status);
    }

    public void Confirming()
    {
        if (Status != ReservationStatus.Pending)
            throw new ArgumentException("Cannot confirm the reservation before pending");

        Status = ReservationStatus.Confirmed;
        Room.ChangeAvailability(Status);
    }

}
