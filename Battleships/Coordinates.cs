using System;

namespace Battleships
{
    public class Coordinates : IEquatable<Coordinates>, IComparable<Coordinates>
    {
        public char Column { get; }
        public int Row { get; }

        public Coordinates(char column, int row)
        {
            Column = column;
            Row = row;
        }
        
        public override string ToString()
        {
            return Column + Row.ToString();
        }

        public bool Equals(Coordinates other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Column == other.Column && Row == other.Row;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Coordinates) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Column, Row);
        }

        public int CompareTo(Coordinates other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }
            if (ReferenceEquals(null, other))
            {
                return 1;
            }
            
            var columnComparison = Column.CompareTo(other.Column);
            
            return columnComparison != 0 ? columnComparison : Row.CompareTo(other.Row);
        }
    }
}