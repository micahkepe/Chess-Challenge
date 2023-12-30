using System;
using ChessChallenge.API;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        int depth = 3;
        Move bestMove = Move.NullMove;
        int bestScore = int.MinValue;

        // Go through all legal moves and evaluate them
        foreach (var move in board.GetLegalMoves())
        {
            board.MakeMove(move);
            int score = AlphaBeta(board, depth - 1, int.MinValue, int.MaxValue, false);
            board.UndoMove(move);

            // Keep track of the best move
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        return bestMove;
    }

    private bool IsGameOver(Board board)
    {
        return board.IsInCheckmate() || board.IsInStalemate() || board.IsDraw();
    }

    private int AlphaBeta(Board board, int depth, int alpha, int beta, bool maximizingPlayer)
    {
        // If we reached the end of the search tree, evaluate the board
        if (depth == 0 || IsGameOver(board))
        {
            return EvaluateBoard(board);
        }

        // If we are the maximizing player, we want to maximize the score
        if (maximizingPlayer)
        {
            int maxEval = int.MinValue;
            foreach (var move in board.GetLegalMoves())
            {
                board.MakeMove(move);
                int eval = AlphaBeta(board, depth - 1, alpha, beta, false);
                board.UndoMove(move);
                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);
                if (beta <= alpha)
                    break;
            }
            return maxEval;
        }
        // If we are the minimizing player, we want to minimize the score
        else
        {
            int minEval = int.MaxValue;
            foreach (var move in board.GetLegalMoves())
            {
                board.MakeMove(move);
                int eval = AlphaBeta(board, depth - 1, alpha, beta, true);
                board.UndoMove(move);
                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);
                if (beta <= alpha)
                    break;
            }
            return minEval;
        }
    }

    private int EvaluateBoard(Board board)
    {
        // Implement your evaluation logic here
        return 0; // Placeholder
    }
}