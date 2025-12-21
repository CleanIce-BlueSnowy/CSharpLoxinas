namespace Error;

public class ErrorList(IEnumerable<ILoxinasError> errors) : Exception {
    public IEnumerable<ILoxinasError> Errors {
        get => errors;
    }
}
