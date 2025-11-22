using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FashionFace.Common.Extensions.Implementations;

public static class ObjectExtensions
{
    public static T DeepCopy<T>(
        this T obj
    )
    {
        var jsonSerializerOptions =
            new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };

        var serialized =
            JsonSerializer
                .Serialize(
                    obj,
                    jsonSerializerOptions
                );

        var deepCopy =
            JsonSerializer
                .Deserialize<T>(
                    serialized
                )!;

        return
            deepCopy;
    }

    public static void ResetIds<T>(
        this T obj,
        Func<Guid> getIdFunc
    )
    {
        if (obj == null)
        {
            return;
        }

        var type =
            obj.GetType();

        var properties =
            type
                .GetProperties(
                    BindingFlags.Public | BindingFlags.Instance
                );

        foreach (var property in properties)
        {
            var isWritableId =
                property is
                {
                    Name: "Id",
                    CanWrite: true,
                };

            if (isWritableId)
            {
                var defaultValue =
                    property
                        .PropertyType
                        .GetDefaultValue();

                var isGuid =
                    property.PropertyType == typeof(Guid);

                var guid =
                    getIdFunc.Invoke();

                var newValue =
                    isGuid
                        ? guid
                        : defaultValue;

                property
                    .SetValue(
                        obj,
                        newValue
                    );
            }
            else
            {
                var isString =
                    property.PropertyType == typeof(string);

                if (isString)
                {
                    continue;
                }

                var propertyValue =
                    property
                        .GetValue(
                            obj
                        );

                var isNull =
                    propertyValue == null;

                if (isNull)
                {
                    continue;
                }

                if (property.PropertyType.IsGenericEnumerable())
                {
                    foreach (var item in (IEnumerable)propertyValue)
                    {
                        ResetIds(
                            item,
                            getIdFunc
                        );
                    }
                }
                else if (property.PropertyType.IsGenericCollection())
                {
                    foreach (var item in (ICollection)propertyValue)
                    {
                        ResetIds(
                            item,
                            getIdFunc
                        );
                    }
                }

                var isClass =
                    property
                        .PropertyType
                        .IsClass;

                if (isClass)
                {
                    var nestedObj =
                        property
                            .GetValue(
                                obj
                            );

                    var isNotNull =
                        nestedObj != null;

                    if (isNotNull)
                    {
                        var nestedObjType =
                            nestedObj!.GetType();

                        var isNotBaseType =
                            nestedObjType != type;

                        if (isNotBaseType)
                        {
                            ResetIds(
                                nestedObj,
                                getIdFunc
                            );
                        }
                    }
                }
            }
        }
    }

    private static bool IsGenericEnumerable(
        this Type type
    )
    {
        var isGenericEnumerable =
            type
                .GetInterfaces()
                .Any(
                    interfaceType =>
                        interfaceType.IsGenericType
                        && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                );

        return
            isGenericEnumerable;
    }

    private static bool IsGenericCollection(
        this Type type
    )
    {
        var isGenericCollection =
            type
                .GetInterfaces()
                .Any(
                    interfaceType =>
                        interfaceType.IsGenericType
                        && interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>)
                );

        return
            isGenericCollection;
    }

    private static object? GetDefaultValue(
        this Type type
    ) =>
        type.IsValueType
            ? Activator.CreateInstance(
                type
            )
            : null;
}