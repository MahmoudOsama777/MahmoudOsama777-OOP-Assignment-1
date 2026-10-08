namespace Part2_HotelReservationSystem;

public class Guest
{

    public int Id { get; init; }
    public string FullName { get; init; }
    public string PhoneNumber { get; init; }


    private List<Reservation> _reservations = new List<Reservation>();

    public IReadOnlyList<Reservation> Reservations => _reservations;
        

    public Guest(int id, string fullName, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new NullReferenceException();

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new NullReferenceException();

        Id = id;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public void PrintGuestDetails()
    {
        Console.WriteLine($"Guest ID: {Id}");
        Console.WriteLine($"Fullname: {FullName}");
        Console.WriteLine($"Phone   : {PhoneNumber}");
    }

    public void MakeReservation(int reservationId, DateTime checkInDate, DateTime checkOutDate, Room room)
    {
        room.Booking(checkInDate, checkOutDate);
        _reservations.Add(new Reservation(reservationId, checkInDate, checkOutDate, room));
    }

    public void PrintAllReservations()
    {
        Console.WriteLine("\n==============================================");
        Console.WriteLine($"                 {FullName}");
        Console.WriteLine("==============================================\n");

        foreach (var reservation in _reservations)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine($"Reservation ID : {reservation.ReservationId}");
            Console.WriteLine($"Check-In       : {reservation.CheckInDate:yyyy-MM-dd}");
            Console.WriteLine($"Check-Out      : {reservation.CheckOutDate:yyyy-MM-dd}");
            Console.WriteLine($"Nights         : {reservation.NumberOfNights}");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Room Number    : {reservation.Room.RoomNumber}");
            Console.WriteLine($"Room Type      : {reservation.Room.RoomType}");
            Console.WriteLine($"Nightly Rate   : {reservation.Room.NightlyRate:N2}");
            Console.WriteLine($"Total Cost     : {reservation.TotalCost:N2}");
            Console.WriteLine("==============================================\n");
        }
    }


}
