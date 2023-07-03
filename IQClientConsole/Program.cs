namespace IQClientConsole
{
    internal class Program
    {
        private static readonly int _waitBetweenAllRequestsSeconds = 300;
        private static readonly int _waitBetweenEachRequestSeconds = 10;


        static void Main(string[] args)
        {
            var processor = new Processor();
            var done = false;
            while (!done)
            {
                processor.GetInverters();
                Thread.Sleep(1000 * _waitBetweenEachRequestSeconds);
                processor.GetMeters();
                Thread.Sleep(1000 * _waitBetweenEachRequestSeconds);
                processor.GetMeterReadings();
                Thread.Sleep(1000 * _waitBetweenEachRequestSeconds);
                processor.GetStatus();
                Thread.Sleep(1000 * _waitBetweenEachRequestSeconds);
                processor.GetConsumption();

                Console.WriteLine(DateTime.Now.ToString("y-MM-dd hh:mm:ss") + $" Waiting {_waitBetweenAllRequestsSeconds} seconds ...") ;
                Console.WriteLine("Press ctrl+C to cancel.");
                Thread.Sleep(1000 * _waitBetweenAllRequestsSeconds);
            }
            
        }

        
    }
}