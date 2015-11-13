using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RunningJournalApi.UnitTests
{
    internal class ClaimComparer : IEqualityComparer<Claim>
    {
        public bool Equals(Claim x, Claim y)
        {
            return Equals( x.Type, y.Type) &&
                Equals(x.Value, y.Value);
        }

        public int GetHashCode(Claim obj)
        {
            return 0;
        }
    }
}