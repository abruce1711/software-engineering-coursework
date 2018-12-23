using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace NapierBank.Models
{
    /*
       Tweet subclass.
       This is a subclass which handles Tweet only operations.
    */
    class TweetModel : Message
    {
        // Constructor
        public TweetModel()
        {
            Header = "";
            Body = "";
            Sender = "";
        }

        // Constructor
        public TweetModel(string header, string body, string sender)
        {
            Header = header;
            Body = body;
            Sender = sender;
        }

        // Reads the hashtags list
        private static List<string> ReadHashtags()
        {
            List<string> hashtags = new List<string>();
            using (var reader = new StreamReader("lists\\hashtags.csv"))
            {
                while(!reader.EndOfStream)
                {
                    hashtags.Add(reader.ReadLine().ToLower());
                }
            }
            return hashtags;
        }

        // Reads the mentions list
        public static List<string> ReadMentions()
        {
            List<string> mentions = new List<string>();
            using (var reader = new StreamReader("lists\\mentions.csv"))
            {
                while (!reader.EndOfStream)
                {
                    mentions.Add(reader.ReadLine().ToLower());
                }
            }
            return mentions;
        }

        // Produce trending list
        public static List<string> ProduceTrendingList()
        {
            List<string> hashTags = ReadHashtags();
            hashTags.Remove("hashtags");
            var trendingList = hashTags
                .GroupBy(s => s)
                .OrderByDescending(g => g.Count())
                .SelectMany(g => g).ToList();
            return trendingList.Distinct().ToList();
        }

        // Parses mentions and hashtags from a tweet
        public void ParseTweet()
        {
            string[] tweetArray = this.Body.Split(' ');
            foreach(string word in tweetArray)
            {
                if(word.StartsWith("#"))
                {
                    string file = "lists\\hashtags.csv";
                    DirectoryInfo di = Directory.CreateDirectory("lists");
                    if(!File.Exists(file))
                    {
                        File.WriteAllText(file, "Hashtags\n");
                    }

                    File.AppendAllText(file, word + '\n');
                } else if (word.StartsWith("@"))
                {
                    string file = "lists\\mentions.csv";
                    DirectoryInfo di = Directory.CreateDirectory("lists");
                    if (!File.Exists(file))
                    {
                        File.WriteAllText(file, "Mentions\n");
                    }

                    File.AppendAllText(file, word + '\n');
                    
                }
            }

        }

        // Reads JSON from tweet file, deserializes and returns list
        public static List<TweetModel> ReadJson()
        {
            DirectoryInfo di = Directory.CreateDirectory("tweets");
            List<TweetModel> processedTweetList = new List<TweetModel>();
            if(File.Exists("tweets\\tweets.json"))
            {
                string file = File.ReadAllText("tweets\\tweets.json");
                string[] jsonList = file.Split('-');

                foreach (string item in jsonList)
                {
                    TweetModel tweet = JsonConvert.DeserializeObject<TweetModel>(item);
                    processedTweetList.Add(tweet);
                }
            }
            return processedTweetList;
        }
    }
}
