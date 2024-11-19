using Domain.Constants;

namespace Domain.Exceptions;

public class FestpayException : Exception
{
    public FestpayException(string message) : base(message) { }
}
public class FestpayUnauthorizedException : FestpayException
{
    public FestpayUnauthorizedException() : base(Messages.UNAUTHORIZED) { }
}
public class FestpayNotFoundException : FestpayException
{
    public FestpayNotFoundException() : base(Messages.NOT_FOUND) { }
}
public class FestpayForbiddenException : FestpayException
{
    public FestpayForbiddenException() : base(Messages.FORBIDDEN) { }
}
public class FestpayBadRequestException : FestpayException
{
    public FestpayBadRequestException() : base(Messages.BAD_REQUEST) { }
}