using Domain.Constants;

namespace Domain.Exceptions;

public class BudgetException : Exception
{
    public BudgetException(string message) : base(message) { }
}
public class BudgetUnauthorizedException : BudgetException
{
    public BudgetUnauthorizedException() : base(Messages.UNAUTHORIZED) { }
}
public class BudgetNotFoundException : BudgetException
{
    public BudgetNotFoundException() : base(Messages.NOT_FOUND) { }
}
public class BudgetForbiddenException : BudgetException
{
    public BudgetForbiddenException() : base(Messages.FORBIDDEN) { }
}
public class BudgetBadRequestException : BudgetException
{
    public BudgetBadRequestException() : base(Messages.BAD_REQUEST) { }
}