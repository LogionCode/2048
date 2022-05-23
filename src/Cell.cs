namespace _2048
{
    class Cell
    {
        int value;

        public Cell() { value = 0; }

        public bool moveTo(Cell target)
        {
            if (!canMove(target))
                return false;

            target.addValue(value);
            value = 0;
            return true;
        }

        public bool canMove(Cell target) // checks if this tile can move to target
        {
            if (value == 0) // tiles that are empty can't move
                return false;

            if (!target.isEmpty() && target.getValue() != value) // can't sum up to tiles with different value
                return false;

            return true;
        }

        public void addValue(int _value) { value += _value; }

        public string toString() // toDo: same as show() and replace with debug informations (value, index)
        {
            if (value == 0)
                return "_";
            return "" + value;
        }

        public bool isEmpty() { return value == 0; }

        public int getValue() { return value; }
    }
}
