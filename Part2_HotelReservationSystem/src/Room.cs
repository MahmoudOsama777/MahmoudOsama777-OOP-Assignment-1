using static Part2_HotelReservationSystem.Reservation;

namespace Part2_HotelReservationSystem;

public class Room
{

    public enum Type { Single = 1, Double = 2, Suite = 3 }

    public int RoomNumber { get; }
    public Type RoomType { get; }
    public decimal NightlyRate { get; private set; }
    public bool IsUnderMaintenance { get; private set; }
    public bool IsAvailable { get; private set; } = true;
    public DateTime? CurrentCheckOut { get; private set; } = null;
    public DateTime? CurrentCheckIn { get; private set; } = null;

    public Room(int roomNumber, Type roomType, decimal nightlyRate)
    {
        if (nightlyRate <= 0)
            throw new ArgumentException("Nightly rate must be positive or greater than zero");

        RoomNumber = roomNumber;
        RoomType = roomType;
        NightlyRate = nightlyRate;
    }

    public void Booking(DateTime checkIn, DateTime checkOut)
    {
        if (IsUnderMaintenance)
            throw new ArgumentException("Cannot booking room under maintenance");

        if (CurrentCheckOut.HasValue && CurrentCheckIn.HasValue)
            if (!(checkIn > CurrentCheckOut.Value))
                throw new ArgumentException("Cannot booking booked room");


        IsAvailable = false;

        CurrentCheckOut = checkOut;
        CurrentCheckIn = checkIn;
        
    }

    public void ChangeAvailability(ReservationStatus status)
    {
        switch (status)
        {
            case ReservationStatus.Pending:
            case ReservationStatus.Confirmed:
            case ReservationStatus.CheckedIn:
                IsAvailable = false;
                break;
            case ReservationStatus.CheckedOut:
            case ReservationStatus.Cancelled:
                IsAvailable = true;
                break;
        }
    }

    public void StartMaintenance()
    {
        IsUnderMaintenance = true;
        IsAvailable = !IsUnderMaintenance;
    }

    public void EndMaintenance()
    {
        IsUnderMaintenance = false;
        IsAvailable = !IsUnderMaintenance;
    }

    public void ChangeNightlyRate(decimal nightlyRate)
    {
        if (nightlyRate < 0)
            throw new ArgumentException("Not a positive number");

        NightlyRate = nightlyRate;
    }
}
