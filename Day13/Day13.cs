using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    public class Cart : IComparable<Cart>
    {
        public int posx;
        public int posy;
        public char heading;
        public int nextTurn; // 0 == left, 1 == straight, 2 == right

        public Cart(int posx, int posy, char heading)
        {
            this.posx = posx;
            this.posy = posy;
            this.heading = heading;
            this.nextTurn = 0;
        }

        public int CompareTo(Cart other)
        {
            if (this.posy < other.posy)
            {
                return -1;
            }
            else if (this.posy > other.posy)
            {
                return 1;
            }
            else
            {
                if (this.posx < other.posx)
                {
                    return -1;
                }
                else if (this.posx > other.posx)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class TrackSquare
    {
        public char visual;
        public Cart cart;

        public TrackSquare(char visual = ' ', Cart cart = null)
        {
            this.visual = visual;
            this.cart = cart;
        }
    }

    class Day13
    {
        static char NewTrackVisual(List<List<TrackSquare>> tracks, int x, int y)
        {
            char c = ' ';
            char up;
            char right;
            char down;
            char left;
            char heading = tracks[y][x].cart.heading;

            if (heading == '^' || heading == 'v')
            {
                c = '|';
            }
            else
            {
                c = '-';
            }

            if (y == 0 || y == tracks.Count - 1 || x == 0 || x == tracks[y].Count - 1)
            {
                return c;
            }
            up = tracks[y - 1][x].visual;

            down = tracks[y + 1][x].visual;

            right = tracks[y][x - 1].visual;

            left = tracks[y][x + 1].visual;

            if (((left == '-' || left == '+' || left == '/' || left == '\\') && (right == '-' || right == '+' || right == '/' || right == '\\')) &&
                    ((up == '|' || up == '+' || up == '/' || up == '\\') && (down == '|' || down == '+' || down == '/' || down == '\\')))
            {
                c = '+';
            }

            return c;
        }

        static void Part1()
        {
            var file = new System.IO.StreamReader("..\\..\\input.txt");
            List<List<TrackSquare>> tracks = new List<List<TrackSquare>>();
            List<Cart> carts = new List<Cart>();
            string line;
            int y = 0;

            while ((line = file.ReadLine()) != null)
            {
                tracks.Add(new List<TrackSquare>());
                List<char> trackRow = line.ToList();
                for (int i = 0; i < trackRow.Count; i++)
                {
                    char c = trackRow[i];
                    Cart cart = null;
                    if (c == '^')
                    {
                        cart = new Cart(i, y, c);
                        carts.Add(cart);
                    }
                    else if (c == '<')
                    {
                        cart = new Cart(i, y, c);
                        carts.Add(cart);
                    }
                    else if (c == 'v')
                    {
                        cart = new Cart(i, y, c);
                        carts.Add(cart);
                    }
                    else if (c == '>')
                    {
                        cart = new Cart(i, y, c);
                        carts.Add(cart);
                    }
                    tracks[y].Add(new TrackSquare(trackRow[i], cart));
                }
                y++;
            }

            foreach (Cart cart in carts)
            {
                char c = NewTrackVisual(tracks, cart.posx, cart.posy);
                tracks[cart.posy][cart.posx].visual = c;
                tracks[cart.posy][cart.posx].cart = cart;
            }

            bool crashed = false;
            int newX;
            int newY;
            char v;
            while (!crashed)
            {
                carts.Sort();

                for (int i = 0; i < carts.Count; i++)
                {
                    newY = carts[i].posy;
                    newX = carts[i].posx;
                    v = tracks[newY][newX].visual;
                    tracks[newY][newX].cart = null;

                    if (v == '|')
                    {
                        if (carts[i].heading == '^')
                        {
                            newY -= 1;
                        }
                        else if (carts[i].heading == 'v')
                        {
                            newY += 1;
                        }
                    }
                    else if (v == '-')
                    {
                        if (carts[i].heading == '<')
                        {
                            newX -= 1;
                        }
                        else if (carts[i].heading == '>')
                        {
                            newX += 1;
                        }
                    }
                    else if (v == '/')
                    {
                        if (carts[i].heading == '^')
                        {
                            newX += 1;
                            carts[i].heading = '>';
                        }
                        else if (carts[i].heading == '>')
                        {
                            newY -= 1;
                            carts[i].heading = '^';
                        }
                        else if (carts[i].heading == 'v')
                        {
                            newX -= 1;
                            carts[i].heading = '<';
                        }
                        else if (carts[i].heading == '<')
                        {
                            newY += 1;
                            carts[i].heading = 'v';
                        }
                    }
                    else if (v == '\\')
                    {
                        if (carts[i].heading == '^')
                        {
                            newX -= 1;
                            carts[i].heading = '<';
                        }
                        else if (carts[i].heading == '>')
                        {
                            newY += 1;
                            carts[i].heading = 'v';
                        }
                        else if (carts[i].heading == 'v')
                        {
                            newX += 1;
                            carts[i].heading = '>';
                        }
                        else if (carts[i].heading == '<')
                        {
                            newY -= 1;
                            carts[i].heading = '^';
                        }
                    }
                    else if (v == '+')
                    {
                        if (carts[i].heading == '^')
                        {
                            if (carts[i].nextTurn == 0)
                            {
                                newX -= 1;
                                carts[i].heading = '<';
                            }
                            else if (carts[i].nextTurn == 1)
                            {
                                newY -= 1;
                            }
                            else
                            {
                                newX += 1;
                                carts[i].heading = '>';
                            }
                        }
                        else if (carts[i].heading == '>')
                        {
                            if (carts[i].nextTurn == 0)
                            {
                                newY -= 1;
                                carts[i].heading = '^';
                            }
                            else if (carts[i].nextTurn == 1)
                            {
                                newX += 1;
                            }
                            else
                            {
                                newY += 1;
                                carts[i].heading = 'v';
                            }
                        }
                        else if (carts[i].heading == 'v')
                        {
                            if (carts[i].nextTurn == 0)
                            {
                                newX += 1;
                                carts[i].heading = '>';
                            }
                            else if (carts[i].nextTurn == 1)
                            {
                                newY += 1;
                            }
                            else
                            {
                                newX -= 1;
                                carts[i].heading = '<';
                            }
                        }
                        else if (carts[i].heading == '<')
                        {
                            if (carts[i].nextTurn == 0)
                            {
                                newY += 1;
                                carts[i].heading = 'v';
                            }
                            else if (carts[i].nextTurn == 1)
                            {
                                newX -= 1;
                            }
                            else
                            {
                                newY -= 1;
                                carts[i].heading = '^';
                            }
                        }
                        carts[i].nextTurn = (carts[i].nextTurn + 1) % 3;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("PANIC");
                    }

                    carts[i].posx = newX;
                    carts[i].posy = newY;

                    if (tracks[newY][newX].cart != null)
                    {
                        crashed = true;
                        System.Diagnostics.Debug.WriteLine(newX + "," + newY);
                    }
                    else
                    {
                        tracks[newY][newX].cart = carts[i];
                    }
                }
            }
        }

        static void Part2()
        {
            var file = new System.IO.StreamReader("..\\..\\input.txt");
            List<List<TrackSquare>> tracks = new List<List<TrackSquare>>();
            List<Cart> carts = new List<Cart>();
            string line;
            int y = 0;

            while ((line = file.ReadLine()) != null)
            {
                tracks.Add(new List<TrackSquare>());
                List<char> trackRow = line.ToList();
                for (int i = 0; i < trackRow.Count; i++)
                {
                    char c = trackRow[i];
                    Cart cart = null;
                    if (c == '^')
                    {
                        cart = new Cart(i, y, c);
                        carts.Add(cart);
                    }
                    else if (c == '<')
                    {
                        cart = new Cart(i, y, c);
                        carts.Add(cart);
                    }
                    else if (c == 'v')
                    {
                        cart = new Cart(i, y, c);
                        carts.Add(cart);
                    }
                    else if (c == '>')
                    {
                        cart = new Cart(i, y, c);
                        carts.Add(cart);
                    }
                    tracks[y].Add(new TrackSquare(trackRow[i], cart));
                }
                y++;
            }

            foreach (Cart cart in carts)
            {
                char c = NewTrackVisual(tracks, cart.posx, cart.posy);
                tracks[cart.posy][cart.posx].visual = c;
                tracks[cart.posy][cart.posx].cart = cart;
            }

            bool crashed = false;
            int newX;
            int newY;
            int oldX;
            int oldY;
            char v;
            while (carts.Count > 1)
            {
                carts.Sort();

                for (int i = 0; i < carts.Count; i++)
                {
                    oldY = carts[i].posy;
                    oldX = carts[i].posx;
                    if (tracks[oldY][oldX].cart == null)
                    {
                        carts.RemoveAt(i);
                        i--;
                    }
                    else
                    {

                        tracks[oldY][oldX].cart = null;
                        v = tracks[oldY][oldX].visual;

                        newX = oldX;
                        newY = oldY;

                        if (v == '|')
                        {
                            if (carts[i].heading == '^')
                            {
                                newY -= 1;
                            }
                            else if (carts[i].heading == 'v')
                            {
                                newY += 1;
                            }
                        }
                        else if (v == '-')
                        {
                            if (carts[i].heading == '<')
                            {
                                newX -= 1;
                            }
                            else if (carts[i].heading == '>')
                            {
                                newX += 1;
                            }
                        }
                        else if (v == '/')
                        {
                            if (carts[i].heading == '^')
                            {
                                newX += 1;
                                carts[i].heading = '>';
                            }
                            else if (carts[i].heading == '>')
                            {
                                newY -= 1;
                                carts[i].heading = '^';
                            }
                            else if (carts[i].heading == 'v')
                            {
                                newX -= 1;
                                carts[i].heading = '<';
                            }
                            else if (carts[i].heading == '<')
                            {
                                newY += 1;
                                carts[i].heading = 'v';
                            }
                        }
                        else if (v == '\\')
                        {
                            if (carts[i].heading == '^')
                            {
                                newX -= 1;
                                carts[i].heading = '<';
                            }
                            else if (carts[i].heading == '>')
                            {
                                newY += 1;
                                carts[i].heading = 'v';
                            }
                            else if (carts[i].heading == 'v')
                            {
                                newX += 1;
                                carts[i].heading = '>';
                            }
                            else if (carts[i].heading == '<')
                            {
                                newY -= 1;
                                carts[i].heading = '^';
                            }
                        }
                        else if (v == '+')
                        {
                            if (carts[i].heading == '^')
                            {
                                if (carts[i].nextTurn == 0)
                                {
                                    newX -= 1;
                                    carts[i].heading = '<';
                                }
                                else if (carts[i].nextTurn == 1)
                                {
                                    newY -= 1;
                                }
                                else
                                {
                                    newX += 1;
                                    carts[i].heading = '>';
                                }
                            }
                            else if (carts[i].heading == '>')
                            {
                                if (carts[i].nextTurn == 0)
                                {
                                    newY -= 1;
                                    carts[i].heading = '^';
                                }
                                else if (carts[i].nextTurn == 1)
                                {
                                    newX += 1;
                                }
                                else
                                {
                                    newY += 1;
                                    carts[i].heading = 'v';
                                }
                            }
                            else if (carts[i].heading == 'v')
                            {
                                if (carts[i].nextTurn == 0)
                                {
                                    newX += 1;
                                    carts[i].heading = '>';
                                }
                                else if (carts[i].nextTurn == 1)
                                {
                                    newY += 1;
                                }
                                else
                                {
                                    newX -= 1;
                                    carts[i].heading = '<';
                                }
                            }
                            else if (carts[i].heading == '<')
                            {
                                if (carts[i].nextTurn == 0)
                                {
                                    newY += 1;
                                    carts[i].heading = 'v';
                                }
                                else if (carts[i].nextTurn == 1)
                                {
                                    newX -= 1;
                                }
                                else
                                {
                                    newY -= 1;
                                    carts[i].heading = '^';
                                }
                            }
                            carts[i].nextTurn = (carts[i].nextTurn + 1) % 3;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("PANIC");
                        }

                        carts[i].posx = newX;
                        carts[i].posy = newY;

                        if (tracks[newY][newX].cart != null)
                        {
                            tracks[newY][newX].cart = null;
                            carts.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            tracks[newY][newX].cart = carts[i];
                        }
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(carts[0].posx + "," + carts[0].posy);
        }

        static void Main(string[] args)
        {
            // Part1();
            Part2();
        }
    }
}
