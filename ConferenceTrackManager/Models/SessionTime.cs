namespace ConferenceTrackManager.Models
{
    public class SessionTime
    {
        //to track the time when creating a session
        public int Hour { get; set; }
        public int Minute { get; set; }

        public SessionTime(int hour, int minute)
        {
            this.Hour = hour;
            this.Minute = minute;
        }
    }
}
