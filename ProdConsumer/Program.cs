using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // BlockingCollection은 스레드 안전한 컬렉션입니다.
        BlockingCollection<string> queue = new BlockingCollection<string>();

        // 프로듀서 작업을 시작합니다.
        Task producerTask = Task.Run(() => Producer(queue));

        // 컨슈머 작업을 시작합니다.
        Task consumerTask = Task.Run(() => Consumer(queue));

        // 프로듀서와 컨슈머 작업이 완료될 때까지 대기합니다.
        Task.WaitAll(producerTask, consumerTask);

        Console.WriteLine("프로듀서와 컨슈머 작업이 완료되었습니다.");
    }

    static void Producer(BlockingCollection<string> queue)
    {
        for (int i = 0; i < 100; i++)
        {
            string item = $"Item {i}";
            queue.Add(item);
            Console.WriteLine($"Produced: {item}");
            Thread.Sleep(100); // 프로듀서가 데이터를 생성하는데 걸리는 시간 시뮬레이션
        }
        // 데이터 추가 완료를 알리기 위해 CompleteAdding을 호출합니다.
        queue.CompleteAdding();
    }

    static void Consumer(BlockingCollection<string> queue)
    {
        foreach (var item in queue.GetConsumingEnumerable())
        {
            Console.WriteLine($"Consumed: {item}");
            Thread.Sleep(150); // 컨슈머가 데이터를 처리하는데 걸리는 시간 시뮬레이션
        }
    }
}
