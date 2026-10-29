namespace DeutscheBank.src.Time
{
    public sealed class SystemClock : IClock
    {
        public DateTime Now() => DateTime.UtcNow;
    }
}
