using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public record Email(string Value)
    {
        public static implicit operator string(Email email) => email.Value;

        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                throw new ArgumentException("Invalid email address.");
            return new Email(value);
        }
    }
}
