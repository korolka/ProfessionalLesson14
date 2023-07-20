//Завдання 4

//Переробіть додаткове завдання з уроку №11 із використанням конструкції async await.
namespace ProfessionalLesson14
{
    internal class Program
    {
        static int counter = 0;

        static object block = new object(); // block - не повинен бути структурним.

        static void Function()
        {
            Console.WriteLine("Method function:" + Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < 50; ++i)
            {
                Console.WriteLine(++counter);
                // Виконання деякої роботи потоком ...
                Thread.Sleep(200);
            }
        }

        public static async void FunctionAsync()
        {
            Task task = new Task(Function);
            task.Start();
            await task;
        }

        static void Main()
        {
            Console.WriteLine("Method main:" + Thread.CurrentThread.ManagedThreadId);
            FunctionAsync();
            // Delay
            Console.ReadKey();
        }
    }
}