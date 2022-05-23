namespace _2048
{
    class Record
    {
        public string name;
        public int score;
        public int moves;

        public Record(string _name, int _score, int _moves)
        {
            score = _score;
            name = _name;
            moves = _moves;
        }

        public string toString()
        {
            return name + ": " + score + " points - " + moves + " moves";
        }
    }

}
