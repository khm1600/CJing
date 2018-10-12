// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.
// First modified : 2018.09

using System.Collections.Generic;

namespace Microsoft.CodeAnalysis.CSharp
{
    public static partial class SyntaxFacts
    {
        public static bool IsKeywordKind(SyntaxKind kind)
        {
            return IsReservedKeyword(kind) || IsContextualKeyword(kind);
        }

        public static IEnumerable<SyntaxKind> GetReservedKeywordKinds()
        {
            for (int i = (int)SyntaxKind.BoolKeyword; i <= (int)SyntaxKind.ImplicitKeyword; i++)
            {
                yield return (SyntaxKind)i;
            }
        }

        public static IEnumerable<SyntaxKind> GetKeywordKinds()
        {
            foreach (var reserved in GetReservedKeywordKinds())
            {
                yield return reserved;
            }

            foreach (var contextual in GetContextualKeywordKinds())
            {
                yield return contextual;
            }
        }

        public static bool IsReservedKeyword(SyntaxKind kind)
        {
            return kind >= SyntaxKind.BoolKeyword && kind <= SyntaxKind.ImplicitKeyword;
        }

        public static bool IsAttributeTargetSpecifier(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.AssemblyKeyword:
                case SyntaxKind.ModuleKeyword:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsAccessibilityModifier(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PrivateKeyword:
                case SyntaxKind.ProtectedKeyword:
                case SyntaxKind.InternalKeyword:
                case SyntaxKind.PublicKeyword:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsPreprocessorKeyword(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.TrueKeyword:
                case SyntaxKind.FalseKeyword:
                case SyntaxKind.DefaultKeyword:
                case SyntaxKind.IfKeyword:
                case SyntaxKind.ElseKeyword:
                case SyntaxKind.ElifKeyword:
                case SyntaxKind.EndIfKeyword:
                case SyntaxKind.RegionKeyword:
                case SyntaxKind.EndRegionKeyword:
                case SyntaxKind.DefineKeyword:
                case SyntaxKind.UndefKeyword:
                case SyntaxKind.WarningKeyword:
                case SyntaxKind.ErrorKeyword:
                case SyntaxKind.LineKeyword:
                case SyntaxKind.PragmaKeyword:
                case SyntaxKind.HiddenKeyword:
                case SyntaxKind.ChecksumKeyword:
                case SyntaxKind.DisableKeyword:
                case SyntaxKind.RestoreKeyword:
                case SyntaxKind.ReferenceKeyword:
                case SyntaxKind.LoadKeyword:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Some preprocessor keywords are only keywords when they appear after a
        /// hash sign (#).  For these keywords, the lexer will produce tokens with
        /// Kind = SyntaxKind.IdentifierToken and ContextualKind set to the keyword
        /// SyntaxKind.
        /// </summary>
        /// <remarks>
        /// This wrinkle is specifically not publicly exposed.
        /// </remarks>
        internal static bool IsPreprocessorContextualKeyword(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.TrueKeyword:
                case SyntaxKind.FalseKeyword:
                case SyntaxKind.DefaultKeyword:
                case SyntaxKind.HiddenKeyword:
                case SyntaxKind.ChecksumKeyword:
                case SyntaxKind.DisableKeyword:
                case SyntaxKind.RestoreKeyword:
                    return false;
                default:
                    return IsPreprocessorKeyword(kind);
            }
        }

        public static IEnumerable<SyntaxKind> GetPreprocessorKeywordKinds()
        {
            yield return SyntaxKind.TrueKeyword;
            yield return SyntaxKind.FalseKeyword;
            yield return SyntaxKind.DefaultKeyword;
            yield return SyntaxKind.HiddenKeyword;
            for (int i = (int)SyntaxKind.ElifKeyword; i <= (int)SyntaxKind.RestoreKeyword; i++)
            {
                yield return (SyntaxKind)i;
            }
        }

        public static bool IsPunctuation(SyntaxKind kind)
        {
            return kind >= SyntaxKind.TildeToken && kind <= SyntaxKind.PercentEqualsToken;
        }

        public static bool IsLanguagePunctuation(SyntaxKind kind)
        {
            return IsPunctuation(kind) && !IsPreprocessorKeyword(kind) && !IsDebuggerSpecialPunctuation(kind);
        }

        public static bool IsPreprocessorPunctuation(SyntaxKind kind)
        {
            return kind == SyntaxKind.HashToken;
        }

        private static bool IsDebuggerSpecialPunctuation(SyntaxKind kind)
        {
            // TODO: What about "<>f_AnonymousMethod"? Or "123#"? What's this used for?
            return kind == SyntaxKind.DollarToken;
        }

        public static IEnumerable<SyntaxKind> GetPunctuationKinds()
        {
            for (int i = (int)SyntaxKind.TildeToken; i <= (int)SyntaxKind.PercentEqualsToken; i++)
            {
                yield return (SyntaxKind)i;
            }
        }

        public static bool IsPunctuationOrKeyword(SyntaxKind kind)
        {
            return kind >= SyntaxKind.TildeToken && kind <= SyntaxKind.EndOfFileToken;
        }

        internal static bool IsLiteral(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.IdentifierToken:
                case SyntaxKind.StringLiteralToken:
                case SyntaxKind.CharacterLiteralToken:
                case SyntaxKind.NumericLiteralToken:
                case SyntaxKind.XmlTextLiteralToken:
                case SyntaxKind.XmlTextLiteralNewLineToken:
                case SyntaxKind.XmlEntityLiteralToken:
                    //case SyntaxKind.Unknown:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsAnyToken(SyntaxKind kind)
        {
            if (kind >= SyntaxKind.TildeToken && kind < SyntaxKind.EndOfLineTrivia) return true;
            switch (kind)
            {
                case SyntaxKind.InterpolatedStringToken:
                case SyntaxKind.InterpolatedStringStartToken:
                case SyntaxKind.InterpolatedVerbatimStringStartToken:
                case SyntaxKind.InterpolatedStringTextToken:
                case SyntaxKind.InterpolatedStringEndToken:
                case SyntaxKind.LoadKeyword:
                case SyntaxKind.UnderscoreToken:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsTrivia(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.EndOfLineTrivia:
                case SyntaxKind.WhitespaceTrivia:
                case SyntaxKind.SingleLineCommentTrivia:
                case SyntaxKind.MultiLineCommentTrivia:
                case SyntaxKind.SingleLineDocumentationCommentTrivia:
                case SyntaxKind.MultiLineDocumentationCommentTrivia:
                case SyntaxKind.DisabledTextTrivia:
                case SyntaxKind.DocumentationCommentExteriorTrivia:
                case SyntaxKind.ConflictMarkerTrivia:
                    return true;
                default:
                    return IsPreprocessorDirective(kind);
            }
        }

        public static bool IsPreprocessorDirective(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.IfDirectiveTrivia:
                case SyntaxKind.ElifDirectiveTrivia:
                case SyntaxKind.ElseDirectiveTrivia:
                case SyntaxKind.EndIfDirectiveTrivia:
                case SyntaxKind.RegionDirectiveTrivia:
                case SyntaxKind.EndRegionDirectiveTrivia:
                case SyntaxKind.DefineDirectiveTrivia:
                case SyntaxKind.UndefDirectiveTrivia:
                case SyntaxKind.ErrorDirectiveTrivia:
                case SyntaxKind.WarningDirectiveTrivia:
                case SyntaxKind.LineDirectiveTrivia:
                case SyntaxKind.PragmaWarningDirectiveTrivia:
                case SyntaxKind.PragmaChecksumDirectiveTrivia:
                case SyntaxKind.ReferenceDirectiveTrivia:
                case SyntaxKind.LoadDirectiveTrivia:
                case SyntaxKind.BadDirectiveTrivia:
                case SyntaxKind.ShebangDirectiveTrivia:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsName(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.IdentifierName:
                case SyntaxKind.GenericName:
                case SyntaxKind.QualifiedName:
                case SyntaxKind.AliasQualifiedName:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsPredefinedType(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.BoolKeyword:
                case SyntaxKind.ByteKeyword:
                case SyntaxKind.SByteKeyword:
                case SyntaxKind.IntKeyword:
                case SyntaxKind.UIntKeyword:
                case SyntaxKind.ShortKeyword:
                case SyntaxKind.UShortKeyword:
                case SyntaxKind.LongKeyword:
                case SyntaxKind.ULongKeyword:
                case SyntaxKind.FloatKeyword:
                case SyntaxKind.DoubleKeyword:
                case SyntaxKind.DecimalKeyword:
                case SyntaxKind.StringKeyword:
                case SyntaxKind.CharKeyword:
                case SyntaxKind.ObjectKeyword:
                case SyntaxKind.VoidKeyword:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsTypeSyntax(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.ArrayType:
                case SyntaxKind.PointerType:
                case SyntaxKind.NullableType:
                case SyntaxKind.PredefinedType:
                case SyntaxKind.TupleType:
                    return true;
                default:
                    return IsName(kind);
            }
        }

        public static bool IsTypeDeclaration(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.DelegateDeclaration:
                case SyntaxKind.EnumDeclaration:
                case SyntaxKind.ClassDeclaration:
                case SyntaxKind.StructDeclaration:
                case SyntaxKind.InterfaceDeclaration:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Member declarations that can appear in global code (other than type declarations).
        /// </summary>
        public static bool IsGlobalMemberDeclaration(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.GlobalStatement:
                case SyntaxKind.FieldDeclaration:
                case SyntaxKind.MethodDeclaration:
                case SyntaxKind.PropertyDeclaration:
                case SyntaxKind.EventDeclaration:
                case SyntaxKind.EventFieldDeclaration:
                    return true;
            }
            return false;
        }

        public static bool IsNamespaceMemberDeclaration(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.ClassDeclaration:
                case SyntaxKind.StructDeclaration:
                case SyntaxKind.InterfaceDeclaration:
                case SyntaxKind.DelegateDeclaration:
                case SyntaxKind.EnumDeclaration:
                case SyntaxKind.NamespaceDeclaration:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsAnyUnaryExpression(SyntaxKind token)
        {
            return IsPrefixUnaryExpression(token) || IsPostfixUnaryExpression(token);
        }

        public static bool IsPrefixUnaryExpression(SyntaxKind token)
        {
            return GetPrefixUnaryExpression(token) != SyntaxKind.None;
        }

        public static bool IsPrefixUnaryExpressionOperatorToken(SyntaxKind token)
        {
            return GetPrefixUnaryExpression(token) != SyntaxKind.None;
        }

        public static SyntaxKind GetPrefixUnaryExpression(SyntaxKind token)
        {
            switch (token)
            {
                case SyntaxKind.PlusToken:
                    return SyntaxKind.UnaryPlusExpression;
                case SyntaxKind.MinusToken:
                    return SyntaxKind.UnaryMinusExpression;
                case SyntaxKind.TildeToken:
                    return SyntaxKind.BitwiseNotExpression;
                case SyntaxKind.ExclamationToken:
                    return SyntaxKind.LogicalNotExpression;
                case SyntaxKind.PlusPlusToken:
                    return SyntaxKind.PreIncrementExpression;
                case SyntaxKind.MinusMinusToken:
                    return SyntaxKind.PreDecrementExpression;
                case SyntaxKind.AmpersandToken:
                    return SyntaxKind.AddressOfExpression;
                case SyntaxKind.AsteriskToken:
                    return SyntaxKind.PointerIndirectionExpression;
                default:
                    return SyntaxKind.None;
            }
        }

        public static bool IsPostfixUnaryExpression(SyntaxKind token)
        {
            return GetPostfixUnaryExpression(token) != SyntaxKind.None;
        }

        public static bool IsPostfixUnaryExpressionToken(SyntaxKind token)
        {
            return GetPostfixUnaryExpression(token) != SyntaxKind.None;
        }

        public static SyntaxKind GetPostfixUnaryExpression(SyntaxKind token)
        {
            switch (token)
            {
                case SyntaxKind.PlusPlusToken:
                    return SyntaxKind.PostIncrementExpression;
                case SyntaxKind.MinusMinusToken:
                    return SyntaxKind.PostDecrementExpression;
                default:
                    return SyntaxKind.None;
            }
        }

        internal static bool IsIncrementOrDecrementOperator(SyntaxKind token)
        {
            switch (token)
            {
                case SyntaxKind.PlusPlusToken:
                case SyntaxKind.MinusMinusToken:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsUnaryOperatorDeclarationToken(SyntaxKind token)
        {
            return IsPrefixUnaryExpressionOperatorToken(token) ||
                   token == SyntaxKind.TrueKeyword ||
                   token == SyntaxKind.FalseKeyword;
        }

        public static bool IsAnyOverloadableOperator(SyntaxKind kind)
        {
            return IsOverloadableBinaryOperator(kind) || IsOverloadableUnaryOperator(kind);
        }

        public static bool IsOverloadableBinaryOperator(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.AsteriskToken:
                case SyntaxKind.SlashToken:
                case SyntaxKind.PercentToken:
                case SyntaxKind.CaretToken:
                case SyntaxKind.AmpersandToken:
                case SyntaxKind.BarToken:
                case SyntaxKind.EqualsEqualsToken:
                case SyntaxKind.LessThanToken:
                case SyntaxKind.LessThanEqualsToken:
                case SyntaxKind.LessThanLessThanToken:
                case SyntaxKind.GreaterThanToken:
                case SyntaxKind.GreaterThanEqualsToken:
                case SyntaxKind.GreaterThanGreaterThanToken:
                case SyntaxKind.ExclamationEqualsToken:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsOverloadableUnaryOperator(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.TildeToken:
                case SyntaxKind.ExclamationToken:
                case SyntaxKind.PlusPlusToken:
                case SyntaxKind.MinusMinusToken:
                case SyntaxKind.TrueKeyword:
                case SyntaxKind.FalseKeyword:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsPrimaryFunction(SyntaxKind keyword)
        {
            return GetPrimaryFunction(keyword) != SyntaxKind.None;
        }

        public static SyntaxKind GetPrimaryFunction(SyntaxKind keyword)
        {
            switch (keyword)
            {
                case SyntaxKind.MakeRefKeyword:
                    return SyntaxKind.MakeRefExpression;
                case SyntaxKind.RefTypeKeyword:
                    return SyntaxKind.RefTypeExpression;
                case SyntaxKind.RefValueKeyword:
                    return SyntaxKind.RefValueExpression;
                case SyntaxKind.CheckedKeyword:
                    return SyntaxKind.CheckedExpression;
                case SyntaxKind.UncheckedKeyword:
                    return SyntaxKind.UncheckedExpression;
                case SyntaxKind.DefaultKeyword:
                    return SyntaxKind.DefaultExpression;
                case SyntaxKind.TypeOfKeyword:
                    return SyntaxKind.TypeOfExpression;
                case SyntaxKind.SizeOfKeyword:
                    return SyntaxKind.SizeOfExpression;
                default:
                    return SyntaxKind.None;
            }
        }

        public static bool IsLiteralExpression(SyntaxKind token)
        {
            return GetLiteralExpression(token) != SyntaxKind.None;
        }

        public static SyntaxKind GetLiteralExpression(SyntaxKind token)
        {
            switch (token)
            {
                case SyntaxKind.StringLiteralToken:
                    return SyntaxKind.StringLiteralExpression;
                case SyntaxKind.CharacterLiteralToken:
                    return SyntaxKind.CharacterLiteralExpression;
                case SyntaxKind.NumericLiteralToken:
                    return SyntaxKind.NumericLiteralExpression;
                case SyntaxKind.NullKeyword:
                    return SyntaxKind.NullLiteralExpression;
                case SyntaxKind.TrueKeyword:
                    return SyntaxKind.TrueLiteralExpression;
                case SyntaxKind.FalseKeyword:
                    return SyntaxKind.FalseLiteralExpression;
                case SyntaxKind.ArgListKeyword:
                    return SyntaxKind.ArgListExpression;
                default:
                    return SyntaxKind.None;
            }
        }

        public static bool IsInstanceExpression(SyntaxKind token)
        {
            return GetInstanceExpression(token) != SyntaxKind.None;
        }

        public static SyntaxKind GetInstanceExpression(SyntaxKind token)
        {
            switch (token)
            {
                case SyntaxKind.ThisKeyword:
                    return SyntaxKind.ThisExpression;
                case SyntaxKind.BaseKeyword:
                    return SyntaxKind.BaseExpression;
                default:
                    return SyntaxKind.None;
            }
        }

        public static bool IsBinaryExpression(SyntaxKind token)
        {
            return GetBinaryExpression(token) != SyntaxKind.None;
        }

        public static bool IsBinaryExpressionOperatorToken(SyntaxKind token)
        {
            return GetBinaryExpression(token) != SyntaxKind.None;
        }

        public static SyntaxKind GetBinaryExpression(SyntaxKind token)
        {
            switch (token)
            {
                case SyntaxKind.QuestionQuestionToken:
                    return SyntaxKind.CoalesceExpression;
                case SyntaxKind.IsKeyword:
                    return SyntaxKind.IsExpression;
                case SyntaxKind.AsKeyword:
                    return SyntaxKind.AsExpression;
                case SyntaxKind.BarToken:
                    return SyntaxKind.BitwiseOrExpression;
                case SyntaxKind.CaretToken:
                    return SyntaxKind.ExclusiveOrExpression;
                case SyntaxKind.AmpersandToken:
                    return SyntaxKind.BitwiseAndExpression;
                case SyntaxKind.EqualsEqualsToken:
                    return SyntaxKind.EqualsExpression;
                case SyntaxKind.ExclamationEqualsToken:
                    return SyntaxKind.NotEqualsExpression;
                case SyntaxKind.LessThanToken:
                    return SyntaxKind.LessThanExpression;
                case SyntaxKind.LessThanEqualsToken:
                    return SyntaxKind.LessThanOrEqualExpression;
                case SyntaxKind.GreaterThanToken:
                    return SyntaxKind.GreaterThanExpression;
                case SyntaxKind.GreaterThanEqualsToken:
                    return SyntaxKind.GreaterThanOrEqualExpression;
                case SyntaxKind.LessThanLessThanToken:
                    return SyntaxKind.LeftShiftExpression;
                case SyntaxKind.GreaterThanGreaterThanToken:
                    return SyntaxKind.RightShiftExpression;
                case SyntaxKind.PlusToken:
                    return SyntaxKind.AddExpression;
                case SyntaxKind.MinusToken:
                    return SyntaxKind.SubtractExpression;
                case SyntaxKind.AsteriskToken:
                    return SyntaxKind.MultiplyExpression;
                case SyntaxKind.SlashToken:
                    return SyntaxKind.DivideExpression;
                case SyntaxKind.PercentToken:
                    return SyntaxKind.ModuloExpression;
                case SyntaxKind.AmpersandAmpersandToken:
                    return SyntaxKind.LogicalAndExpression;
                case SyntaxKind.BarBarToken:
                    return SyntaxKind.LogicalOrExpression;
                default:
                    return SyntaxKind.None;
            }
        }

        public static bool IsAssignmentExpression(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.OrAssignmentExpression:
                case SyntaxKind.AndAssignmentExpression:
                case SyntaxKind.ExclusiveOrAssignmentExpression:
                case SyntaxKind.LeftShiftAssignmentExpression:
                case SyntaxKind.RightShiftAssignmentExpression:
                case SyntaxKind.AddAssignmentExpression:
                case SyntaxKind.SubtractAssignmentExpression:
                case SyntaxKind.MultiplyAssignmentExpression:
                case SyntaxKind.DivideAssignmentExpression:
                case SyntaxKind.ModuloAssignmentExpression:
                case SyntaxKind.SimpleAssignmentExpression:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsAssignmentExpressionOperatorToken(SyntaxKind token)
        {
            switch (token)
            {
                case SyntaxKind.BarEqualsToken:
                case SyntaxKind.AmpersandEqualsToken:
                case SyntaxKind.CaretEqualsToken:
                case SyntaxKind.LessThanLessThanEqualsToken:
                case SyntaxKind.GreaterThanGreaterThanEqualsToken:
                case SyntaxKind.PlusEqualsToken:
                case SyntaxKind.MinusEqualsToken:
                case SyntaxKind.AsteriskEqualsToken:
                case SyntaxKind.SlashEqualsToken:
                case SyntaxKind.PercentEqualsToken:
                case SyntaxKind.EqualsToken:
                    return true;
                default:
                    return false;
            }
        }

        public static SyntaxKind GetAssignmentExpression(SyntaxKind token)
        {
            switch (token)
            {
                case SyntaxKind.BarEqualsToken:
                    return SyntaxKind.OrAssignmentExpression;
                case SyntaxKind.AmpersandEqualsToken:
                    return SyntaxKind.AndAssignmentExpression;
                case SyntaxKind.CaretEqualsToken:
                    return SyntaxKind.ExclusiveOrAssignmentExpression;
                case SyntaxKind.LessThanLessThanEqualsToken:
                    return SyntaxKind.LeftShiftAssignmentExpression;
                case SyntaxKind.GreaterThanGreaterThanEqualsToken:
                    return SyntaxKind.RightShiftAssignmentExpression;
                case SyntaxKind.PlusEqualsToken:
                    return SyntaxKind.AddAssignmentExpression;
                case SyntaxKind.MinusEqualsToken:
                    return SyntaxKind.SubtractAssignmentExpression;
                case SyntaxKind.AsteriskEqualsToken:
                    return SyntaxKind.MultiplyAssignmentExpression;
                case SyntaxKind.SlashEqualsToken:
                    return SyntaxKind.DivideAssignmentExpression;
                case SyntaxKind.PercentEqualsToken:
                    return SyntaxKind.ModuloAssignmentExpression;
                case SyntaxKind.EqualsToken:
                    return SyntaxKind.SimpleAssignmentExpression;
                default:
                    return SyntaxKind.None;
            }
        }

        public static SyntaxKind GetCheckStatement(SyntaxKind keyword)
        {
            switch (keyword)
            {
                case SyntaxKind.CheckedKeyword:
                    return SyntaxKind.CheckedStatement;
                case SyntaxKind.UncheckedKeyword:
                    return SyntaxKind.UncheckedStatement;
                default:
                    return SyntaxKind.None;
            }
        }

        public static SyntaxKind GetAccessorDeclarationKind(SyntaxKind keyword)
        {
            switch (keyword)
            {
                case SyntaxKind.GetKeyword:
                    return SyntaxKind.GetAccessorDeclaration;
                case SyntaxKind.SetKeyword:
                    return SyntaxKind.SetAccessorDeclaration;
                case SyntaxKind.AddKeyword:
                    return SyntaxKind.AddAccessorDeclaration;
                case SyntaxKind.RemoveKeyword:
                    return SyntaxKind.RemoveAccessorDeclaration;
                default:
                    return SyntaxKind.None;
            }
        }

        public static bool IsAccessorDeclaration(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsAccessorDeclarationKeyword(SyntaxKind keyword)
        {
            switch (keyword)
            {
                case SyntaxKind.GetKeyword:
                case SyntaxKind.SetKeyword:
                case SyntaxKind.AddKeyword:
                case SyntaxKind.RemoveKeyword:
                    return true;
                default:
                    return false;
            }
        }

        public static SyntaxKind GetSwitchLabelKind(SyntaxKind keyword)
        {
            switch (keyword)
            {
                case SyntaxKind.CaseKeyword:
                    return SyntaxKind.CaseSwitchLabel;
                case SyntaxKind.DefaultKeyword:
                    return SyntaxKind.DefaultSwitchLabel;
                default:
                    return SyntaxKind.None;
            }
        }

        public static SyntaxKind GetBaseTypeDeclarationKind(SyntaxKind kind)
        {
            return kind == SyntaxKind.EnumKeyword ? SyntaxKind.EnumDeclaration : GetTypeDeclarationKind(kind);
        }

        public static SyntaxKind GetTypeDeclarationKind(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.ClassKeyword:
                    return SyntaxKind.ClassDeclaration;
                case SyntaxKind.StructKeyword:
                    return SyntaxKind.StructDeclaration;
                case SyntaxKind.InterfaceKeyword:
                    return SyntaxKind.InterfaceDeclaration;
                default:
                    return SyntaxKind.None;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "bool":
                case "布":
                    return SyntaxKind.BoolKeyword;
                case "byte":
                case "字节":
                    return SyntaxKind.ByteKeyword;
                case "sbyte":
                case "符字节":
                    return SyntaxKind.SByteKeyword;
                case "short":
                case "短":
                    return SyntaxKind.ShortKeyword;
                case "ushort":
                case "无短":
                    return SyntaxKind.UShortKeyword;
                case "int":
                case "整":
                    return SyntaxKind.IntKeyword;
                case "uint":
                case "无整":
                    return SyntaxKind.UIntKeyword;
                case "long":
                case "长":
                    return SyntaxKind.LongKeyword;
                case "ulong":
                case "无长":
                    return SyntaxKind.ULongKeyword;
                case "double":
                case "双":
                    return SyntaxKind.DoubleKeyword;
                case "float":
                case "浮":
                    return SyntaxKind.FloatKeyword;
                case "decimal":
                case "十浮":
                    return SyntaxKind.DecimalKeyword;
                case "string":
                case "字符串":
                    return SyntaxKind.StringKeyword;
                case "char":
                case "字符":
                    return SyntaxKind.CharKeyword;
                case "void":
                case "无":
                    return SyntaxKind.VoidKeyword;
                case "object":
                case "对象":
                    return SyntaxKind.ObjectKeyword;
                case "typeof":
                case "类型":
                    return SyntaxKind.TypeOfKeyword;
                case "sizeof":
                case "大小":
                    return SyntaxKind.SizeOfKeyword;
                case "null":
                case "空":
                    return SyntaxKind.NullKeyword;
                case "true":
                case "真":
                    return SyntaxKind.TrueKeyword;
                case "false":
                case "假":
                    return SyntaxKind.FalseKeyword;
                case "if":
                case "若":
                    return SyntaxKind.IfKeyword;
                case "else":
                case "否则":
                    return SyntaxKind.ElseKeyword;
                case "while":
                case "当":
                    return SyntaxKind.WhileKeyword;
                case "for":
                case "对于":
                    return SyntaxKind.ForKeyword;
                case "foreach":
                case "对于每":
                    return SyntaxKind.ForEachKeyword;
                case "do":
                case "做":
                    return SyntaxKind.DoKeyword;
                case "switch":
                case "开关":
                    return SyntaxKind.SwitchKeyword;
                case "case":
                case "情形":
                    return SyntaxKind.CaseKeyword;
                case "default":
                case "默认":
                    return SyntaxKind.DefaultKeyword;
                case "lock":
                case "锁":
                    return SyntaxKind.LockKeyword;
                case "try":
                case "试":
                    return SyntaxKind.TryKeyword;
                case "throw":
                case "丢":
                    return SyntaxKind.ThrowKeyword;
                case "catch":
                case "接":
                    return SyntaxKind.CatchKeyword;
                case "finally":
                case "最后":
                    return SyntaxKind.FinallyKeyword;
                case "goto":
                case "转至":
                    return SyntaxKind.GotoKeyword;
                case "break":
                case "断":
                    return SyntaxKind.BreakKeyword;
                case "continue":
                case "继":
                    return SyntaxKind.ContinueKeyword;
                case "return":
                case "返回":
                    return SyntaxKind.ReturnKeyword;
                case "public":
                case "公":
                    return SyntaxKind.PublicKeyword;
                case "private":
                case "私":
                    return SyntaxKind.PrivateKeyword;
                case "internal":
                case "内":
                    return SyntaxKind.InternalKeyword;
                case "protected":
                case "护":
                    return SyntaxKind.ProtectedKeyword;
                case "static":
                case "静":
                    return SyntaxKind.StaticKeyword;
                case "readonly":
                case "只读":
                    return SyntaxKind.ReadOnlyKeyword;
                case "sealed":
                case "密封":
                    return SyntaxKind.SealedKeyword;
                case "const":
                case "常":
                    return SyntaxKind.ConstKeyword;
                case "fixed":
                case "固定":
                    return SyntaxKind.FixedKeyword;
                case "stackalloc":
                case "栈分配":
                    return SyntaxKind.StackAllocKeyword;
                case "volatile":
                case "易变":
                    return SyntaxKind.VolatileKeyword;
                case "new":
                case "新":
                    return SyntaxKind.NewKeyword;
                case "override":
                case "取代":
                    return SyntaxKind.OverrideKeyword;
                case "abstract":
                case "抽象":
                    return SyntaxKind.AbstractKeyword;
                case "virtual":
                case "虚":
                    return SyntaxKind.VirtualKeyword;
                case "event":
                case "事件":
                    return SyntaxKind.EventKeyword;
                case "extern":
                case "外":
                    return SyntaxKind.ExternKeyword;
                case "ref":
                case "引":
                    return SyntaxKind.RefKeyword;
                case "out":
                case "出":
                    return SyntaxKind.OutKeyword;
                case "in":
                case "入":
                case "于":
                    return SyntaxKind.InKeyword;
                case "is":
                case "是":
                    return SyntaxKind.IsKeyword;
                case "as":
                case "作为":
                    return SyntaxKind.AsKeyword;
                case "params":
                case "变参":
                    return SyntaxKind.ParamsKeyword;
                case "__arglist":
                    return SyntaxKind.ArgListKeyword;
                case "__makeref":
                    return SyntaxKind.MakeRefKeyword;
                case "__reftype":
                    return SyntaxKind.RefTypeKeyword;
                case "__refvalue":
                    return SyntaxKind.RefValueKeyword;
                case "this":
                case "此":
                    return SyntaxKind.ThisKeyword;
                case "base":
                case "基":
                    return SyntaxKind.BaseKeyword;
                case "namespace":
                case "命名空间":
                    return SyntaxKind.NamespaceKeyword;
                case "using":
                case "用":
                    return SyntaxKind.UsingKeyword;
                case "class":
                case "类":
                    return SyntaxKind.ClassKeyword;
                case "struct":
                case "结构":
                    return SyntaxKind.StructKeyword;
                case "interface":
                case "接口":
                    return SyntaxKind.InterfaceKeyword;
                case "enum":
                case "枚举":
                    return SyntaxKind.EnumKeyword;
                case "delegate":
                case "委托":
                    return SyntaxKind.DelegateKeyword;
                case "checked":
                case "检查":
                    return SyntaxKind.CheckedKeyword;
                case "unchecked":
                case "未检查":
                    return SyntaxKind.UncheckedKeyword;
                case "unsafe":
                case "不安全":
                    return SyntaxKind.UnsafeKeyword;
                case "operator":
                case "算符":
                    return SyntaxKind.OperatorKeyword;
                case "implicit":
                case "隐式":
                    return SyntaxKind.ImplicitKeyword;
                case "explicit":
                case "显式":
                    return SyntaxKind.ExplicitKeyword;
                default:
                    return SyntaxKind.None;
            }
        }

        public static SyntaxKind GetOperatorKind(string operatorMetadataName)
        {
            switch (operatorMetadataName)
            {
                case WellKnownMemberNames.AdditionOperatorName: return SyntaxKind.PlusToken;
                case WellKnownMemberNames.BitwiseAndOperatorName: return SyntaxKind.AmpersandToken;
                case WellKnownMemberNames.BitwiseOrOperatorName: return SyntaxKind.BarToken;
                // case WellKnownMemberNames.ConcatenateOperatorName:
                case WellKnownMemberNames.DecrementOperatorName: return SyntaxKind.MinusMinusToken;
                case WellKnownMemberNames.DivisionOperatorName: return SyntaxKind.SlashToken;
                case WellKnownMemberNames.EqualityOperatorName: return SyntaxKind.EqualsEqualsToken;
                case WellKnownMemberNames.ExclusiveOrOperatorName: return SyntaxKind.CaretToken;
                case WellKnownMemberNames.ExplicitConversionName: return SyntaxKind.ExplicitKeyword;
                // case WellKnownMemberNames.ExponentOperatorName:
                case WellKnownMemberNames.FalseOperatorName: return SyntaxKind.FalseKeyword;
                case WellKnownMemberNames.GreaterThanOperatorName: return SyntaxKind.GreaterThanToken;
                case WellKnownMemberNames.GreaterThanOrEqualOperatorName: return SyntaxKind.GreaterThanEqualsToken;
                case WellKnownMemberNames.ImplicitConversionName: return SyntaxKind.ImplicitKeyword;
                case WellKnownMemberNames.IncrementOperatorName: return SyntaxKind.PlusPlusToken;
                case WellKnownMemberNames.InequalityOperatorName: return SyntaxKind.ExclamationEqualsToken;
                //case WellKnownMemberNames.IntegerDivisionOperatorName: 
                case WellKnownMemberNames.LeftShiftOperatorName: return SyntaxKind.LessThanLessThanToken;
                case WellKnownMemberNames.LessThanOperatorName: return SyntaxKind.LessThanToken;
                case WellKnownMemberNames.LessThanOrEqualOperatorName: return SyntaxKind.LessThanEqualsToken;
                // case WellKnownMemberNames.LikeOperatorName:
                case WellKnownMemberNames.LogicalNotOperatorName: return SyntaxKind.ExclamationToken;
                case WellKnownMemberNames.ModulusOperatorName: return SyntaxKind.PercentToken;
                case WellKnownMemberNames.MultiplyOperatorName: return SyntaxKind.AsteriskToken;
                case WellKnownMemberNames.OnesComplementOperatorName: return SyntaxKind.TildeToken;
                case WellKnownMemberNames.RightShiftOperatorName: return SyntaxKind.GreaterThanGreaterThanToken;
                case WellKnownMemberNames.SubtractionOperatorName: return SyntaxKind.MinusToken;
                case WellKnownMemberNames.TrueOperatorName: return SyntaxKind.TrueKeyword;
                case WellKnownMemberNames.UnaryNegationOperatorName: return SyntaxKind.MinusToken;
                case WellKnownMemberNames.UnaryPlusOperatorName: return SyntaxKind.PlusToken;
                default:
                    return SyntaxKind.None;
            }
        }

        public static SyntaxKind GetPreprocessorKeywordKind(string text)
        {
            switch (text)
            {
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "false":
                    return SyntaxKind.FalseKeyword;
                case "default":
                    return SyntaxKind.DefaultKeyword;
                case "if":
                    return SyntaxKind.IfKeyword;
                case "else":
                    return SyntaxKind.ElseKeyword;
                case "elif":
                    return SyntaxKind.ElifKeyword;
                case "endif":
                    return SyntaxKind.EndIfKeyword;
                case "region":
                    return SyntaxKind.RegionKeyword;
                case "endregion":
                    return SyntaxKind.EndRegionKeyword;
                case "define":
                    return SyntaxKind.DefineKeyword;
                case "undef":
                    return SyntaxKind.UndefKeyword;
                case "warning":
                    return SyntaxKind.WarningKeyword;
                case "error":
                    return SyntaxKind.ErrorKeyword;
                case "line":
                    return SyntaxKind.LineKeyword;
                case "pragma":
                    return SyntaxKind.PragmaKeyword;
                case "hidden":
                    return SyntaxKind.HiddenKeyword;
                case "checksum":
                    return SyntaxKind.ChecksumKeyword;
                case "disable":
                    return SyntaxKind.DisableKeyword;
                case "restore":
                    return SyntaxKind.RestoreKeyword;
                case "r":
                    return SyntaxKind.ReferenceKeyword;
                case "load":
                    return SyntaxKind.LoadKeyword;
                default:
                    return SyntaxKind.None;
            }
        }

        public static IEnumerable<SyntaxKind> GetContextualKeywordKinds()
        {
            for (int i = (int)SyntaxKind.YieldKeyword; i <= (int)SyntaxKind.WhenKeyword; i++)
            {
                yield return (SyntaxKind)i;
            }
        }

        public static bool IsContextualKeyword(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.YieldKeyword:
                case SyntaxKind.PartialKeyword:
                case SyntaxKind.FromKeyword:
                case SyntaxKind.GroupKeyword:
                case SyntaxKind.JoinKeyword:
                case SyntaxKind.IntoKeyword:
                case SyntaxKind.LetKeyword:
                case SyntaxKind.ByKeyword:
                case SyntaxKind.WhereKeyword:
                case SyntaxKind.SelectKeyword:
                case SyntaxKind.GetKeyword:
                case SyntaxKind.SetKeyword:
                case SyntaxKind.AddKeyword:
                case SyntaxKind.RemoveKeyword:
                case SyntaxKind.OrderByKeyword:
                case SyntaxKind.AliasKeyword:
                case SyntaxKind.OnKeyword:
                case SyntaxKind.EqualsKeyword:
                case SyntaxKind.AscendingKeyword:
                case SyntaxKind.DescendingKeyword:
                case SyntaxKind.AssemblyKeyword:
                case SyntaxKind.ModuleKeyword:
                case SyntaxKind.TypeKeyword:
                case SyntaxKind.GlobalKeyword:
                case SyntaxKind.FieldKeyword:
                case SyntaxKind.MethodKeyword:
                case SyntaxKind.ParamKeyword:
                case SyntaxKind.PropertyKeyword:
                case SyntaxKind.TypeVarKeyword:
                case SyntaxKind.NameOfKeyword:
                case SyntaxKind.AsyncKeyword:
                case SyntaxKind.AwaitKeyword:
                case SyntaxKind.WhenKeyword:
                case SyntaxKind.UnderscoreToken:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsQueryContextualKeyword(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.FromKeyword:
                case SyntaxKind.WhereKeyword:
                case SyntaxKind.SelectKeyword:
                case SyntaxKind.GroupKeyword:
                case SyntaxKind.IntoKeyword:
                case SyntaxKind.OrderByKeyword:
                case SyntaxKind.JoinKeyword:
                case SyntaxKind.LetKeyword:
                case SyntaxKind.OnKeyword:
                case SyntaxKind.EqualsKeyword:
                case SyntaxKind.ByKeyword:
                case SyntaxKind.AscendingKeyword:
                case SyntaxKind.DescendingKeyword:
                    return true;
                default:
                    return false;
            }
        }

        public static SyntaxKind GetContextualKeywordKind(string text)
        {
            switch (text)
            {
                case "yield":
                case "生成":
                    return SyntaxKind.YieldKeyword;
                case "partial":
                case "部分":
                    return SyntaxKind.PartialKeyword;
                case "from":
                case "从":
                    return SyntaxKind.FromKeyword;
                case "group":
                case "分组":
                    return SyntaxKind.GroupKeyword;
                case "join":
                case "加入":
                    return SyntaxKind.JoinKeyword;
                case "into":
                case "至":
                    return SyntaxKind.IntoKeyword;
                case "let":
                case "令":
                    return SyntaxKind.LetKeyword;
                case "by":
                case "以":
                    return SyntaxKind.ByKeyword;
                case "where":
                case "其中":
                    return SyntaxKind.WhereKeyword;
                case "select":
                case "选出":
                    return SyntaxKind.SelectKeyword;
                case "get":
                case "取":
                    return SyntaxKind.GetKeyword;
                case "set":
                case "设":
                    return SyntaxKind.SetKeyword;
                case "add":
                case "添加":
                    return SyntaxKind.AddKeyword;
                case "remove":
                case "删除":
                    return SyntaxKind.RemoveKeyword;
                case "orderby":
                case "排序":
                    return SyntaxKind.OrderByKeyword;
                case "alias":
                case "别名":
                    return SyntaxKind.AliasKeyword;
                case "on":
                case "在":
                    return SyntaxKind.OnKeyword;
                case "equals":
                case "等于":
                    return SyntaxKind.EqualsKeyword;
                case "ascending":
                case "升序":
                    return SyntaxKind.AscendingKeyword;
                case "descending":
                case "降序":
                    return SyntaxKind.DescendingKeyword;
                case "assembly":
                case "程序集":
                    return SyntaxKind.AssemblyKeyword;
                case "module":
                case "模块":
                    return SyntaxKind.ModuleKeyword;
                case "type":
                case "类型":
                    return SyntaxKind.TypeKeyword;
                case "field":
                case "字段":
                    return SyntaxKind.FieldKeyword;
                case "method":
                case "方法":
                    return SyntaxKind.MethodKeyword;
                case "param":
                case "参数":
                    return SyntaxKind.ParamKeyword;
                case "property":
                case "属性":
                    return SyntaxKind.PropertyKeyword;
                case "typevar":
                case "类型参数":
                    return SyntaxKind.TypeVarKeyword;
                case "global":
                case "全局":
                    return SyntaxKind.GlobalKeyword;
                case "async":
                case "异步":
                    return SyntaxKind.AsyncKeyword;
                case "await":
                case "等候":
                    return SyntaxKind.AwaitKeyword;
                case "when":
                case "若有":
                    return SyntaxKind.WhenKeyword;
                case "nameof":
                case "名称":
                    return SyntaxKind.NameOfKeyword;
                case "_":
                    return SyntaxKind.UnderscoreToken;
                default:
                    return SyntaxKind.None;
            }
        }

        public static string GetText(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.TildeToken:
                    return "~";
                case SyntaxKind.ExclamationToken:
                    return "!";
                case SyntaxKind.DollarToken:
                    return "$";
                case SyntaxKind.PercentToken:
                    return "%";
                case SyntaxKind.CaretToken:
                    return "^";
                case SyntaxKind.AmpersandToken:
                    return "&";
                case SyntaxKind.AsteriskToken:
                    return "*";
                case SyntaxKind.OpenParenToken:
                    return "(";
                case SyntaxKind.CloseParenToken:
                    return ")";
                case SyntaxKind.MinusToken:
                    return "-";
                case SyntaxKind.PlusToken:
                    return "+";
                case SyntaxKind.EqualsToken:
                    return "=";
                case SyntaxKind.OpenBraceToken:
                    return "{";
                case SyntaxKind.CloseBraceToken:
                    return "}";
                case SyntaxKind.OpenBracketToken:
                    return "[";
                case SyntaxKind.CloseBracketToken:
                    return "]";
                case SyntaxKind.BarToken:
                    return "|";
                case SyntaxKind.BackslashToken:
                    return "\\";
                case SyntaxKind.ColonToken:
                    return ":";
                case SyntaxKind.SemicolonToken:
                    return ";";
                case SyntaxKind.DoubleQuoteToken:
                    return "\"";
                case SyntaxKind.SingleQuoteToken:
                    return "'";
                case SyntaxKind.LessThanToken:
                    return "<";
                case SyntaxKind.CommaToken:
                    return ",";
                case SyntaxKind.GreaterThanToken:
                    return ">";
                case SyntaxKind.DotToken:
                    return ".";
                case SyntaxKind.QuestionToken:
                    return "?";
                case SyntaxKind.HashToken:
                    return "#";
                case SyntaxKind.SlashToken:
                    return "/";
                case SyntaxKind.SlashGreaterThanToken:
                    return "/>";
                case SyntaxKind.LessThanSlashToken:
                    return "</";
                case SyntaxKind.XmlCommentStartToken:
                    return "<!--";
                case SyntaxKind.XmlCommentEndToken:
                    return "-->";
                case SyntaxKind.XmlCDataStartToken:
                    return "<![CDATA[";
                case SyntaxKind.XmlCDataEndToken:
                    return "]]>";
                case SyntaxKind.XmlProcessingInstructionStartToken:
                    return "<?";
                case SyntaxKind.XmlProcessingInstructionEndToken:
                    return "?>";

                // compound
                case SyntaxKind.BarBarToken:
                    return "||";
                case SyntaxKind.AmpersandAmpersandToken:
                    return "&&";
                case SyntaxKind.MinusMinusToken:
                    return "--";
                case SyntaxKind.PlusPlusToken:
                    return "++";
                case SyntaxKind.ColonColonToken:
                    return "::";
                case SyntaxKind.QuestionQuestionToken:
                    return "??";
                case SyntaxKind.MinusGreaterThanToken:
                    return "->";
                case SyntaxKind.ExclamationEqualsToken:
                    return "!=";
                case SyntaxKind.EqualsEqualsToken:
                    return "==";
                case SyntaxKind.EqualsGreaterThanToken:
                    return "=>";
                case SyntaxKind.LessThanEqualsToken:
                    return "<=";
                case SyntaxKind.LessThanLessThanToken:
                    return "<<";
                case SyntaxKind.LessThanLessThanEqualsToken:
                    return "<<=";
                case SyntaxKind.GreaterThanEqualsToken:
                    return ">=";
                case SyntaxKind.GreaterThanGreaterThanToken:
                    return ">>";
                case SyntaxKind.GreaterThanGreaterThanEqualsToken:
                    return ">>=";
                case SyntaxKind.SlashEqualsToken:
                    return "/=";
                case SyntaxKind.AsteriskEqualsToken:
                    return "*=";
                case SyntaxKind.BarEqualsToken:
                    return "|=";
                case SyntaxKind.AmpersandEqualsToken:
                    return "&=";
                case SyntaxKind.PlusEqualsToken:
                    return "+=";
                case SyntaxKind.MinusEqualsToken:
                    return "-=";
                case SyntaxKind.CaretEqualsToken:
                    return "^=";
                case SyntaxKind.PercentEqualsToken:
                    return "%=";

                // Keywords
                case SyntaxKind.BoolKeyword:
                    return "bool";
                case SyntaxKind.ByteKeyword:
                    return "byte";
                case SyntaxKind.SByteKeyword:
                    return "sbyte";
                case SyntaxKind.ShortKeyword:
                    return "short";
                case SyntaxKind.UShortKeyword:
                    return "ushort";
                case SyntaxKind.IntKeyword:
                    return "int";
                case SyntaxKind.UIntKeyword:
                    return "uint";
                case SyntaxKind.LongKeyword:
                    return "long";
                case SyntaxKind.ULongKeyword:
                    return "ulong";
                case SyntaxKind.DoubleKeyword:
                    return "double";
                case SyntaxKind.FloatKeyword:
                    return "float";
                case SyntaxKind.DecimalKeyword:
                    return "decimal";
                case SyntaxKind.StringKeyword:
                    return "string";
                case SyntaxKind.CharKeyword:
                    return "char";
                case SyntaxKind.VoidKeyword:
                    return "void";
                case SyntaxKind.ObjectKeyword:
                    return "object";
                case SyntaxKind.TypeOfKeyword:
                    return "typeof";
                case SyntaxKind.SizeOfKeyword:
                    return "sizeof";
                case SyntaxKind.NullKeyword:
                    return "null";
                case SyntaxKind.TrueKeyword:
                    return "true";
                case SyntaxKind.FalseKeyword:
                    return "false";
                case SyntaxKind.IfKeyword:
                    return "if";
                case SyntaxKind.ElseKeyword:
                    return "else";
                case SyntaxKind.WhileKeyword:
                    return "while";
                case SyntaxKind.ForKeyword:
                    return "for";
                case SyntaxKind.ForEachKeyword:
                    return "foreach";
                case SyntaxKind.DoKeyword:
                    return "do";
                case SyntaxKind.SwitchKeyword:
                    return "switch";
                case SyntaxKind.CaseKeyword:
                    return "case";
                case SyntaxKind.DefaultKeyword:
                    return "default";
                case SyntaxKind.TryKeyword:
                    return "try";
                case SyntaxKind.CatchKeyword:
                    return "catch";
                case SyntaxKind.FinallyKeyword:
                    return "finally";
                case SyntaxKind.LockKeyword:
                    return "lock";
                case SyntaxKind.GotoKeyword:
                    return "goto";
                case SyntaxKind.BreakKeyword:
                    return "break";
                case SyntaxKind.ContinueKeyword:
                    return "continue";
                case SyntaxKind.ReturnKeyword:
                    return "return";
                case SyntaxKind.ThrowKeyword:
                    return "throw";
                case SyntaxKind.PublicKeyword:
                    return "public";
                case SyntaxKind.PrivateKeyword:
                    return "private";
                case SyntaxKind.InternalKeyword:
                    return "internal";
                case SyntaxKind.ProtectedKeyword:
                    return "protected";
                case SyntaxKind.StaticKeyword:
                    return "static";
                case SyntaxKind.ReadOnlyKeyword:
                    return "readonly";
                case SyntaxKind.SealedKeyword:
                    return "sealed";
                case SyntaxKind.ConstKeyword:
                    return "const";
                case SyntaxKind.FixedKeyword:
                    return "fixed";
                case SyntaxKind.StackAllocKeyword:
                    return "stackalloc";
                case SyntaxKind.VolatileKeyword:
                    return "volatile";
                case SyntaxKind.NewKeyword:
                    return "new";
                case SyntaxKind.OverrideKeyword:
                    return "override";
                case SyntaxKind.AbstractKeyword:
                    return "abstract";
                case SyntaxKind.VirtualKeyword:
                    return "virtual";
                case SyntaxKind.EventKeyword:
                    return "event";
                case SyntaxKind.ExternKeyword:
                    return "extern";
                case SyntaxKind.RefKeyword:
                    return "ref";
                case SyntaxKind.OutKeyword:
                    return "out";
                case SyntaxKind.InKeyword:
                    return "in";
                case SyntaxKind.IsKeyword:
                    return "is";
                case SyntaxKind.AsKeyword:
                    return "as";
                case SyntaxKind.ParamsKeyword:
                    return "params";
                case SyntaxKind.ArgListKeyword:
                    return "__arglist";
                case SyntaxKind.MakeRefKeyword:
                    return "__makeref";
                case SyntaxKind.RefTypeKeyword:
                    return "__reftype";
                case SyntaxKind.RefValueKeyword:
                    return "__refvalue";
                case SyntaxKind.ThisKeyword:
                    return "this";
                case SyntaxKind.BaseKeyword:
                    return "base";
                case SyntaxKind.NamespaceKeyword:
                    return "namespace";
                case SyntaxKind.UsingKeyword:
                    return "using";
                case SyntaxKind.ClassKeyword:
                    return "class";
                case SyntaxKind.StructKeyword:
                    return "struct";
                case SyntaxKind.InterfaceKeyword:
                    return "interface";
                case SyntaxKind.EnumKeyword:
                    return "enum";
                case SyntaxKind.DelegateKeyword:
                    return "delegate";
                case SyntaxKind.CheckedKeyword:
                    return "checked";
                case SyntaxKind.UncheckedKeyword:
                    return "unchecked";
                case SyntaxKind.UnsafeKeyword:
                    return "unsafe";
                case SyntaxKind.OperatorKeyword:
                    return "operator";
                case SyntaxKind.ImplicitKeyword:
                    return "implicit";
                case SyntaxKind.ExplicitKeyword:
                    return "explicit";
                case SyntaxKind.ElifKeyword:
                    return "elif";
                case SyntaxKind.EndIfKeyword:
                    return "endif";
                case SyntaxKind.RegionKeyword:
                    return "region";
                case SyntaxKind.EndRegionKeyword:
                    return "endregion";
                case SyntaxKind.DefineKeyword:
                    return "define";
                case SyntaxKind.UndefKeyword:
                    return "undef";
                case SyntaxKind.WarningKeyword:
                    return "warning";
                case SyntaxKind.ErrorKeyword:
                    return "error";
                case SyntaxKind.LineKeyword:
                    return "line";
                case SyntaxKind.PragmaKeyword:
                    return "pragma";
                case SyntaxKind.HiddenKeyword:
                    return "hidden";
                case SyntaxKind.ChecksumKeyword:
                    return "checksum";
                case SyntaxKind.DisableKeyword:
                    return "disable";
                case SyntaxKind.RestoreKeyword:
                    return "restore";
                case SyntaxKind.ReferenceKeyword:
                    return "r";
                case SyntaxKind.LoadKeyword:
                    return "load";

                // contextual keywords
                case SyntaxKind.YieldKeyword:
                    return "yield";
                case SyntaxKind.PartialKeyword:
                    return "partial";
                case SyntaxKind.FromKeyword:
                    return "from";
                case SyntaxKind.GroupKeyword:
                    return "group";
                case SyntaxKind.JoinKeyword:
                    return "join";
                case SyntaxKind.IntoKeyword:
                    return "into";
                case SyntaxKind.LetKeyword:
                    return "let";
                case SyntaxKind.ByKeyword:
                    return "by";
                case SyntaxKind.WhereKeyword:
                    return "where";
                case SyntaxKind.SelectKeyword:
                    return "select";
                case SyntaxKind.GetKeyword:
                    return "get";
                case SyntaxKind.SetKeyword:
                    return "set";
                case SyntaxKind.AddKeyword:
                    return "add";
                case SyntaxKind.RemoveKeyword:
                    return "remove";
                case SyntaxKind.OrderByKeyword:
                    return "orderby";
                case SyntaxKind.AliasKeyword:
                    return "alias";
                case SyntaxKind.OnKeyword:
                    return "on";
                case SyntaxKind.EqualsKeyword:
                    return "equals";
                case SyntaxKind.AscendingKeyword:
                    return "ascending";
                case SyntaxKind.DescendingKeyword:
                    return "descending";
                case SyntaxKind.AssemblyKeyword:
                    return "assembly";
                case SyntaxKind.ModuleKeyword:
                    return "module";
                case SyntaxKind.TypeKeyword:
                    return "type";
                case SyntaxKind.FieldKeyword:
                    return "field";
                case SyntaxKind.MethodKeyword:
                    return "method";
                case SyntaxKind.ParamKeyword:
                    return "param";
                case SyntaxKind.PropertyKeyword:
                    return "property";
                case SyntaxKind.TypeVarKeyword:
                    return "typevar";
                case SyntaxKind.GlobalKeyword:
                    return "global";
                case SyntaxKind.NameOfKeyword:
                    return "nameof";
                case SyntaxKind.AsyncKeyword:
                    return "async";
                case SyntaxKind.AwaitKeyword:
                    return "await";
                case SyntaxKind.WhenKeyword:
                    return "when";
                case SyntaxKind.InterpolatedVerbatimStringStartToken:
                    return "$@\"";
                case SyntaxKind.InterpolatedStringStartToken:
                    return "$\"";
                case SyntaxKind.InterpolatedStringEndToken:
                    return "\"";
                case SyntaxKind.UnderscoreToken:
                    return "_";
                default:
                    return string.Empty;
            }
        }

        public static bool IsTypeParameterVarianceKeyword(SyntaxKind kind)
        {
            return kind == SyntaxKind.OutKeyword || kind == SyntaxKind.InKeyword;
        }

        public static bool IsDocumentationCommentTrivia(SyntaxKind kind)
        {
            return kind == SyntaxKind.SingleLineDocumentationCommentTrivia ||
                kind == SyntaxKind.MultiLineDocumentationCommentTrivia;
        }
    }
}
