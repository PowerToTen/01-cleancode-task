using System.Linq;

namespace CleanCode
{
	public class Chess
	{
		private readonly Board board;

		public Chess(Board board)
		{
		this.board = board;
		}

		public string GetWhiteStatus() {
			var underCheck = CheckForWhite();
			var canMove =  false;
			foreach (Loc figureLocation in board.Figures(Cell.White))
			{
				foreach (Loc figureMoves in board.Get(figureLocation).Figure.Moves(figureLocation, board)){
				Cell oldDest = board.PerformMove(figureLocation, figureMoves);
				if (!CheckForWhite( ))
					canMove = true;
				board.PerformUndoMove(figureLocation, figureMoves, oldDest);
				}
			}
			if (underCheck)
				if (canMove)
					return "check";
				else return "mate";
			if (canMove)	
                return "ok";
			return "stalemate";
		}

		private bool CheckForWhite()
		{
			foreach (Loc location in board.Figures(Cell.Black))
			{
				var cell = board.Get(location);
				var moves = cell.Figure.Moves(location, board);
				if (moves.Any(to => board.Get(to).IsWhiteKing))
				    return true;
			}
			return false;
		}
	}
}