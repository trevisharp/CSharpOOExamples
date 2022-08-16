using System;
using System.Threading;
using System.Collections.Generic;

Form form = new Form();
form.Size = new Size(100, 20);
form.Text = "Minha Tela";

Application.Start(form);

public static class Application
{
    public static void Start(Form form)
    {
        while (true)
        {
            var formText = form.Draw();
            Console.Clear();
            Console.WriteLine(formText);
            Thread.Sleep(1000);
        }
    }
}

public class Form : Control
{
    public List<Control> Controls 
        { get; private set;} = new List<Control>();

    public override string Draw()
    {
        string form = "┌";
        for (int i = 0; i < Size.Width; i++)
        {
            form += "─";
        }
        form += "┐\n";

        form += "│ " + Text;
        var spaces = Size.Width - Text.Length - 1;
        for (int i = 0; i < spaces; i++)
        {
            form += " ";
        }
        form += "│\n";

        form += "├";
        for (int i = 0; i < Size.Width; i++)
        {
            form += "─";
        }
        form += "┤\n";
        

        for (int j = 0 ; j < Size.Height; j++)
        {
            form += "│";
            for (int i = 0; i < Size.Width; i++)
            {
                form += " ";
            }
            form += "|\n";
        }

        form += "└";
        for (int i = 0; i < Size.Width; i++)
        {
            form += "─";
        }
        form += "┘";

        return form;
    }
}

public class Point
{
    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }
}

public class Size
{
    public Size(int width, int height)
    {
        this.Width = width;
        this.Height = height;
    }

    public int Width { get; set; }
    public int Height { get; set; }
}

public abstract class Control
{
    public Size Size { get; set; }
    public string Text { get; set; }
    public Point Location { get; set; }

    public abstract string Draw();
}

public class Button : Control
{
    public override string Draw()
    {
        string button = "┌";
        for (int i = 0; i < Size.Width; i++)
        {
            button += "─";
        }
        button += "┐\n";

        for (int j = 0 ; j < Size.Height; j++)
        {
            button += "│";
            if (j == Size.Height / 2)
            {
                int spaces = (Size.Width - Text.Length) / 2;
                for (int i = 0; i < spaces; i++)
                {
                    button += " ";
                }
                button += Text;
                if ((Size.Width - Text.Length) % 2 == 1)
                {
                    spaces++;
                }
                for (int i = 0; i < spaces; i++)
                {
                    button += " ";
                }
            }
            else
            {
                for (int i = 0; i < Size.Width; i++)
                {
                    button += " ";
                }
            }
            button += "|\n";
        }

        button += "└";
        for (int i = 0; i < Size.Width; i++)
        {
            button += "─";
        }
        button += "┘";

        return button;
    }
}

public class Label : Control
{
    public override string Draw()
    {
        throw new System.NotImplementedException();
    }
}

public class CheckBox : Control
{
    public override string Draw()
    {
        throw new System.NotImplementedException();
    }
}

public class TextBox : Control
{
    public override string Draw()
    {
        throw new System.NotImplementedException();
    }
}

public class Panel : Control
{
    public override string Draw()
    {
        throw new System.NotImplementedException();
    }
}