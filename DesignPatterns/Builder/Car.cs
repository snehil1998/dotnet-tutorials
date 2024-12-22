using System.Runtime.InteropServices;

namespace DesignPatterns.Builder;

public class Car
{
    public string? Model { get; set; }
    public string? Make { get; set; }
    public string? Color {get; set; }
    public int Year { get; set; }
    public string? Engine { get; set; }

    public override string ToString()
    {
        return $"Car Make: {Make}, Model: {Model}, Year: {Year}, Engine: {Engine}, Color: {Color}";
    }
}