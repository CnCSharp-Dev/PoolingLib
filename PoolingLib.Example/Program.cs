using PoolingLib.Pools;
public static class Program
{
    public static void Main(string[] args)
    {
        for (int k = 0; k < 10; k++)
        {
            var list = ListPool<string>.Pool.Get();
            for (int i = 0; i < 100; i++)            //1 - 100
            {
                list.Add($"{i}");
            }

            Console.WriteLine(list[new Random().Next(0, list.Count)]);

            ListPool<string>.Pool.Release(list);//release
        }
    }
}