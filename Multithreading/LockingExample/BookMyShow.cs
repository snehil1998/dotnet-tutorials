
public sealed class BookMyShow
{
    public int availableTickets = 3;

    public void Book(object? wantedTickets)
    {
        int tickets = Convert.ToInt32(wantedTickets);
        if (tickets <= availableTickets)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} has tickets booked. Tickets left: {availableTickets}");
            availableTickets -= tickets;
        }
        else
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: tickets unavailable. Tickets left: {availableTickets}");
        }
    }

    public void BookLocked(object? wantedTickets)
    {
        lock(_lockObject)
        {
            int tickets = Convert.ToInt32(wantedTickets);
            if (tickets <= availableTickets)
            {
                availableTickets -= tickets;
                Console.WriteLine($"{Thread.CurrentThread.Name} has tickets booked. Tickets left: {availableTickets}");
            }
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: tickets unavailable. Tickets left: {availableTickets}");
            }
        }
    }

    private object _lockObject = new();
}