// <copyright file="SymbolicString.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace ZenLib.ModelChecking
{
    using System.Diagnostics.CodeAnalysis;
    using ZenLib.Solver;

    /// <summary>
    /// Representation of a symbolic string value.
    /// </summary>
    internal class SymbolicString<TModel, TVar, TBool, TBitvec, TInt, TString> : SymbolicValue<TModel, TVar, TBool, TBitvec, TInt, TString>
    {
        public SymbolicString(ISolver<TModel, TVar, TBool, TBitvec, TInt, TString> solver, TString value) : base(solver)
        {
            this.Value = value;
        }

        public TString Value { get; }

        internal override SymbolicValue<TModel, TVar, TBool, TBitvec, TInt, TString> Merge(TBool guard, SymbolicValue<TModel, TVar, TBool, TBitvec, TInt, TString> other)
        {
            var o = (SymbolicString<TModel, TVar, TBool, TBitvec, TInt, TString>)other;
            var value = this.Solver.Ite(guard, this.Value, o.Value);
            return new SymbolicString<TModel, TVar, TBool, TBitvec, TInt, TString>(this.Solver, value);
        }

        /// <summary>
        /// Convert the object to a string.
        /// </summary>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return "<symstring>";
        }
    }
}
