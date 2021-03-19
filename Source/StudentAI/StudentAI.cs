using System;
using System.Collections.Generic;
using GameAI.GamePlaying.Core;

namespace GameAI.GamePlaying
{
    public class StudentAI : Behavior
    {
        //private int Evaluate(Board board)
        //{
        //    for (int i = 0; i < 8; i++)
        //        for (int j = 0; j < 8; j++)
        //        {
        //           if()
        //        }
        //    return board.Score * 10000;
        //}
        public StudentAI()
        {

        }
        public ComputerMove Run(int _color, Board _board, int _lookAheadDepth)
        {
            ComputerMove bestMove = null;
            Board newState = new Board();
            List<ComputerMove> moveList = new List<ComputerMove>();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if(_board.IsValidMove(_color,i,j))
                    {
                        ComputerMove curr = new ComputerMove(i,j);
                        moveList.Add(curr);
                    }
                }

            for (int i = 0; i < moveList.Count; i++)
            {
                newState.Copy(_board);
                newState.MakeMove(_color, moveList[i].row, moveList[i].column);
                int depth = 0;
                if (newState.IsTerminalState() || depth == _lookAheadDepth)
                {
                    moveList[i].rank = ExampleAI.MinimaxExample.EvaluateTest(_board);
                }
                else
                {
                    moveList[i].rank = Run(_color, newState, _lookAheadDepth--).rank;
                }
                if (bestMove == null || moveList[i].rank > bestMove.rank)
                {
                    bestMove = moveList[i];
                }
            }
            return bestMove;
        }
    }
}
