using RockScissorsPaper.Api.Game;

namespace RockScissorsPaper.Api.Dtos.Responses;
public static class GameViewExtensions
{
    public static GameResultView ToResultView(this GameState @this) =>
        new GameResultView(new[] {
            new MoveDto(@this.PlayerOneMove.Player, (Moves)@this.PlayerOneMove.Move!),
            new MoveDto(@this.PlayerTwoMove.Player!, (Moves)@this.PlayerTwoMove.Move!)
        }, @this.Winner!);


    public static GameStatusView ToStatusView(this GameState @this) => new GameStatusView(@this.PlayerOneMove.Player, @this.PlayerTwoMove.Player);

}