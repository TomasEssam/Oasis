using System.Runtime.Serialization;

namespace Todo.Domain.Helpers.Exceptions
{
    public class NoClaimException : Exception
    {
        public NoClaimException(string message) : base(message)
        {
        }

        protected NoClaimException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
