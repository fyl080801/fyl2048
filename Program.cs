using System;
using System.Collections.Generic;

namespace fyl2048
{
    class Program
    {
        static NumberItem[][] a = new NumberItem[4][];

        static void Main(string[] args)
        {
            for (var i = 0; i < 4; i++)
            {
                a[i] = new NumberItem[4];
                for (var j = 0; j < 4; j++)
                {
                    a[i][j] = new NumberItem(0);
                }
            }

            var rnd = new Random();
            int rnx1 = rnd.Next(0, 3), rny1 = rnd.Next(0, 3),
                rnx2, rny2;
            do
            {
                rnx2 = rnd.Next(0, 3);
                rny2 = rnd.Next(0, 3);
            } while (rnx1 == rnx2 && rny1 == rny2);
            a[rnx1][rny1] = new NumberItem(rnd.Next(1, 2) * 2);
            a[rnx2][rny2] = new NumberItem(rnd.Next(1, 2) * 2);

            WriteResult();

            var isend = false;
            while (!isend)
            {
                var adds = 0;
                var moves = 0;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            moves = moveLeft();
                            adds = addLeft();
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            moves = moveRight();
                            adds = addRight();
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            moves = moveUp();
                            adds = addUp();
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            moves = moveDown();
                            adds = addDown();
                            break;
                        }
                    default:
                        {
                            isend = true;
                            break;
                        }
                }

                if (moves > 0 || adds > 0)
                {
                    var end = SetGame();
                    WriteResult();
                    if (end)
                    {
                        break;
                    }
                }
                else
                {
                    WriteResult();
                }
            }

            Console.WriteLine("game over");
            Console.WriteLine("value:");
            Console.ReadKey();
        }

        static void WriteResult()
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    Console.Write(a[i][j].Value == 0 ? " " : a[i][j].Value.ToString());
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------");
        }

        static bool SetGame()
        {
            var empty = new List<NumberItem>();
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (a[i][j].IsEmpty())
                    {
                        empty.Add(a[i][j]);
                    }
                }
            }
            if (empty.Count > 0)
            {
                var rnd = new Random();
                empty.ToArray()[rnd.Next(0, empty.Count - 1)].Reset(rnd.Next(1, 2) * 2);
            }

            // 结束检测
            if (empty.Count <= 1)
            {
                int i, j;
                for (i = 0; i < 4; i++)
                {
                    for (j = 0; j < 3; j++)
                    {
                        if (a[i][j].Check(a[i][j + 1]))
                        {
                            return false;
                        }
                    }
                }

                for (j = 0; j < 4; j++)
                {
                    for (i = 0; i < 3; i++)
                    {
                        if (a[i][j].Check(a[i + 1][j]))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        static int moveLeft()
        {
            int moves = 0;
            int i, j, k;
            for (i = 0; i < 4; i++)
            {
                for (j = 1; j < 4; j++)
                {
                    k = j;
                    while (k - 1 >= 0 && a[i][k - 1].IsEmpty())
                    {
                        if (!a[i][k].IsEmpty() || !a[i][k - 1].IsEmpty())
                            moves++;
                        a[i][k].Swap(a[i][k - 1]);
                        k--;
                    }
                }
            }
            return moves;
        }

        static int addLeft()
        {
            int adds = 0;
            int i, j;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (a[i][j].Check(a[i][j + 1]))
                    {
                        a[i][j].Combin(a[i][j + 1]);
                        adds++;
                        moveLeft();
                    }
                }
            }
            return adds;
        }

        static int moveRight()
        {
            int moves = 0;
            int i, j, k;
            for (i = 0; i < 4; i++)
            {
                for (j = 2; j >= 0; j--)
                {
                    k = j;
                    while (k + 1 <= 3 && a[i][k + 1].IsEmpty())
                    {
                        if (!a[i][k + 1].IsEmpty() || !a[i][k].IsEmpty())
                            moves++;
                        a[i][k + 1].Swap(a[i][k]);
                        k++;
                    }
                }
            }
            return moves;
        }

        static int addRight()
        {
            int adds = 0;
            int i, j;
            for (i = 0; i < 4; i++)
            {
                for (j = 3; j >= 1; j--)
                {
                    if (a[i][j].Check(a[i][j - 1]))
                    {
                        a[i][j].Combin(a[i][j - 1]);
                        adds++;
                        moveRight();
                    }
                }
            }
            return adds;
        }

        static int moveUp()
        {
            int moves = 0;
            int i, j, k;
            for (j = 0; j < 4; j++)
            {
                for (i = 1; i < 4; i++)
                {
                    k = i;
                    while (k - 1 >= 0 && a[k - 1][j].IsEmpty())
                    {
                        if (!a[k][j].IsEmpty() || !a[k - 1][j].IsEmpty())
                            moves++;
                        a[k][j].Swap(a[k - 1][j]);
                        k--;
                    }
                }
            }
            return moves;
        }

        static int addUp()
        {
            int adds = 0;
            int i, j;
            for (j = 0; j < 4; j++)
            {
                for (i = 0; i < 3; i++)
                {
                    if (a[i][j].Check(a[i + 1][j]))
                    {
                        a[i][j].Combin(a[i + 1][j]);
                        adds++;
                        moveUp();
                    }
                }
            }
            return adds;
        }

        static int moveDown()
        {
            int moves = 0;
            int i, j, k;
            for (j = 0; j < 4; j++)
            {
                for (i = 2; i >= 0; i--)
                {
                    k = i;
                    while (k + 1 <= 3 && a[k + 1][j].IsEmpty())
                    {
                        if (!a[k + 1][j].IsEmpty() || !a[k][j].IsEmpty())
                            moves++;
                        a[k + 1][j].Swap(a[k][j]);
                        k++;
                    }
                }
            }
            return moves;
        }

        static int addDown()
        {
            int adds = 0;
            int i, j;
            for (j = 0; j < 4; j++)
            {
                for (i = 3; i >= 1; i--)
                {
                    if (a[i - 1][j].Check(a[i][j]))
                    {
                        a[i - 1][j].Combin(a[i][j]);
                        adds++;
                        moveDown();
                    }
                }
            }
            return adds;
        }
    }
}
