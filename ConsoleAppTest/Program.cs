
using System.Numerics;

public class Input
{
    char input;
    public static string ReadLine() => Console.ReadLine();
    public static ConsoleKeyInfo ReadKey(bool intercept = false) => Console.ReadKey(intercept);
    public static void WaitForKeyPress() => Console.ReadKey();
    public static void WaitForKey() => Console.Read();

    public static bool GetInput(string expectedKey)
    {
        if (Console.KeyAvailable)
        {
            var keyInfo = Console.ReadKey(true);
            if (keyInfo.KeyChar.ToString().ToLower() == expectedKey.ToLower())
            {
                return true;
            }
        }
        return false;
    }
}


public class Map
{
    int _width;
    int _height;
    public int Width { get => _width; set => _width = value; }
    public int Height { get => _height; set => _height = value; }

    public Map(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public int Area() => _width * _height;

    public void Resize(int newWidth, int newHeight)
    {
        _width = newWidth;
        _height = newHeight;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Map Width: {_width}, Height: {_height}, Area: {Area()}");
    }

    public void Clear()
    {
        Console.Clear();
    }

    public void SetCursorPosition(int x, int y)
    {
        Console.SetCursorPosition(x, y);
    }

    public void SetColors(ConsoleColor foreground, ConsoleColor background)
    {
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
    }

    public void ResetColors()
    {
        Console.ResetColor();
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }

    public void Beep(int frequency, int duration)
    {
        Console.Beep(frequency, duration);
    }

    public void Exit()
    {
        Environment.Exit(0);
    }

    public void ClearLine(int line)
    {
        int currentLine = Console.CursorTop;
        Console.SetCursorPosition(0, line);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLine);
    }

    public void SetTitle(string title)
    {
        Console.Title = title;
    }

    public string Title() => Console.Title;

    public (int Width, int Height) GetWindowSize() => (Console.WindowWidth, Console.WindowHeight);
    public void SetWindowSize(int width, int height) => Console.SetWindowSize(width, height);

    public void HideCursor() => Console.CursorVisible = false;
    public void ShowCursor() => Console.CursorVisible = true;

    public void SetCursorSize(int size) => Console.CursorSize = size;
    public int GetCursorSize() => Console.CursorSize;

    public (int Left, int Top) GetCursorPosition() => (Console.CursorLeft, Console.CursorTop);

    public void DrawBox(int x, int y, int width, int height, char borderChar = '#')
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                {
                    Console.SetCursorPosition(x + j, y + i);
                    Console.Write(borderChar);
                }
            }
        }
    }

    public void DrawTriangle(int x, int y, int height, char borderChar = '*')
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = -i; j <= i; j++)
            {
                Console.SetCursorPosition(x + j, y + i);
                Console.Write(borderChar);
            }
        }
    }

    public void DrawCircle(int centerX, int centerY, int radius, char borderChar = 'o')
    {
        double angleStep = 1.0 / radius;
        for (double angle = 0; angle < 2 * Math.PI; angle += angleStep)
        {
            int x = centerX + (int)(radius * Math.Cos(angle));
            int y = centerY + (int)(radius * Math.Sin(angle));
            Console.SetCursorPosition(x, y);
            Console.Write(borderChar);
        }
    }

    public void DrawText(int x, int y, string text)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(text);
    }

    public void Update()
    {
        SetColors(ConsoleColor.DarkGreen, ConsoleColor.DarkGreen);
        Clear();
        SetColors(ConsoleColor.Red, ConsoleColor.Red);
        DrawBox(50, 10, 9, 4);
        SetColors(ConsoleColor.Yellow, ConsoleColor.Yellow);
        DrawTriangle(54, 5, 5);
        SetColors(ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
    }
}


public class Player
{
    int _pv;
    int _atk;
    int _x = 52;
    int _y = 12;

    public void Damage(Player @this, int damage)
    {
        _pv -= damage;
    }

    public int PV { get => _pv; set => _pv = value; }
    public int ATK { get => _atk; set => _atk = value; }

    public void DrawPlayer(ref Map map, char symbol = '@')
    {
        map.SetCursorPosition(_x, _y);
        map.Write(symbol.ToString());
    }

    public void Move(int deltaX, int deltaY)
    {
        _x += deltaX;
        _y += deltaY;
    }

    public void Update()
    {

    }
}


class Game
{
    static void Main(string[] args)
    {
        Player player = new Player();
        player.PV = 12;
        player.Damage(player, 3);

        Map map = new Map(10, 5);
        map.SetTitle("Console Games");
        map.HideCursor();

        while (true)
        {
            map.Update();
            player.DrawPlayer(ref map);
            
            
            char input = Console.ReadKey().KeyChar;
            
            switch (input)
            {
                case 'z':
                    player.Move(0, -1);
                    break;
                case 'q':
                    player.Move(-1, 0);
                    break;
                case 's':
                    player.Move(0, 1);
                    break;
                case 'd':
                    player.Move(1, 0);
                    break;
                default: break;
            }
            // if (Input.GetInput("z")) { map.Update(); player.Move(0, -1); };
            // if (Input.GetInput("q")) { map.Update(); player.Move(-1, 0); };
            // if (Input.GetInput("s")) { map.Update(); player.Move(0, 1); };
            // if (Input.GetInput("d")) { map.Update(); player.Move(1, 0); };
            
            //if (Input.GetInput("a")) { map.Exit(); };
        }

    }
}
