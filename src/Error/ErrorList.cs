namespace Error;

public class ErrorList(IEnumerable<LoxinasError> errors) : Exception {
    public IEnumerable<LoxinasError> Errors {
        get => errors;
    }
}
