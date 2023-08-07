using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtility.API
{
    public enum APIErrorCode
    {
        Ok = 0,
        Processing = 1,

        NotFoundAPIPath = 10,
        BadRequestJosnFormat = 11,
        NotSupportedFunction = 12,

        DisabledLicense = 20,
        ExpiredLicense = 21,
        NotFoundVersion = 22,
        FailedLoadRPC = 23,
        FailedLoadHTTP = 24,

        FailedCreateNode = 100,
        FailedStreamingChannel = 200,
        FailedCreateChannel = 201,
        FailedCalibration = 210,
        FailedSnapshot = 220,
        FailedAddRoi = 300,
        FailedLink = 310,
    }
}
