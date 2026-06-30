namespace Task5;

public static class RetryExecutor
{
    public static Result<T> Execute<T>(
        Func<T> operation,
        int maxAttempts,
        Func<Exception, bool>? shouldRetry = null)
    {
        if (operation == null)
            throw new ArgumentNullException(nameof(operation));

        if (maxAttempts <= 0)
            throw new ArgumentException("Max attempts must be greater than zero.");

        int attempts = 0;

        while (attempts < maxAttempts)
        {
            attempts++;

            try
            {
                T value = operation();
                return new Result<T>(true, value, null, attempts);
            }
            catch (Exception ex)
            {
                bool canRetry = attempts < maxAttempts && (shouldRetry == null || shouldRetry(ex));

                if (!canRetry)
                {
                    return new Result<T>(false, default, ex, attempts);
                }
            }
        }

        return new Result<T>(false, default, null, attempts);
    }
}