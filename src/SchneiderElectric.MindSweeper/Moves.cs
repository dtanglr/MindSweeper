using SchneiderElectric.MindSweeper.Moves;

namespace SchneiderElectric.MindSweeper;

partial class Field
{
    public sealed class Moves
    {
        private readonly List<Move> _moves;

        public Moves()
        {
            _moves = [];
        }

        public Moves(IEnumerable<Move> moves)
        {
            _moves = new List<Move>(moves);
        }

        public void Add(Move move) => _moves.Add(move);

        public List<Move> ToList() => _moves;

        public int Length => _moves.Count;
    }
}
