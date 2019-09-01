using System.Collections.Generic;

namespace ConferenceTrackManager.Models
{
    public class ConfTrack
    {
        //object for a particular conference track
        //Name and a list of session talks
        public string TrackName { get; set; }
        public IList<SessionTalk> TalkList { get; set; }
    }
}
