using NBench;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BL;

namespace TaskManager.PerformanceTest
{
    
    public class Class1
    {
        private const string AddCounterName = "AddCounter";
        private Counter addCounter;


        private const int AcceptableMinAddThroughput = 20000;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            addCounter = context.GetCounter(AddCounterName);

        }

        [PerfBenchmark(RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [CounterThroughputAssertion(AddCounterName, MustBe.LessThan, AcceptableMinAddThroughput)]
        public void GetTasksThroughput_ThroughputMode(BenchmarkContext context)
        {
            TaskCrud tasks = new TaskCrud();

            List<TaskManager.DAL.TaskInformation> task = tasks.GetAllTasks().ToList();
            addCounter.Increment();
        }

        [PerfCleanup]
        public void Cleanup(BenchmarkContext context)
        {
            //does nothing
        }
    }
}
