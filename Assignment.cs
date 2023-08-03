namespace AutoAssignment
{
    public static class Assignment<T, TT>
    {
        private static Func<T, TT, TT>? _assign;
        private static Func<T, TT>? _create;

        public static void DefineCreation(Func<T, TT> how) => _create = how;
        public static void DefineAssignment(Func<T, TT, TT> how) => _assign = how;

        public static TT From(T source)
        {
            if (_create == null) throw new UndefinedRelationshipException(Mode.Create);
            return _create(source);
        }

        public static TT From(T source, TT destination)
        {
            if (_assign == null) throw new UndefinedRelationshipException(Mode.Assign);
            return _assign(source, destination);
        }

        private class UndefinedRelationshipException : Exception
        {
            public UndefinedRelationshipException(Mode mode)
                : base(
                    $"Relationship to {mode.ToString()} from '{typeof(T)}' to '{typeof(TT)}' is not defined!\n" +
                    $"Define using {nameof(Assignment<T, TT>)}." +
                    $"{(mode == Mode.Create ? nameof(DefineCreation) + "(source => destination)" : nameof(DefineAssignment) + "((source, destination) => destination)")}" +
                    $" first!"
                )
            {
            }
        }

        private enum Mode
        {
            Create = 0,
            Assign = 1
        }
    }
}
