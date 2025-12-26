namespace Error;

public abstract class LoxinasError(string message) : Exception(message) {
    public abstract string Type { get; }
}
