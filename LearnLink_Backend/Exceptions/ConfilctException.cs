namespace LearnLink_Backend.Exceptions
{
    public class ConfilctException : Exception
    {
        public ConfilctException(string message) : base(message) { }
        public ConfilctException(string message, Exception inner) : base(message, inner) { }
    }
}
