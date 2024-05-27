using MindSweeper.Application.Components.Squares;
using MindSweeper.Domain;

namespace MindSweeper.Application.Components;

partial class Field
{
    /// <summary>
    /// Represents the collection of bombs in the field.
    /// </summary>
    public sealed class Bombs
    {
        private readonly Lazy<HashSet<int>> _bombs;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bombs"/> class with the specified settings.
        /// </summary>
        /// <param name="settings">The game settings.</param>
        public Bombs(Settings settings) : this(settings.Bombs, settings.Squares) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bombs"/> class with the specified bomb capacity and squares capacity.
        /// </summary>
        /// <param name="bombCapacity">The number of bombs.</param>
        /// <param name="squaresCapacity">The number of squares.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the bomb capacity is less than the minimum bombs or exceeds the squares capacity.</exception>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Bombs"/> class with the specified bombs.
        /// </summary>
        /// <param name="bombs">The collection of bomb indices.</param>
        public Bombs(IEnumerable<int> bombs)
        {
            _bombs = new(() => new HashSet<int>(bombs));
        }

        /// <summary>
        /// Gets the number of bombs.
        /// </summary>
        public int Length => _bombs.Value.Count;

        /// <summary>
        /// Converts the bombs to a list.
        /// </summary>
        /// <returns>A list of bomb indices.</returns>
        public List<int> ToList() => [.. _bombs.Value];

        /// <summary>
        /// Checks if a bomb is present on the specified square.
        /// </summary>
        /// <param name="square">The square to check.</param>
        /// <returns><c>true</c> if a bomb is present on the square; otherwise, <c>false</c>.</returns>
        public bool OnSquare(Square square) => _bombs.Value.Contains(square.Index);

        /// <summary>
        /// Implicitly converts the <see cref="Bombs"/> object to a list of bomb indices.
        /// </summary>
        /// <param name="bombs">The <see cref="Bombs"/> object.</param>
        public static implicit operator List<int>(Bombs bombs) => bombs.ToList();
    }
}
