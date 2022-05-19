namespace _2048
{
    class Cell
    {
        int value;
        bool didMove = false;

        public Cell() { value = 0; }
        public bool moveTo(Cell target)
        {
            if (value == 0 || didMove || target.hasMoved()) // tiles that are empty or have already moved can't move now
                return false;

            if (!target.isEmpty() && target.getValue() != value) // can't sum up to tiles with different value
                return false;


            if (!target.isEmpty()) // if target is not empty tile it can't move again later
            {
                didMove = true;
                target.didMove = true;
            }

            target.addValue(value);
            value = 0;
            return true;
        }
        public string toString()
        {
            if (value == 0)
                return "_";
            return "" + value;
        }
        public bool isEmpty() { return value == 0; }
        public bool hasMoved() { return didMove; }
        public int getValue() { return value; }
        public void addValue(int _value) { value += _value; }
        public void freeMove() { didMove = false; }
    }
}
