namespace Proto.Compiler.Utils;

public abstract class Peekable<T> : IPeekable<T> {
#pragma warning disable
  private T _current;
#pragma warning restore

  private bool _shouldUpdate = true;

  protected abstract T ReadElement();

  public T Peek() {
    if (this._shouldUpdate) {
      this._current = this.ReadElement();
      this._shouldUpdate = false;
    }
    return this._current;
  }

  public T Next() {
    var value = this.Peek();
    this._shouldUpdate = true;
    return value;
  }
}
