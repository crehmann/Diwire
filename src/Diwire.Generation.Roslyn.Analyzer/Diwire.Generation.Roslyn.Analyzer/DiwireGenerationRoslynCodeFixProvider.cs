using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace Diwire.Generation.Roslyn.Analyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(DiwireGenerationRoslynCodeFixProvider)), Shared]
    public class DiwireGenerationRoslynCodeFixProvider : CodeFixProvider
    {
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DiwireGenerationRoslynAnalyzer.DiagnosticId);

        public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf()
                .OfType<ClassDeclarationSyntax>()
                .First();

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: Title.ToString(),
                    createChangedSolution: c => OverrideRegisterTypeMethodAsync(context.Document, declaration, c),
                    equivalenceKey: nameof(DiwireGenerationRoslynCodeFixProvider)),
                diagnostic);
        }

        private async Task<Solution> OverrideRegisterTypeMethodAsync(Document document, ClassDeclarationSyntax classDeclaration, CancellationToken cancellationToken)
        {
            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var symbol = semanticModel.GetDeclaredSymbol(classDeclaration);
            var module = new ModuleInfo(symbol);
            var method = RegisterTypesMethodBuilder
                .Build(module)
                .WithAdditionalAnnotations(Formatter.Annotation);

            var updatedClassDeclaration = classDeclaration.AddMembers(method);
            var syntaxTree = await document.GetSyntaxTreeAsync(cancellationToken);
            var updatedSyntaxTree = syntaxTree.GetRoot().ReplaceNode(classDeclaration, updatedClassDeclaration);

            return document.WithSyntaxRoot(updatedSyntaxTree).Project.Solution;
        }
    }
}
