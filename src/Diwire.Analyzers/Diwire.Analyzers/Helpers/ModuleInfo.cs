﻿using Diwire.Analyzers.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diwire.Analyzers.Helpers
{
    public class ModuleInfo
    {
        private const string ContainerRegistryParameterName = "containerRegistry";
        private static readonly MethodDeclarationSyntax methodDeclaration = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.ParseTypeName("void"), Constants.RegisterTypeMethod)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(SyntaxFactory.Parameter(
                    attributeLists: SyntaxFactory.List<AttributeListSyntax>(),
                    modifiers: SyntaxFactory.TokenList(),
                    type: SyntaxFactory.ParseTypeName(Constants.ContainerRegistryInterface),
                    identifier: SyntaxFactory.Identifier(ContainerRegistryParameterName),
                    @default: null))
                .AddBodyStatements(SyntaxFactory.EmptyStatement().WithLeadingTrivia(SyntaxFactory.Comment("// This Code was generated by Diwire")));

        public ModuleInfo(INamedTypeSymbol moduleSymbol)
        {
            Module = moduleSymbol ?? throw new ArgumentNullException(nameof(moduleSymbol));
            Registrations = Module.GetAttributes(Constants.RegisterTypeAttribute)
                .Select(x => new RegistrationInfo(x)).ToArray();
        }

        public INamedTypeSymbol Module { get; }

        public IEnumerable<RegistrationInfo> Registrations { get; }

        public MethodDeclarationSyntax Build()
        {
            var registrationStatments = Registrations.Select(x => CreateStatement(x)).ToArray();
            return methodDeclaration.AddBodyStatements(registrationStatments);
        }

        private static StatementSyntax CreateStatement(RegistrationInfo registration)
        {
            var statement = new StringBuilder()
                .Append(ContainerRegistryParameterName)
                .Append(".")
                .Append(registration.Lifetime == Constants.LifetimeSingelton
                    ? Constants.RegisterSingeltonMethod
                    : Constants.RegisterTransientMethod)
                .Append($"<{registration.FromType.GetFullName()}>")
                .Append($"(_ => new {registration.Constructor.ContainingSymbol.GetFullName()}({CreateConstructorParameters(registration.Constructor)}));")
                .ToString();

            return SyntaxFactory.ParseStatement(statement)
                            .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);
        }

        private static string CreateConstructorParameters(IMethodSymbol methodSymbol)
            => methodSymbol.Parameters.Length == 0
                ? string.Empty
                : string.Join(",", methodSymbol.Parameters.Select(parameter => $"_.Resolve<{parameter.Type.GetFullName()}>()"));
    }
}
