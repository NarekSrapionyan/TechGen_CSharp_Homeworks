namespace TicTacToe.Game;

public class ComputerPlayer : Player
{
    public ComputerPlayer(CellSymbol symbol) : base("Computer", symbol)
    {
    }

    public int GetMove(Board board, CellSymbol opponentSymbol)
    {
        int winningMove = FindBestMove(board, Symbol);

        if (winningMove != -1)
        {
            return winningMove;
        }

        int blockingMove = FindBestMove(board, opponentSymbol);

        if (blockingMove != -1)
        {
            return blockingMove;
        }

        if (board.IsCellEmpty(4))
        {
            return 4;
        }

        int[] corners = { 0, 2, 6, 8 };

        for (int i = 0; i < corners.Length; i++)
        {
            if (board.IsCellEmpty(corners[i]))
            {
                return corners[i];
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (board.IsCellEmpty(i))
            {
                return i;
            }
        }

        return -1;
    }

    private int FindBestMove(Board board, CellSymbol symbol)
    {
        int[,] winLines =
        {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },

            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },

            { 0, 4, 8 },
            { 2, 4, 6 }
        };

        for (int i = 0; i < 8; i++)
        {
            int first = winLines[i, 0];
            int second = winLines[i, 1];
            int third = winLines[i, 2];

            int emptyIndex = -1;
            int symbolCount = 0;

            if (board.GetCell(first) == symbol)
            {
                symbolCount++;
            }
            else if (board.GetCell(first) == CellSymbol.Empty)
            {
                emptyIndex = first;
            }

            if (board.GetCell(second) == symbol)
            {
                symbolCount++;
            }
            else if (board.GetCell(second) == CellSymbol.Empty)
            {
                emptyIndex = second;
            }

            if (board.GetCell(third) == symbol)
            {
                symbolCount++;
            }
            else if (board.GetCell(third) == CellSymbol.Empty)
            {
                emptyIndex = third;
            }

            if (symbolCount == 2 && emptyIndex != -1)
            {
                return emptyIndex;
            }
        }

        return -1;
    }
}