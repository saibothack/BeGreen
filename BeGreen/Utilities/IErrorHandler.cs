using System;

namespace BeGreen.Utilities
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
