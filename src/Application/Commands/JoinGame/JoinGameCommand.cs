namespace Application.Commands.JoinGame;

public readonly record struct JoinGameCommand(
    Guid GameId,
    bool CreateIfNotExists,
    Guid PlayerId) : IRequest<Result>;
