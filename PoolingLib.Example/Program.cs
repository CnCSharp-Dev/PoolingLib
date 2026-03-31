using PoolingLib.Extensions;
using PoolingLib.Pools;
public static class Program
{
    public static void Main(string[] args)
    {
        var list = ListPool<int>.Pool.Get();
        var builder = StringBuilderPool.Pool.Get();

        for(int i = 0; i < 100; i++)
        {
            list.Add(i);
            builder.AppendLine(i.ToString());
        }
        builder.AppendLine($"Count: {list.Count}");

        Console.WriteLine(builder.ReturnTo(true));
        ListPool<int>.Pool.Return(list);
    }
}