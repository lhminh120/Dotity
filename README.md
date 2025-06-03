# Dotity

**My name is Minh Mino**

Dotity is a framework inspired by Entitas, an ECS (Entity Component System) framework. The purpose of building this framework was for learning, and I've learned a lot about how ECS works. Now I want to open source it so that anyone interested can contribute. Thank you all.

There are two versions of this framework. Version 2 is more performant than version 1 because it caches data when creating groups. Details will be explained later.

---

## Version 1

### Component

A `Component` is where you define data. You can attach data like `int`, `float`, `string`, or even Unity `Component`. Destroying a component doesn't delete it but puts it into a reused list. When creating a new one, it first checks the list:

```csharp
Dotity.Component.Create<GameObjectComponent>(ComponentKey.GameObject);
```

---

### Entity

An `Entity` only contains an ID and a list of components. Destroying an entity also just moves it into a reused list for reuse. Each time a component is added or removed, it triggers corresponding events which help the `Group` update its membership.

```csharp
Entity.CreateEntity();
```

---

### Matcher and Group

`Group` manages entities with common characteristics — meaning entities with the same set of components. This check is performed using `Matcher`, which allows checking for presence or absence of components, and can be combined.

---

### System

Systems handle all logic related to entities and follow this order:

- `InitializeSystem`
- `ExecuteSystem`
- `RenderSystem`
- `CleanUpSystem`

Example game loop:

```csharp
private void Awake()
{
    InitSystem();
    _systemManager.Initialize();
}

private void Update()
{
    _tick = Time.deltaTime;
    _systemManager.ServiceExecute();
    _systemManager.Execute();
    _systemManager.Render();
    _systemManager.CleanUp();
}
```

---

### Example: InitializeSystem

```csharp
public class CreateViewSystem : IInitializeSystem
{
    private int numberEntities = 10000;

    public void Initialize()
    {
        for (int i = 0; i < numberEntities; i++)
        {
            CreateEntity();
        }
    }

    private void CreateEntity()
    {
        GameObject obj = Singleton<GameData>.Instance.CreateObject();
        Transform trans = obj.transform;

        Entity.CreateEntity()
            .AddComponents(
                Dotity.Component.Create<GameObjectComponent>(ComponentKey.GameObject).Init(obj),
                Dotity.Component.Create<PositionComponent>(ComponentKey.Position).Init(trans.position),
                Dotity.Component.Create<TransformComponent>(ComponentKey.Transform).Init(trans),
                Dotity.Component.Create<SpeedComponent>(ComponentKey.Speed).Init(1));
    }
}
```

---

### ExecuteSystem

```csharp
public class MoveSystem : ExecuteSystem
{
    public MoveSystem() : base(new Matcher().AnyOf(ComponentKey.Position)) { }

    public override void Execute(IEntity entity)
    {
        Move(entity.GetComponent<PositionComponent>(ComponentKey.Position),
             entity.GetComponent<SpeedComponent>(ComponentKey.Speed));
    }

    public override bool ExecuteCondition(IEntity entity) => true;

    private void Move(PositionComponent position, SpeedComponent speed)
    {
        position.position.y += speed.speed * GameSystem._tick;
        if (position.position.y > 5)
            position.position.y = -5;
        position.HasChange();
    }
}
```

---

### RenderSystem

```csharp
public class RenderTransformSystem : RenderSystem
{
    public RenderTransformSystem() : base(new Matcher().AnyOf(ComponentKey.Position, ComponentKey.Transform)) { }

    public override void Render(IEntity entity)
    {
        var position = entity.GetComponent<PositionComponent>(ComponentKey.Position);
        var transform = entity.GetComponent<TransformComponent>(ComponentKey.Transform);
        RenderTransform(position, transform);
    }

    public override bool RenderCondition(IEntity entity)
    {
        return entity.GetComponent<PositionComponent>(ComponentKey.Position).IsChange();
    }

    private void RenderTransform(PositionComponent position, TransformComponent transform)
    {
        transform.transform.position = position.position;
        position.FinishChange();
    }
}
```

---

### System Initialization

```csharp
protected override void InitSystem()
{
    _systemManager
        .Add(new CreateViewSystem())
        .Add(new MoveSystem_V2())
        .Add(new RenderTransformSystem_V2());
}
```

---

## Version 2

The key difference in version 2 is that instead of calling `GetComponent` during each loop, you cache component references at the beginning. This optimizes performance but requires more manual management.

---

### ExecuteSystem_V2

```csharp
public class MoveSystem_V2 : ExecuteSystem_V2
{
    private class MoveSystemData
    {
        public int entityId;
        public PositionComponent position;
        public SpeedComponent speed;
    }

    private List<MoveSystemData> _data = new();

    public MoveSystem_V2() : base(new Matcher().AnyOf(ComponentKey.Position)) { }

    public override void CreateRuntimeDataList()
    {
        var entities = _group.GetEntities();
        for (int i = 0; i < entities.Count; i++)
        {
            _data.Add(new MoveSystemData()
            {
                entityId = entities[i].Id,
                position = entities[i].GetComponent<PositionComponent>(ComponentKey.Position),
                speed = entities[i].GetComponent<SpeedComponent>(ComponentKey.Speed),
            });
        }
    }

    public override void Execute()
    {
        foreach (var d in _data)
            Move(d.position, d.speed);
    }

    public override void OnEntityAdded(IEntity entity)
    {
        _data.Add(new MoveSystemData()
        {
            entityId = entity.Id,
            position = entity.GetComponent<PositionComponent>(ComponentKey.Position),
            speed = entity.GetComponent<SpeedComponent>(ComponentKey.Speed),
        });
    }

    public override void OnEntityRemoved(IEntity entity)
    {
        _data.RemoveAll(d => d.entityId == entity.Id);
    }

    private void Move(PositionComponent position, SpeedComponent speed)
    {
        position.position.y += speed.speed * GameSystem._tick;
        if (position.position.y > 5)
            position.position.y = -5;
        position.HasChange();
    }
}
```

---

### RenderSystem_V2

```csharp
public class RenderTransformSystem_V2 : RenderSystem_V2
{
    private class RenderTransformSystemData
    {
        public int entityId;
        public PositionComponent position;
        public TransformComponent transform;
    }

    private List<RenderTransformSystemData> _data = new();

    public RenderTransformSystem_V2() : base(new Matcher().AnyOf(ComponentKey.Position, ComponentKey.Transform)) { }

    public override void Render()
    {
        foreach (var d in _data)
        {
            if (d.position.IsChange())
                RenderTransform(d.position, d.transform);
        }
    }

    private void RenderTransform(PositionComponent position, TransformComponent transform)
    {
        transform.transform.position = position.position;
        position.FinishChange();
    }

    public override void CreateRuntimeDataList()
    {
        var entities = _group.GetEntities();
        foreach (var e in entities)
        {
            _data.Add(new RenderTransformSystemData()
            {
                entityId = e.Id,
                position = e.GetComponent<PositionComponent>(ComponentKey.Position),
                transform = e.GetComponent<TransformComponent>(ComponentKey.Transform),
            });
        }
    }

    public override void OnEntityAdded(IEntity entity)
    {
        _data.Add(new RenderTransformSystemData()
        {
            entityId = entity.Id,
            position = entity.GetComponent<PositionComponent>(ComponentKey.Position),
            transform = entity.GetComponent<TransformComponent>(ComponentKey.Transform),
        });
    }

    public override void OnEntityRemoved(IEntity entity)
    {
        _data.RemoveAll(d => d.entityId == entity.Id);
    }
}
```

---

## Final Note

Thank you to everyone who took the time to explore this source code. If you have any questions, feel free to reach out. I'm happy to answer everything.
