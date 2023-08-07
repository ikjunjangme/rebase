using System;

namespace PublicUtility.Generators
{
    public class HashUID
    {
        static public string GetSimpleHashUid()
        {
            while (true)
            {
                string hash_guid = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                if (hash_guid.Length == 8)
                    return hash_guid;
            }
            
        }
    }
}
