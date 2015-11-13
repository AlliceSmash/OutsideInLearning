using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RunningJournalApi
{
    public class SimpleWebToken : IEnumerable<Claim>
    {
        private readonly IEnumerable<Claim> claims;

        public SimpleWebToken(params Claim[] claims)
        {
            this.claims = claims;
        }

        public IEnumerator<Claim> GetEnumerator()
        {
            return claims.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return claims.Select(c => string.Format("{0}={1}", c.Type, c.Value))
                .DefaultIfEmpty(string.Empty)
                .Aggregate((x, y) => string.Format("{0}&{1}", x, y));
        }

        public static bool TryParse(string tokenString, out SimpleWebToken token)
        {
            token = null;
            if ( tokenString == string.Empty)
            {
                token = new SimpleWebToken();
                return true;
            }
            if (tokenString == null)
                return false;

            var claimPairs = tokenString.Split('&').ToArray();
            if(!claimPairs.All(c => c.Contains("=")))
            {
                return false;
            }
            var claims = claimPairs.Select(c => c.Split('='))
                .Select(c => new Claim(c[0], c[1]));
            token = new SimpleWebToken(claims.ToArray());
            return true;
        }
    }
}