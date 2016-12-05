using System;

namespace TicTacToe.Bll.Infrastructure
{
    public class CustomValidationException : Exception
    {
        public string Property { get; protected set; }

        public CustomValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
