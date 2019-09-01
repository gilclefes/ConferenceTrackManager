using System;
using System.Collections.Generic;
using System.IO;
using ConferenceTrackManager.Models;
using ConferenceTrackManager.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferenceTrackManager.UnitTest
{
    [TestClass]
    public class ConfTrackUnitTest
    {


        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileReaderFileExist()
        {
            // arrange
            FileReader.ReadFile("asads");

            // act       
            // assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ParseTalks()
        {
            // arrange
            TalkParser.ParseTalks(new string[] { "wrong tests", "worng value" });

            // act       
            // assert is handled by ExpectedException
        }

        [TestMethod]    
        public void AssembleTracks()
        {
            // arrange
         var result =  TrackAssembler.GetConfTracks(new List<Talk>(){});
         Assert.IsNotNull(result);
        }
    }
}
