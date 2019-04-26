using System;
namespace Companion
{
    public static class CompanionServer
    {
        public static string base_url = "https://answeralsdev.evergreencircuits.com/api/v1/";
        public static string recording_url = base_url + "recording/" + App.UserID + "/" + App.CurrentSentenceHash;
        public static string sentence_url = base_url + "sentence/" + App.UserID;
        public static string image_url = base_url + "picture/" + App.UserID;
    }
}
