using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Dotity
{
    public abstract class JobSystem : IExcuteSystem
    {
        protected IGroup _group;
        private int _numberThread;
        public JobSystem(int numberThread, IMatcher matcher)
        {
            _group = Group.CreateGroup(matcher);
            _numberThread = numberThread;
        }
        public void Excute()
        {
            List<IEntity> entities = _group.GetEntities();

            int count = entities.Count;
            if(count <= _numberThread)
            {
                for (int i = 0; i < count; i++)
                {
                    JobExcute(entities[i]);
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
        private void LoopAllEntities(int from, int to, List<IEntity> entities)
        {
            //DebugClass.Log("from " + from + " to " + to + " count " + entities.Count);
            for (int i = from; i < to; i++)
            {
                //if(i < 0 || i >= entities.Count)
                //    DebugClass.Log("count " + entities.Count + " i " + i);
                JobExcute(entities[i]);
            }
        }
        public abstract void JobExcute(IEntity entity);
    }
}

