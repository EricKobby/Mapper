using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mapper
{
    public class Mapper : IMapper
    {
        public T2 MapValues<T1, T2>(T1 objectWithNewValues, T2 objectToUpdate)
        {
            //check if a T is a value type or reference type
            if (objectToUpdate == null)
                throw new NullReferenceException("Object to update null");
            if (objectWithNewValues == null)
                throw new NullReferenceException("Object with new values is null");
            //Mapping
            var properties = objectToUpdate.GetType().GetProperties().ToList();
            var newValueProperties = objectWithNewValues.GetType().GetProperties().ToList();
            foreach (var propertyInfo in newValueProperties)
            {
                //getting corresponding propertyInfo from objectToUpdate's PropertyInfo
                var foundInfo = properties.FirstOrDefault(prop => prop.Name == propertyInfo.Name);
                if (foundInfo is null)
                    continue;
                if (propertyInfo.GetValue(objectWithNewValues) != null)
                {
                    if (foundInfo.PropertyType == typeof(string))
                    {
                        var stringValue = propertyInfo.GetValue(objectWithNewValues).ToString();
                        RegexOptions options = RegexOptions.None;
                        Regex regex = new Regex("[ ]{2,}", options);
                        stringValue = regex.Replace(stringValue, " ");
                        foundInfo.SetValue(objectToUpdate, stringValue);
                    }
                    else
                    {
                        foundInfo.SetValue(objectToUpdate, propertyInfo.GetValue(objectWithNewValues));
                    }
                }
            }
            return objectToUpdate;
        }
    }
}
