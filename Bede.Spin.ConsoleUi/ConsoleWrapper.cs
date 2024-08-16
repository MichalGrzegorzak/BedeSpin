namespace Bede.Spin.ConsoleUi;
public interface IConsoleWrapper
{
    string? ReadLine();
    void Write(string data);
    void WriteLine(string data);
}

public class ConsoleWrapper : IConsoleWrapper
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public void Write(string txt)
    {
        Console.Write(txt);
    }
    public void WriteLine(string txt)
    {
        Console.WriteLine(txt);
    }
}