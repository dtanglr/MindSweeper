using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Components;

partial class Field
{
    public sealed class Moves
    {
        private readonly List<Direction> _moves;

        public Moves()
        {
            _moves = [];
        }

        public Moves(IEnumerable<Direction> moves)
        {
            _moves = new List<Direction>(moves);
        }

        public void Add(Direction move) => _moves.Add(move);

        public List<Direction> ToList() => _moves;

        public int Length => _moves.Count;
    }
}
