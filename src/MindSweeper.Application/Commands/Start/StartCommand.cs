﻿using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Start;

/// <summary>
/// Represents a command to start the game.
/// </summary>
public record StartCommand(string PlayerId, Settings Settings) : IRequest<Result<StartCommandResponse>>;