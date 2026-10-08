namespace Part2_HotelReservationSystem;

public class Program
{
    static void Main(string[] args)
    {
        Guest guest1 = new Guest(1, "Mahmoud Osama", "0123183124");
        Guest guest2 = new Guest(2, "Ahmed Ali", "0111235324");
        Guest guest3 = new Guest(3, "Nada Ahmed", "0153123458132");

        Room room1 = new Room(101, Room.Type.Single, 100.50m);
        Room room2 = new Room(102, Room.Type.Double, 250.50m);
        Room room3 = new Room(103, Room.Type.Suite, 550.50m);

        guest1.MakeReservation(1, new DateTime(2026, 10, 2), new DateTime(2026, 10, 5), room1);
        guest1.MakeReservation(2, new DateTime(2026, 10, 6), new DateTime(2026, 10, 10), room1);

        guest2.MakeReservation(3, new DateTime(2026, 10, 11), new DateTime(2026, 10, 12), room1);
        guest2.MakeReservation(4, new DateTime(2026, 10, 6), new DateTime(2026, 10, 10), room2);

        guest1.PrintAllReservations();
        guest2.PrintAllReservations();
    }
}
