using System;
using System.Collections.Generic;
using System.IO;

namespace _2048
{
    class Scoreboard
    {
        static readonly string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // specify where records are kept
        const string savedGames = "/SavedGames";
        const string gameFolder = "/2048";
        const string fileName = "/scoreboard.json";
        readonly List<Record> scores;

        public Scoreboard() // read from file on creation
        {
            scores = fromJson(filePath + savedGames + gameFolder + fileName);
        }

        ~Scoreboard()
        {
            saveScoreboard();
        }

        public void addScore(Record record) // adds another record to scoreboard
        {
            if (record == null)
                return;

            scores.Add(record);
        }

        void saveScoreboard()
        {

            if (!Directory.Exists(filePath + savedGames))
                Directory.CreateDirectory(filePath + savedGames);


            if (!Directory.Exists(filePath + savedGames + gameFolder))
                Directory.CreateDirectory(filePath + savedGames + gameFolder);

            File.WriteAllText(filePath + savedGames + gameFolder + fileName, toJson(scores));
        }

        public void emptyScoreboard()
        {
            scores.Clear();
        }

        public Record[] readSorted() // read score list in order from highest to lowest
        {
            Record[] records = scores.ToArray();
            bool doneSorting = false;

            while (!doneSorting) // sort records
            {
                doneSorting = true;
                for (int i = 1; i < records.Length; i++)
                {
                    if (records[i].score > records[i - 1].score)
                    {
                        (records[i - 1], records[i]) = (records[i], records[i - 1]);
                        doneSorting = false;
                    }
                }

            }
            return records;
        }

        public string toJson(List<Record> records) // converts score list into json formatted string
        {
            string output = "[\n";

            for (int i = 0; i < records.Count; i++)
            {
                output += "\t{\n";
                output += "\t\t" + '"' + "name" + '"' + ": " + '"' + records[i].name + '"' + ",\n";
                output += "\t\t" + '"' + "score" + '"' + ": " + records[i].score + ",\n";
                output += "\t\t" + '"' + "moves" + '"' + ": " + records[i].moves + '\n';
                output += "\t}";

                if (i < records.Count - 1) // if it's not last record add comma
                    output += ',';
                output += '\n';
            }
            output += ']';

            return output;
        }

        public List<Record> fromJson(string file) // reads json file and converts it into record list
        {
            List<Record> records = new List<Record>();

            if (!File.Exists(file))
                return records;

            string[] json = File.ReadAllLines(file);

            if (json.Length < 2)
                return records; // if less than 2 lines scoreboard.js is empty;

            for (int i = 1; i < json.Length - 1; i += 5) // every record covers 5 lines
            {
                string name = json[i + 1].Substring(11, json[i + 1].Length - 13);
                int score = int.Parse(json[i + 2].Substring(11, json[i + 2].Length - 12));
                int moves = int.Parse(json[i + 3].Substring(11, json[i + 3].Length - 11));

                Record temp = new Record(name, score, moves);
                records.Add(temp);
            }
            return records;
        }
    }
}
