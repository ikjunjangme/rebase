using System;

namespace PredefineConstant.Enum.User
{
    //[Flags]
    public enum Authority
    {
        //Operator = 0,
        //Guest = 1 << 0,
        //// admin is all
        //Admin = ~(-1 << 1),
        Operator = 0,
        Guest = 1,
        Admin = 2,
    }
}
