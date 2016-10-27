using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjektKatastrophenschutz.Utils
{
    public static class DummyDataHelper
    {
        private static readonly string listOfWordsUrl = "https://dl.dropboxusercontent.com/u/1320308/List_of_german_words.txt";

        public static Task<List<string>> GetRandomWords(int count)
        {
            var task = new Task<List<string>>(() =>
            {
                var listOfWords = new List<string>();
                var listOfRandomWords = new List<string>();

                var client = new WebClient();
                var stream = client.OpenRead(listOfWordsUrl);

                if (stream != null)
                {
                    var reader = new StreamReader(stream);
                    while (! reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        listOfWords.Add(line);
                    }
                }

                var random = new Random();

                for (var i = 0; i < count; i++)
                {                
                    var randomNumber = random.Next(0, listOfWords.Count);
                    listOfRandomWords.Add(listOfWords[randomNumber]);
                }

                return listOfRandomWords;
            });

            task.Start();
            return task;
        }

        public static Task<string> GetRandomWordsFromList(List<string> words, int min, int max, Random random)
        {
            var wordCount = new Random().Next(min, max);

            var task = new Task<string>(() =>
            {
                var result = string.Empty;
                //var random = new Random();

                for (var i = 0; i < wordCount; i++)
                {                 
                    var randomNumber = random.Next(0, words.Count);
                    result += (words[randomNumber] + " ");
                }

                return result;
            });

            task.Start();
            return task;
        }

        public static Task<int> GetRandomNumber(int min, int max, Random random)
        {
            var task = new Task<int>(() =>
            {
                var wordCount = random.Next(min, max);
                return wordCount;
            });

            task.Start();
            return task;
        }

        public static T GetRandomEnumValue<T>(Random random)
        {
            var values = Enum.GetValues(typeof(T));

            var randomValue = (T)values.GetValue(random.Next(values.Length));
            return randomValue;
        }
    }
}
