﻿using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Module.Shared.Persistence;

public static class DbContextFindByIdsExtensions
{
    private static readonly MethodInfo? ContainsMethod = typeof(Enumerable).GetMethods()
        .FirstOrDefault(m => m.Name == "Contains" && m.GetParameters().Length == 2)
        ?.MakeGenericMethod(typeof(object));

    public static Task<T[]> FindByIdsAsync<T>(this DbSet<T> dbSet, params object[] keyValues)
        where T : class
    {
        var entityType = dbSet.EntityType;
        var primaryKey = entityType.FindPrimaryKey();
        if (primaryKey != null && primaryKey.Properties.Count != 1)
            throw new NotSupportedException("Only a single primary key is supported");

        var pkProperty = primaryKey?.Properties[0];
        var pkPropertyType = pkProperty?.ClrType;

        // validate passed key values
        foreach (var keyValue in keyValues)
        {
            if (pkPropertyType != null && !pkPropertyType.IsInstanceOfType(keyValue))
                throw new ArgumentException($"Key value '{keyValue}' is not of the right type");
        }

        // retrieve member info for primary key
        if (pkProperty?.Name == null) return null!;

        var pkMemberInfo = typeof(T).GetProperty(pkProperty.Name);
        if (pkMemberInfo == null)
            throw new ArgumentException("Type does not contain the primary key as an accessible property");

        // build lambda expression
        var parameter = Expression.Parameter(typeof(T), "e");
        var body = Expression.Call(null, ContainsMethod!,
            Expression.Constant(keyValues),
            Expression.Convert(Expression.MakeMemberAccess(parameter, pkMemberInfo), typeof(object)));
        var predicateExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

        // run query
        return dbSet.Where(predicateExpression).ToArrayAsync();

    }
}