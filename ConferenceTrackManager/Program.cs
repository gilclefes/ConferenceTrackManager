using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConferenceTrackManager.Services;

namespace ConferenceTrackManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //get list of talks from the input file
                //var talkList = TalkParser.ParseTalks(FileReader.ReadFile(Directory.GetCurrentDirectory() + "\\testdata.txt"));

                ////get the list of session tracks from the talk list
                //var confList = TrackAssembler.GetConfTracks(talkList);

                //foreach (var track in confList)
                //{
                //    Console.WriteLine();
                //    Console.WriteLine("{0}", track.TrackName);
                //    foreach (var talk in track.TalkList)
                //    {
                //        Console.WriteLine("{0} {1} {2}", talk.Time, talk.Talk.Topic, talk.Talk.Duration); ;
                //    }

                //}
                //Console.ReadLine();
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                Console.ReadLine();
            }

        }


        private int solution(int A, int B)
        {
            int indexOf = 0;

            var strA = A.ToString();
            var strB = B.ToString();
            var answer = new StringBuilder();

            var foundIndexes = new List<int>();

            long t1 = DateTime.Now.Ticks;
            for (int i = strB.IndexOf(strA); i > -1; i = strB.IndexOf(strA, i + 1))
            {
                // for loop end when i=-1 ('a' not found)
                foundIndexes.Add(i);
            }
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            return indexOf;
        }

        private int solution1(int A, int B)
        {
            int indexOf = 0;

            var strA = A.ToString();
            var strB = B.ToString();
            if (strB.IndexOf(strA,StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                return strB.IndexOf(strA, StringComparison.CurrentCultureIgnoreCase);
            }
            else
            {
                return -1;
            }
        }

        private int[] getZeroSum(int N)
        {
            var integers = new List<int>();

            if (N%2 == 0)
            {
                int value = N/2;
                while (value>0)
                {

                    integers.Add(value);
                    integers.Add(value *-1);
                    value = value - 1;
                }
            }
            else
            {
                int value = N / 2;

                while (value > 0)
                {
                    N = N - 1;
                    integers.Add(N);
                    integers.Add(N * -1);
                    value = value - 1;
                }
                integers.Add(0);
            }

            return integers.ToArray();
        }
    }
}
