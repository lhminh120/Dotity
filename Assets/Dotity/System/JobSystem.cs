
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Dotity
{
    public abstract class JobSystem : IExecuteSystem
    {
        protected IGroup _group;
        private readonly int _numberThread;
        public JobSystem(int numberThread, IMatcher matcher)
        {
            _group = Group.CreateGroup(matcher);
            _numberThread = numberThread;
        }
        public void Execute()
        {
            List<Entity> entities = _group.GetEntities();

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


            //DebugClass.Log("_numberThread " + _numberThread);
            //DebugClass.Log("numberCount " + numberCount);
            //for (int i = 0; i < _numberThread; i++)
            //{
            //    DebugClass.Log("i " + i + " from " + (i * numberCount) + " to " + ((i + 1) * numberCount));
            //}

        }
        private void LoopAllEntities(int from, int to, List<Entity> entities)
        {
            //DebugClass.Log("from " + from + " to " + to + " count " + entities.Count);
            for (int i = from; i < to; i++)
            {
                //if(i < 0 || i >= entities.Count)
                //    DebugClass.Log("count " + entities.Count + " i " + i);
                if (JobExecuteCondition(entities[i]))
                {
                    JobExecute(entities[i]);
                }

            }
        }
        public abstract void JobExecute(Entity entity);
        public abstract bool JobExecuteCondition(Entity entity);
    }
}

