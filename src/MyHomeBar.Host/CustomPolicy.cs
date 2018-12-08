using Serilog.Core;
using Serilog.Events;
using System.Collections.Generic;

namespace MyHomeBar.Host
{
    public class LoginData
    {
        public string Username;
        public string Password;
    }

    public class CustomPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            result = null;

            if (value is LoginData)
            {
                result = new StructureValue(
                    new List<LogEventProperty>
                    {
                        new LogEventProperty("Username", new ScalarValue(((LoginData)value).Username))
                    });
            }

            return (result != null);
        }
    }
}
