namespace ConferenceTrackManager.Models
{
    public class SessionTalk
    {
        //an object for a talk at a session
        //will have the talk it self and the time of the talk
        public string Time { get; set; }
        public Talk Talk { get; set; }
    }
}
