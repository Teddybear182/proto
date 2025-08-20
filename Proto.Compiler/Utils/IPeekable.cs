namespace Proto.Compiler.Utils;

public interface IPeekable<out T> {
  public T Peek();
  public T Next();
}
