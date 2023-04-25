namespace SoftTouch.Spirv.Internals;



public class InstructionStorage
{
    public static InstructionStorage Instance {get;} = new();

    Dictionary<Type,InstructionList> Storage = new();

    InstructionStorage(){}

    public void Add<T>(in T instruction) 
        where T : IInstruction
    {
        if(Storage.TryGetValue(typeof(T),out InstructionList? list) && list is InstructionList<T> l)
        {
            l.Add(instruction);
        }
        else
        {
            Storage.Add(typeof(T),new InstructionList<T>(){instruction});
        }
    }
    public void Remove<T>(in T instruction) 
        where T : IInstruction
    {
        if(Storage.TryGetValue(typeof(T),out InstructionList? list) && list is InstructionList<T> l)
        {
            l.Remove(instruction);
        }
    }

}