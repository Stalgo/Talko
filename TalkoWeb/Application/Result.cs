namespace TalkoWeb.Application
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        private Result(bool isSuccess, IEnumerable<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors ?? Enumerable.Empty<string>();
        }

        public static Result Success() => new Result(true, Enumerable.Empty<string>());

        public static Result Failure(IEnumerable<string> errors) => new Result(false, errors);
    }
}
