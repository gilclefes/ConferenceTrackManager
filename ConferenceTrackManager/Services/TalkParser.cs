using System;
using System.Collections.Generic;
using System.Linq;
using ConferenceTrackManager.Models;

namespace ConferenceTrackManager.Services
{
    //this class is to return a list of  talks

    public static class TalkParser
    {
        public static List<Talk> ParseTalks(string[] sourceFile)
        {
            try
            {
                var talkList = new List<Talk>();

                //check to confirm if the source file has content
                //else throw an exception
                if (sourceFile != null)
                {
                    foreach (var talk in sourceFile)
                    {
                        //split te file  using the last space character
                        //the firt part is the talk name and the second part is the time
                        //for the second part check if is lightning which is 5 minutes
                        //else check the first digits of a string and take that as the time
                        //string output = new string(input.TakeWhile(char.IsDigit).ToArray());

                        var talkTopic = talk.Substring(0, talk.LastIndexOf(' '));
                        var talkTime = talk.Substring(talk.LastIndexOf(' ') + 1);
                        int talkMinutes;
                        if (talkTime.Equals("lightning", StringComparison.CurrentCultureIgnoreCase))
                        {
                            talkMinutes = 5;
                        }
                        else
                        {
                            //get the first digits of the time part as the  and confirm if
                            //it is actually a number, if not a number throw exception
                            var output = new string(talkTime.Trim().TakeWhile(char.IsDigit).ToArray());

                            if (!Int32.TryParse(output, out talkMinutes))
                            {
                                Console.WriteLine("The Input Data is not in the correct format");
                                throw new Exception("Please check file Format");
                                // return null;
                            }
                        }

                        var newTalk = new Talk
                        {
                            Topic = talkTopic,
                            NumberOfMinutes = talkMinutes,
                            Duration = talkTime
                        };

                        talkList.Add(newTalk);
                    }

                    return talkList;
                }
                else
                {
                    throw new Exception("Please File Content is Empty");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("Please check file Format");
            }
        }
    }
}
