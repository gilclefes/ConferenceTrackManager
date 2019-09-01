namespace ConferenceTrackManager.Models
{
    public class Talk
    {
        //a talk object will have the topic and the numer of minutes
        //duration is to keep track of the time as read from the input
        //file
        public string Topic { get; set; }
        public int NumberOfMinutes { get; set; }
        public string Duration { get; set; }
    }
}
