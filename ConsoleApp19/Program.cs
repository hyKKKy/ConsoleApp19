namespace ConsoleApp19
{
    internal class Program
    {
        static async Task Main()
        {
            string fileName = "numbers.txt";
            fillFiles(fileName);

            var numbers = (await File.ReadAllLinesAsync(fileName))
                          .AsParallel()
                          .Select(int.Parse)
                          .ToList();

            Task<int> sumTask = Task.Run(() => numbers.AsParallel().Sum());
            Task<int> minTask = Task.Run(() => numbers.AsParallel().Min());
            Task<int> maxTask = Task.Run(() => numbers.AsParallel().Max());
            Task<List<int>> evenTask = Task.Run(() => numbers.AsParallel().Where(x => x % 2 == 0).ToList());
            Task<List<int>> oddTask = Task.Run(() => numbers.AsParallel().Where(x => x % 2 != 0).ToList());
            Task<List<int>> uniqueTask = Task.Run(() =>);

            await Task.WhenAll(sumTask, minTask, maxTask, evenTask, oddTask);
            Console.WriteLine($"Sum - {await sumTask}\n Min - {await minTask}\n Max - {await maxTask}\n even - {string.Join(", ", await evenTask)}\n odd - {string.Join(", ", await oddTask)}");

        }
        static void fillFiles(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }

            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(random.Next(1, 100));
            }
        }
    }
    

}
