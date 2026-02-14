using System;
using System.Threading.Tasks;

class Program
{
    public static string folderPath = @"/Users/abdulwahid/Downloads";
    public static string fileName = "health.txt";
    
    private static bool _disposed = false;
    
    
    static async Task Main()
    {
    
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(2));
       
        Console.WriteLine("Started...");
       
       while (!_disposed)
       {
           while (await timer.WaitForNextTickAsync())
           {
               CheckEnvironmentWorkerHealth();
               if (_disposed)
                   break;
           }
       }
       
       Console.WriteLine("Stopped...");
   
    }

    public static void CheckEnvironmentWorkerHealth()
    {
        Console.WriteLine($"Checking at {DateTime.Now}");
        string fullPath = Path.Combine(folderPath, fileName);

        if (File.Exists(fullPath))
        {
            string content = File.ReadAllText(fullPath);

            if (content.Contains("Error") || content.Contains("Failed"))
            {
                Console.WriteLine($"{DateTime.Now}:Not Healthy");
                _disposed = true;
            }

            Console.WriteLine(content);
        }
    }
}