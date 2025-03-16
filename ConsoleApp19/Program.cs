using System.Net;

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
            Task<List<int>> uniqueTask = Task.Run(() => numbers.AsParallel().Distinct().ToList());
            //findLongestContinuing(numbers);
            //findLongestPositiveLine(numbers);
            multytable(5, 8);
            await Task.WhenAll(sumTask, minTask, maxTask, evenTask, oddTask);
            //Console.WriteLine($"Sum - {await sumTask}\n Min - {await minTask}\n Max - {await maxTask}\n even - {string.Join(", ", await evenTask)}\n odd - {string.Join(", ", await oddTask)}\n unique - {string.Join(", ", await uniqueTask)}"); 
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
                for(int i = 0; i < 20; i++) 
                { 
                    writer.WriteLine(random.Next(-100, 100)); 
                } 
            }
        }
        static void findLongestContinuing(List<int> numbers)
        {
            if (numbers.Count == 0) return;

            List<int> longestList = new(), tempList = new() { numbers[0] };

            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[i - 1])
                    tempList.Add(numbers[i]);
                else
                {
                    if (tempList.Count > longestList.Count)
                        longestList = new List<int>(tempList);
                    tempList = new List<int> { numbers[i] };
                }
            }

            if (tempList.Count > longestList.Count)
                longestList = tempList;

            Console.WriteLine($"Continuing: {string.Join(" ", longestList)} Length: {longestList.Count}");
        }
        static void findLongestPositiveLine(List<int> numbers)
        {
            var longestLine = new List<int>();
            var tmp = new List<int>();

            foreach(var num in numbers)
            {
                if (num > 0)
                {
                    longestLine.Add(num);
                }
                else
                    if (tmp.Count > longestLine.Count)
                        longestLine = new List<int>(tmp);
                    tmp.Clear();
            }
            if(tmp.Count > longestLine.Count)
            {
                longestLine = tmp;
            }

            Console.WriteLine($"Continuing of positive numbers: {string.Join(" ", longestLine)} Length: {longestLine.Count}");
        }
        static void multytable(int min, int max)
        {
            Parallel.For(min, max + 1, i =>
            {
                for (int j = 1; j < 11; j++)
                {
                    int result = i * j;
                    Console.WriteLine($"{i} * {j} = {result}");
                }
                Console.WriteLine("----------------------------");
            });
        }
    }
}
