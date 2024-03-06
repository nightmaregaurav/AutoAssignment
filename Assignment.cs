namespace AutoAssignment
{
    public static class Assignment<T, TT>
    {
        private static Func<T, TT, TT>? _assign;
        private static Func<T, TT>? _create;


        /// <summary>
        /// Define how to create a new instance of TT from T
        /// </summary>
        /// <param name="how">The function to create a new instance of TT from T</param>
        /// <exception cref="Assignment{T,TT}.RelationshipAlreadyDefinedException">If the relationship is already defined</exception>
        /// <typeparam name="T">The source type</typeparam>
        /// <typeparam name="TT">The destination type</typeparam>
        /// <returns></returns>
        public static void DefineCreation(Func<T, TT> how) {
            if (_create != null) throw new RelationshipAlreadyDefinedException(Mode.Create);
            _create = how;
        }

        /// <summary>
        /// Define how to assign values from T to TT
        /// </summary>
        /// <param name="how">The function to assign values from T to TT</param>
        /// <exception cref="Assignment{T,TT}.RelationshipAlreadyDefinedException">If the relationship is already defined</exception>
        /// <typeparam name="T">The source type</typeparam>
        /// <typeparam name="TT">The destination type</typeparam>
        /// <returns></returns>
        public static void DefineAssignment(Func<T, TT, TT> how)
        {
            if (_assign != null) throw new RelationshipAlreadyDefinedException(Mode.Assign);
            _assign = how;
        }

        /// <summary>
        /// Create a new instance of TT from T
        /// </summary>
        /// <param name="source">The source instance</param>
        /// <exception cref="Assignment{T,TT}.UndefinedRelationshipException">If the relationship is not defined</exception>
        /// <typeparam name="T">The source type</typeparam>
        /// <typeparam name="TT">The destination type</typeparam>
        /// <returns>The new instance of TT</returns>
        public static TT From(T source)
        {
            if (_create == null) throw new UndefinedRelationshipException(Mode.Create);
            return _create(source);
        }

        /// <summary>
        /// Assign values from T to TT
        /// </summary>
        /// <param name="source">The source instance</param>
        /// <param name="destination">The destination instance</param>
        /// <exception cref="Assignment{T,TT}.UndefinedRelationshipException">If the relationship is not defined</exception>
        /// <typeparam name="T">The source type</typeparam>
        /// <typeparam name="TT">The destination type</typeparam>
        /// <returns>The destination instance</returns>
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

        private class RelationshipAlreadyDefinedException : Exception
        {
            public RelationshipAlreadyDefinedException(Mode mode) : base($"Relationship to {mode.ToString()} from '{typeof(T)}' to '{typeof(TT)}' is already defined!")
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
