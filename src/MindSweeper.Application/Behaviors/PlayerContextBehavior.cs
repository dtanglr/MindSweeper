﻿using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using MindSweeper.Domain;

namespace MindSweeper.Application.Behaviors;

public sealed class PlayerContextBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : class
{
    private readonly PlayerContext _context;
    private readonly IGameRepository _repository;
    private readonly ILogger<PlayerContextBehavior<TRequest>> _logger;

    public PlayerContextBehavior(PlayerContext context, IGameRepository repository, ILogger<PlayerContextBehavior<TRequest>> logger)
    {
        if (string.IsNullOrEmpty(context.Id))
        {
            throw new ArgumentException("Player ID cannot be null or empty.", nameof(context));
        }

        _context = context;
        _repository = repository;
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.GetGameAsync(_context.Id, cancellationToken);
            _context.Game = result.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred getting the game for player: {PlayerId}.", _context.Id);
        }
    }
}