# ActionSchedulerService
This code was created with the help of AI. This code is used when a method should be executed, but it does not matter when or, for example, when an exception occurs, but it is known that it can occur at this moment, but will not occur a little later (for example, when reading a modified file).

Example:
```C#
private void InitData(string path)
{
    try
    {
        var data = File.ReadAllText(path);
        _entities = JsonConvert.DeserializeObject<Entity>(data) ?? [];
    }
    catch { _actionSchedulerService.RegisterAction(InitData, path); }
}
```
