﻿using System;

namespace Simple.Patterns
{
    public class DisposableAction : IDisposable
    {
        protected Action WhenDisposing { get; set; }
        protected bool OnlyOneDispose { get; set; }
        protected bool AlreadyDisposed { get; set; }


        public DisposableAction(Action whatToDo, bool ensureOnlyOneDispose)
        {
            WhenDisposing = whatToDo;
            OnlyOneDispose = ensureOnlyOneDispose;
        }


        public DisposableAction(Action whatToDo) : this(whatToDo, true)
        {
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (!OnlyOneDispose || !AlreadyDisposed)
            {
                WhenDisposing();
                AlreadyDisposed = true;
            }
        }

        #endregion
    }
}
