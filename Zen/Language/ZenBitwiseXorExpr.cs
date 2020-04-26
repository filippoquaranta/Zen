﻿// <copyright file="ZenBitwiseXorExpr.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Zen
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Class representing an And expression.
    /// </summary>
    internal sealed class ZenBitwiseXorExpr<T> : Zen<T>
    {
        private static Dictionary<(object, object), ZenBitwiseXorExpr<T>> hashConsTable =
            new Dictionary<(object, object), ZenBitwiseXorExpr<T>>();

        public static ZenBitwiseXorExpr<T> Create(Zen<T> expr1, Zen<T> expr2)
        {
            CommonUtilities.Validate(expr1);
            CommonUtilities.Validate(expr2);
            CommonUtilities.ValidateIsIntegerType(typeof(T));

            var key = (expr1, expr2);
            if (hashConsTable.TryGetValue(key, out var value))
            {
                return value;
            }

            var ret = new ZenBitwiseXorExpr<T>(expr1, expr2);
            hashConsTable[key] = ret;
            return ret;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZenBitwiseXorExpr{T}"/> class.
        /// </summary>
        /// <param name="expr1">First Zen expression.</param>
        /// <param name="expr2">First Zen expression.</param>
        private ZenBitwiseXorExpr(Zen<T> expr1, Zen<T> expr2)
        {
            this.Expr1 = expr1;
            this.Expr2 = expr2;
        }

        /// <summary>
        /// Gets the first expression.
        /// </summary>
        internal Zen<T> Expr1 { get; }

        /// <summary>
        /// Gets the second expression.
        /// </summary>
        internal Zen<T> Expr2 { get; }

        /// <summary>
        /// Convert the expression to a string.
        /// </summary>
        /// <returns>The string representation.</returns>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"({this.Expr1.ToString()} ^ {this.Expr2.ToString()})";
        }

        /// <summary>
        /// Implementing the visitor interface.
        /// </summary>
        /// <param name="visitor">The visitor object.</param>
        /// <param name="parameter">The visitor parameter.</param>
        /// <typeparam name="TParam">The visitor parameter type.</typeparam>
        /// <typeparam name="TReturn">The visitor return type.</typeparam>
        /// <returns>A return value.</returns>
        internal override TReturn Accept<TParam, TReturn>(IZenExprVisitor<TParam, TReturn> visitor, TParam parameter)
        {
            return visitor.VisitZenBitwiseXorExpr(this, parameter);
        }

        /// <summary>
        /// Implementing the transformer interface.
        /// </summary>
        /// <param name="visitor">The visitor object.</param>
        /// <returns>A return value.</returns>
        internal override Zen<T> Accept(IZenExprTransformer visitor)
        {
            return visitor.VisitZenBitwiseXorExpr(this);
        }
    }
}