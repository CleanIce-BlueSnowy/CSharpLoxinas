namespace Error;

public interface ILoxinasError {
    public string Message { get; }
    public string Type {
        get => "Loxinas Error";
    }
}
