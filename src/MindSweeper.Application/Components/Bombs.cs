using MindSweeper.Application.Components.Squares;
using MindSweeper.Domain;

namespace MindSweeper.Application.Components;

partial class Field
{
    public sealed class Bombs
    {
        private readonly Lazy<HashSet<int>> _bombs;

        public Bombs(Settings settings) : this(settings.Bombs, settings.Squares) { }

        private Bombs(int bombCapacity, int squaresCapacity)
        {
            if (bombCapacity < Settings.MinimumBombs)
            {
                throw new ArgumentOutOfRangeException(nameof(bombCapacity), $"The number of bombs must be at least {Settings.MinimumBombs}.");
            }

            if (bombCapacity > squaresCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(bombCapacity), $"The number of bombs must not exceed {squaresCapacity}.");
            }

            _bombs = new(() =>
            {
                var bombs = new HashSet<int>(bombCapacity);

                while (bombs.Count < bombCapacity)
                {
                    bombs.Add(Random.Shared.Next(0, squaresCapacity));
                }

                return bombs;
            });
        }

        public Bombs(IEnumerable<int> bombs)
        {
            _bombs = new(() => new HashSet<int>(bombs));
        }

        public int Length => _bombs.Value.Count;

        public List<int> ToList() => [.. _bombs.Value];

        public bool OnSquare(Square square) => _bombs.Value.Contains(square.Index);

        public static implicit operator List<int>(Bombs bombs) => bombs.ToList();
    }
}
