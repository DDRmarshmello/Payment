using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;


namespace BACK.Utils
{
        public static partial class utils
        {
        public static string Serializer<T>(T Obj)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 64, // Opcional: Ajusta la profundidad máxima si es necesario
            };

            if (Obj == null)
                return "";
            string jsonString = JsonSerializer.Serialize(Obj, options);
            return jsonString;
        }
        public static TTarget ConvertTo<TSource, TTarget>(TSource source) where TTarget : new()
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                var target = new TTarget();
                var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var targetProperties = typeof(TTarget).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var sourceProp in sourceProperties)
                {
                    var targetProp = targetProperties.FirstOrDefault(p => p.Name == sourceProp.Name && p.CanWrite);
                    if (targetProp != null)
                    {
                        var sourceValue = sourceProp.GetValue(source);

                        if (sourceValue == null)
                        {
                            continue; // Si el valor de la propiedad en el objeto fuente es nulo, se omite.
                        }

                        // Si la propiedad es del mismo tipo, la copiamos directamente.
                        if (targetProp.PropertyType == sourceProp.PropertyType)
                        {
                            targetProp.SetValue(target, sourceValue);
                        }
                        // Si la propiedad es una colección, tratamos de convertir los elementos dentro de ella.
                        else if (typeof(IEnumerable).IsAssignableFrom(sourceProp.PropertyType) &&
                                 typeof(IEnumerable).IsAssignableFrom(targetProp.PropertyType))
                        {
                            var sourceList = (IEnumerable)sourceValue;
                            var targetItemType = targetProp.PropertyType.GetGenericArguments().First();
                            var targetList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(targetItemType));

                            foreach (var item in sourceList)
                            {
                                // Convertir cada elemento en la colección
                                var convertedItem = ConvertTo(item, Activator.CreateInstance(targetItemType));
                                targetList.Add(convertedItem);
                            }

                            targetProp.SetValue(target, targetList);
                        }
                        // Si la propiedad es un objeto complejo (no una colección ni un tipo simple), hacemos la conversión recursivamente.
                        else if (IsComplexType(sourceProp.PropertyType) && IsComplexType(targetProp.PropertyType))
                        {
                            var convertedNestedObject = ConvertTo(sourceValue, Activator.CreateInstance(targetProp.PropertyType));
                            targetProp.SetValue(target, convertedNestedObject);
                        }
                    }
                }

                return target;
            }

            // Método auxiliar para identificar si un tipo es complejo (no es primitivo ni una clase básica de .NET)
        private static bool IsComplexType(Type type)
            {
                return type.IsClass && type != typeof(string);
            }

            // Versión no genérica para convertir un objeto
        private static object ConvertTo(object source, object target)
            {
                if (source == null || target == null)
                {
                    throw new ArgumentNullException("Source or target cannot be null.");
                }

                var sourceType = source.GetType();
                var targetType = target.GetType();

                var method = typeof(utils)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.Name == "ConvertTo" && m.IsGenericMethodDefinition && m.GetParameters().Length == 1);

                if (method == null)
                {
                    throw new InvalidOperationException("ConvertTo method not found.");
                }

                var genericMethod = method.MakeGenericMethod(sourceType, targetType);
                return genericMethod.Invoke(null, new[] { source });
            }

    }
}


