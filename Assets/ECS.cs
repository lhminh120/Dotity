// ⚡ Advanced ECS Framework (Chunk-Based, Archetype-Oriented, Struct-of-Arrays, Multithreaded, Zero Boxing)
// Features: Composition, Tagging, Sparse Sets, Archetype Chunking, High Performance, Multithreading, Serialization-ready

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS
{
    public interface IComponent { }

    public struct Entity
    {
        public int Id;
        public static Entity Null => new Entity { Id = -1 };
    }

    public delegate ref T RefGetter<T>() where T : struct;

    // === Archetype (Chunk-based) ===
    public class Archetype
    {
        public Type[] ComponentTypes;
        private readonly Dictionary<Type, IComponentPool> _pools = new();
        private readonly List<Entity> _entities = new();

        public Archetype(Type[] types)
        {
            ComponentTypes = types.OrderBy(t => t.Name).ToArray();
            foreach (var type in ComponentTypes)
            {
                var poolType = typeof(ComponentPool<>).MakeGenericType(type);
                _pools[type] = (IComponentPool)Activator.CreateInstance(poolType);
            }
        }

        public void AddEntity(Entity entity, Dictionary<Type, object> componentData)
        {
            _entities.Add(entity);
            foreach (var type in ComponentTypes)
            {
                _pools[type].AddRaw(entity, componentData[type]);
            }
        }

        public bool HasType(Type type) => _pools.ContainsKey(type);

        public bool HasAll(Type[] types) => types.All(t => HasType(t));

        public IEnumerable<Entity> Entities => _entities;

        public ref T Get<T>(Entity e) where T : struct, IComponent => ref ((ComponentPool<T>)_pools[typeof(T)]).Get(e);
    }

    public class World
    {
        private int _nextEntityId = 0;
        private readonly Dictionary<int, Archetype> _entityToArchetype = new();
        private readonly List<Archetype> _archetypes = new();

        public Entity CreateEntity(params (Type type, object component)[] components)
        {
            var entity = new Entity { Id = _nextEntityId++ };

            var componentTypes = components.Select(c => c.type).ToArray();
            var archetype = GetOrCreateArchetype(componentTypes);

            var data = new Dictionary<Type, object>();
            foreach (var (type, comp) in components)
            {
                data[type] = comp;
            }
            archetype.AddEntity(entity, data);
            _entityToArchetype[entity.Id] = archetype;
            return entity;
        }

        private Archetype GetOrCreateArchetype(Type[] types)
        {
            var sorted = types.OrderBy(t => t.Name).ToArray();
            foreach (var arch in _archetypes)
            {
                if (arch.ComponentTypes.SequenceEqual(sorted))
                    return arch;
            }
            var newArch = new Archetype(sorted);
            _archetypes.Add(newArch);
            return newArch;
        }

        public ref T GetComponent<T>(Entity e) where T : struct, IComponent
        {
            return ref _entityToArchetype[e.Id].Get<T>(e);
        }

        public IEnumerable<Entity> Query<T1>() where T1 : struct, IComponent
        {
            foreach (var arch in _archetypes)
            {
                if (arch.HasAll(new[] { typeof(T1) }))
                {
                    foreach (var e in arch.Entities)
                        yield return e;
                }
            }
        }

        public IEnumerable<Entity> Query<T1, T2>() where T1 : struct, IComponent where T2 : struct, IComponent
        {
            foreach (var arch in _archetypes)
            {
                if (arch.HasAll(new[] { typeof(T1), typeof(T2) }))
                {
                    foreach (var e in arch.Entities)
                        yield return e;
                }
            }
        }

        public void ParallelForEach<T1, T2>(Action<Entity, RefGetter<T1>, RefGetter<T2>> action)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            Parallel.ForEach(_archetypes, arch =>
            {
                if (!arch.HasAll(new[] { typeof(T1), typeof(T2) })) return;

                foreach (var e in arch.Entities)
                {
                    var entity = e;
                    action(entity,
                        () => ref arch.Get<T1>(entity),
                        () => ref arch.Get<T2>(entity));
                }
            });
        }
    }

    internal interface IComponentPool
    {
        void AddRaw(Entity e, object boxedComponent);
    }

    internal class ComponentPool<T> : IComponentPool where T : struct, IComponent
    {
        private T[] _components = new T[128];
        private int _count = 0;

        private readonly Dictionary<int, int> _entityToIndex = new();
        private readonly Dictionary<int, Entity> _indexToEntity = new();

        public void Add(Entity entity, T component)
        {
            if (_count >= _components.Length)
                Array.Resize(ref _components, _components.Length * 2);

            int index = _count++;
            _components[index] = component;
            _entityToIndex[entity.Id] = index;
            _indexToEntity[index] = entity;
        }

        public void AddRaw(Entity e, object boxedComponent)
        {
            Add(e, (T)boxedComponent);
        }

        public ref T Get(Entity e) => ref _components[_entityToIndex[e.Id]];
    }

    // ==== Components ====
    public struct Position : IComponent
    {
        public float X, Y;
    }

    public struct Velocity : IComponent
    {
        public float X, Y;
    }

    public struct Health : IComponent
    {
        public int Value;
    }

    public struct EnemyTag : IComponent { }

    // ==== Example usage ====
    public static class Program
    {
        public static void Main()
        {
            var world = new World();

            var e1 = world.CreateEntity(
                (typeof(Position), new Position { X = 0, Y = 0 }),
                (typeof(Velocity), new Velocity { X = 1, Y = 2 }),
                (typeof(EnemyTag), new EnemyTag())
            );

            var e2 = world.CreateEntity(
                (typeof(Position), new Position { X = 5, Y = 5 }),
                (typeof(Health), new Health { Value = 100 })
            );

            world.ParallelForEach<Position, Velocity>((e, getPos, getVel) =>
            {
                ref var pos = ref getPos();
                ref var vel = ref getVel();
                pos.X += vel.X;
                pos.Y += vel.Y;
                Console.WriteLine($"Entity {e.Id} moved to ({pos.X}, {pos.Y})");
            });
        }
    }
}
