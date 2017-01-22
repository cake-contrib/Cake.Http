using System;

namespace Cake.Http.Tests
{
    public static class CakeAssert
    {
        public static void IsArgumentNullException(Exception exception, string parameterName)
        {
            Xunit.Assert.IsType<ArgumentNullException>(exception);   
            Xunit.Assert.Equal(parameterName, ((ArgumentNullException)exception).ParamName);
        }

        public static void IsArgumentException(Exception exception, string parameterName, string message)
        {
            Xunit.Assert.IsType<ArgumentException>(exception);
            Xunit.Assert.Equal(parameterName, ((ArgumentException)exception).ParamName);
            Xunit.Assert.Equal(new ArgumentException(message, parameterName).Message, exception.Message);
        }

        public static void IsExceptionWithMessage<T>(Exception exception, string message)
            where T : Exception
        {
            Xunit.Assert.IsType<T>(exception);
            Xunit.Assert.Equal(message, exception.Message);
        }
    }
}
