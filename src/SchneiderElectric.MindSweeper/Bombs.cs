﻿using SchneiderElectric.MindSweeper.Squares;

namespace SchneiderElectric.MindSweeper;

partial class Field
{
    public sealed class Bombs
    {
        private readonly Lazy<HashSet<int>> _bombs;

        public Bombs() : this(new Settings()) { }

        public Bombs(Settings settings) : this(settings.Columns, settings.Rows, settings.Bombs) { }

        public Bombs(int columnCapacity, int rowCapacity, int bombCapacity)
        {
            _bombs = new(() =>
            {
                var bombs = new HashSet<int>(bombCapacity);

                while (bombs.Count < bombCapacity)
                {
                    bombs.Add(Random.Shared.Next(0, columnCapacity * rowCapacity));
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
