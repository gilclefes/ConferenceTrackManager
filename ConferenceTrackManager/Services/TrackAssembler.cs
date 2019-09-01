using System;
using System.Collections.Generic;
using System.Linq;
using ConferenceTrackManager.Models;

namespace ConferenceTrackManager.Services
{
    public static class TrackAssembler
    {

        //this is to get the possible conference tracks
        //from a list of talks
        public static List<ConfTrack> GetConfTracks(List<Talk> talkList)
        {
            try
            {
                //every track has 2 parts Morning and Afternoon session
                //and morning session has 180 minutes whiles after has 240minutes
                var totalMinutes = 180;
                var sessionStage = (int)SessionType.Morning;
                var completed = true;
                var trackCount = 0;
                var confTrackList = new List<ConfTrack>();
                var confTrack = new ConfTrack();
                var sessionTime = new SessionTime(9, 0);

                //after a talk is adding to a session
                //it is removed from the list
                //so checking to see if the list has any talk left
                while (talkList.Any())
                {


                    //checking to see if we are now at lunch
                    if (totalMinutes == 0 && sessionStage == (int)SessionType.Morning)
                    {
                        totalMinutes = 240;
                        sessionStage = (int)SessionType.Afternoon;
                        sessionTime = new SessionTime(1, 0);
                        confTrack.TalkList.Add(new SessionTalk
                        {
                            //Hour = 12,
                            //Minute = 0,
                            Time = "12:00PM",
                            Talk = new Talk
                            {
                                Topic = "Lunch",
                                Duration = ""
                            }
                        });
                    }

                    //checking to see if we are at the end of a session
                    else if (totalMinutes == 0 && sessionStage == (int)SessionType.Afternoon)
                    {

                        completed = true;
                        confTrack.TalkList.Add(new SessionTalk
                        {
                            Time = "05:00PM",
                            Talk = new Talk
                            {
                                Topic = "Networking Event",
                                Duration = ""
                            }
                        });

                        confTrackList.Add(confTrack);
                    }

                    if (completed)
                    {
                        sessionTime = new SessionTime(9, 0);
                        totalMinutes = 180;
                        sessionStage = 1;
                        trackCount = trackCount + 1;
                        confTrack = new ConfTrack
                        {
                            TrackName = "Track " + trackCount.ToString(),
                            TalkList = new List<SessionTalk>()
                        };
                        completed = false;
                    }


                    //randomly  seleciting a talk that can fit the time left for the session
                    //the cantalkbeadded method used the Subset Sum Problem to find out
                    //if the talks left can fit the minutes left  in a session
                    var selectedTalk = RandomlySelectTalk(talkList, totalMinutes);
                    while (!CanTalkBeAdded(selectedTalk, talkList, totalMinutes))
                    {
                        selectedTalk = RandomlySelectTalk(talkList, totalMinutes);
                    }

                    
                    totalMinutes = totalMinutes - selectedTalk.NumberOfMinutes;

                    confTrack.TalkList.Add(new SessionTalk
                    {
                        Time = TimeFormated(sessionTime, sessionStage == (int)SessionType.Morning ? "AM" : "PM"),
                        Talk = new Talk
                        {
                            Topic = selectedTalk.Topic,
                            Duration = selectedTalk.Duration
                        }
                    });

                    sessionTime = AddMinutes(sessionTime, selectedTalk.NumberOfMinutes);
                    talkList.Remove(selectedTalk);

                    //if this is the last talk removed from the list
                    //and we are in the afternoon session
                    //add the network event
                    //
                    if (!talkList.Any() && sessionStage == (int)SessionType.Afternoon && totalMinutes>=0)
                    {
                        confTrack.TalkList.Add(new SessionTalk
                        {
                            Time = "05:00PM",
                            Talk = new Talk
                            {
                                Topic = "Networking Event",
                                Duration = ""
                            }
                        });
                        confTrackList.Add(confTrack);
                    }

                }

                return confTrackList;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }



        private static Talk RandomlySelectTalk(List<Talk> talkList, int totalMinuteLeft)
        {

            //randomly select a talk from the list of talks
            var selectedTalk =
               talkList.Where(x => x.NumberOfMinutes <= totalMinuteLeft).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            return selectedTalk;

        }
        private static bool CanTalkBeAdded(Talk talk, List<Talk> talkList, int totalMinuteLeft)
        {
            // this is to check to see if the selected talk can be added to the session
            // check to make sure that after selecting the talk the some or all of the rest of talks left
            // can fill up equally the time of minues left in the session
            try
            {
                //if it is the only talk in the list return it
                if (talkList.Count == 1)
                {
                    return true;
                }

                //check to  see if after selecting this talk the minues left will be zero
                //select it
                var minutesLeft = totalMinuteLeft - talk.NumberOfMinutes;
                if (minutesLeft < 0)
                {
                    return false;
                }
                else if (minutesLeft == 0)
                {
                    return true;
                }

                //if the sum of the minutes in the talk left is less than
                // the rest of minues left in the session 
                var totalSum = talkList.Select(x => x.NumberOfMinutes).Sum();
                if (totalSum < totalMinuteLeft)
                {
                    return true;
                }


                //select all talks left with their minutes less than the minutes left in the session
                //after that check to make sure if a sum of the subset can be equal to the minutes left
                var minutesArray = talkList.Where(x => x.NumberOfMinutes <= minutesLeft && !x.Topic.Equals(talk.Topic))
                    .Select(x => x.NumberOfMinutes).ToArray();

                return IsSubsetSum(minutesArray, minutesArray.Length, minutesLeft);


            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }

        private static bool IsSubsetSum(int[] set, int n, int sum)
        {
            // Base Cases
            if (sum == 0)
                return true;
            if (n == 0 && sum != 0)
                return false;

            // If last element is greater than sum, then ignore it
            if (set[n - 1] > sum)
                return IsSubsetSum(set, n - 1, sum);

            /* else, check if sum can be obtained by any of the following
               (a) including the last element
               (b) excluding the last element   */
            return IsSubsetSum(set, n - 1, sum) ||
                                        IsSubsetSum(set, n - 1, sum - set[n - 1]);
        }

        private static SessionTime AddMinutes(SessionTime currentTime, int minutes)
        {
            var newTime = new TimeSpan(currentTime.Hour, currentTime.Minute, 0).Add(new TimeSpan(0, minutes, 0));
            return new SessionTime(newTime.Hours, newTime.Minutes);
        }

        private static string TimeFormated(SessionTime currentTime, string aMorPm)
        {
            return currentTime.Hour.ToString().PadLeft(2, '0') + ":"
                + currentTime.Minute.ToString().PadLeft(2, '0') + aMorPm;
        }
    }
}
