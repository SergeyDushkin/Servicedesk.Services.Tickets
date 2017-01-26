namespace servicedesk.Services.Tickets.Domain
{
    public static class States
    {
        public static string Created => "created";
        public static string Completed => "completed";
        public static string Rejected => "rejected";
    }
}