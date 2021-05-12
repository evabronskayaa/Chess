using Chess.Logic;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace Chess.Tests
{
    public class Tests
    {
        [TestCase]
        public void RookCorrectMoveTests()
        {
            Board.FillBoard();
            Board.Figures[4, 4] = new Rook(false);

            Assert.AreEqual(true, Board.Figures[4, 4].IsCorrectMove(new Point(4, 4), new Point(4, 5)));
            Assert.AreEqual(true, Board.Figures[4, 4].IsCorrectMove(new Point(4, 4), new Point(5, 4)));
            Assert.AreEqual(false, Board.Figures[4, 4].IsCorrectMove(new Point(4, 4), new Point(4, 1)));
        }

        [TestCase(3, 4, ExpectedResult = true)]
        [TestCase(6, 5, ExpectedResult = true)]
        public bool CorrectMoveTests(int x, int y)
        {
            Board.Figures[x, y] = new Knight(false);
            Board.DataWorker.SaveBoard();
            Board.DataWorker.LoadBoard();
            return Board.Figures[x, y] is Knight;
        }

        [TestCaseSource(nameof(TestCases))]
        public void CorrectMoveTests(bool result, Point start, Point end)
        {
            Board.FillBoard();
            Assert.AreEqual( result , Board.Figures[start.X, start.Y].IsCorrectMove(start, end));
        }
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(false, new Point(0, 1), new Point(1, 1));
                yield return new TestCaseData(true, new Point(0, 1), new Point(0, 2));
            }
        }
    }
}