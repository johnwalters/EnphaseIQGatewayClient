namespace IQClientConsole
{
    internal class Program
    {
        private static readonly int _waitBetweenAllRequestsSeconds = 300;
        private static readonly int _waitBetweenEachRequestSeconds = 10;
        private static readonly TimeOnly _startTimeOfDay = new TimeOnly(7, 00);
        private static readonly TimeOnly _endTimeOfDay = new TimeOnly(21, 00);
        private static readonly bool _doInverters = true;
        private static readonly bool _doMeters = false;
        private static readonly bool _doMeterReadings = false;
        private static readonly bool _doStatus = false;
        private static readonly bool _doConsumption = true;


        static void Main(string[] args)
        {
            var processor = new Processor();
            var done = false;
            while (!done)
            {

                if (IsInDaytime())
                {
                    
                    if (_doInverters)
                    {
                        processor.GetInverters();
                        Thread.Sleep(1000 * _waitBetweenEachRequestSeconds);
                    }
                    

                }
                else
                {

                    Console.WriteLine(DateTime.Now.ToString("y-MM-dd hh:mm:ss") + $" Not in daytime - no Inverter requests made.");
                }

                if (_doMeters)
                {
                    processor.GetMeters();
                    Thread.Sleep(1000 * _waitBetweenEachRequestSeconds);
                }
                if (_doMeterReadings)
                {
                    processor.GetMeterReadings();
                    Thread.Sleep(1000 * _waitBetweenEachRequestSeconds);
                }
                if (_doStatus)
                {
                    processor.GetStatus();
                    Thread.Sleep(1000 * _waitBetweenEachRequestSeconds);
                }

                if (_doConsumption)
                {
                    processor.GetConsumption();
                    Thread.Sleep(1000 * _waitBetweenEachRequestSeconds);
                }




                Console.WriteLine(DateTime.Now.ToString("y-MM-dd hh:mm:ss") + $"  Waiting {_waitBetweenAllRequestsSeconds} seconds ...");
                Console.WriteLine("Press ctrl+C to cancel.");
                Thread.Sleep(1000 * _waitBetweenAllRequestsSeconds);
               

            }
            
        }

        static bool IsInDaytime()
        {
            var nowTime = TimeOnly.FromDateTime(DateTime.Now);
            if (nowTime > _endTimeOfDay) return false;
            if (nowTime < _startTimeOfDay) return false;
            return true;
        }

        
    }
}