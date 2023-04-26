namespace SoftTouch.Spirv.Experiments;


public interface IMixinA
{
    void SayHi()
    {
        Console.WriteLine("Hello i'm Mixin A");
    }
}
public interface IMixinB
{
    void SayHi()
    {
        Console.WriteLine("Hello i'm Mixin A");
    }
}

public class MyMixed : IMixinA
{
    
}