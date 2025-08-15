namespace Proto.Compiler.Utils;

internal interface IPeekable<out T> {
  public T Peek();
  public T Next();
}
