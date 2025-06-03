
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Dotity
{
    public abstract class JobSystem : BaseSystem, IExecuteSystem
    {
        private readonly int _numberThread;
        public JobSystem(int numberThread, IMatcher matcher) : base(matcher)
        {
            _numberThread = numberThread;
        }
        public void Execute()
        {
            List<IEntity> entities = _group.GetEntities();

            int count = entities.Count;
            if (count <= _numberThread)
            {
                for (int i = 0; i < count; i++)
                {
                    if (JobExecuteCondition(entities[i]))
                    {
                        JobExecute(entities[i]);
                    }

                }
                return;
            }

            int numberCount = count / _numberThread;
            int remain = count % _numberThread;

            if (remain == 0)
            {
                Task[] tasks = new Task[_numberThread];
                int[] froms = new int[_numberThread];
                int[] tos = new int[_numberThread];
                for (int i = 0; i < _numberThread; i++)
                {
                    int index = i;
                    froms[index] = index * numberCount;
                    tos[index] = (index + 1) * numberCount;
                    tasks[index] = Task.Run(() => LoopAllEntities(froms[index], tos[index], entities));
                }
                Task.WaitAll(tasks);
            }
            else
            {
                Task[] tasks = new Task[_numberThread + 1];
                int[] froms = new int[_numberThread];
                int[] tos = new int[_numberThread];
                for (int i = 0; i < _numberThread; i++)
                {
                    int index = i;
                    froms[index] = index * numberCount;
                    tos[index] = (index + 1) * numberCount;
                    tasks[index] = Task.Run(() => LoopAllEntities(froms[index], tos[index], entities));
                }
                tasks[_numberThread] = Task.Run(() => LoopAllEntities(_numberThread * numberCount, _numberThread * numberCount + remain, entities));
                Task.WaitAll(tasks);
            }

        }
        private void LoopAllEntities(int from, int to, List<IEntity> entities)
        {
            for (int i = from; i < to; i++)
            {
                if (JobExecuteCondition(entities[i]))
                {
                    JobExecute(entities[i]);
                }

            }
        }
        public abstract void JobExecute(IEntity entity);
        public abstract bool JobExecuteCondition(IEntity entity);
    }
}

