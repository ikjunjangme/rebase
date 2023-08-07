using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtility.HTTP
{
    public enum HTTPStatusCode
    {
        // 참고 : https://developer.mozilla.org/ko/docs/Web/HTTP/Status
        Continue = 101,
        Processing = 102,
        Ok = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,
        AlreadyReported = 208,
        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        RequestTimeout = 408,
        Conflict = 409,
        PayloadTooLarge = 413,
        RequestUriTooLong = 414,
        FailedDependency = 424,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
        HttpVersionNotSupported = 505,
    }
}
