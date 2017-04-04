using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace TicTacToe.IntegrationTests.Core
{
    /// <summary>
    ///     <para>
    ///         Marks a test method as being a data theory. Data theories are tests which
    ///         are fed various bits of data from a data source, mapping to parameters on
    ///         the test method.
    ///         If the data source contains multiple rows, then the test method is executed
    ///         multiple times (once with each data row). Data is provided by attributes which
    ///         derive from <see cref="Xunit.Sdk.DataAttribute"/> (notably,
    ///         <see cref="InlineDataAttribute"/> and <see cref="Xunit.MemberDataAttribute"/>).
    ///     </para>
    ///     <para>
    ///         The name of the method will be used as the test method's
    ///         <see cref="Xunit.FactAttribute.DisplayName"/> after being reformatted by
    ///         replacing specific characters in the method's name with other characters.
    ///     </para>
    /// </summary>
    [Xunit.Sdk.XunitTestCaseDiscoverer("Xunit.Sdk.TheoryDiscoverer", "xunit.execution.{Platform}")]
    public class TheoryWithAutomaticDisplayNameAttribute : FactWithAutomaticDisplayNameAttribute
    {
        /// <summary>
        ///     <para>
        ///         Marks a test method as being a data theory. Data theories are tests which
        ///         are fed various bits of data from a data source, mapping to parameters on
        ///         the test method.
        ///         If the data source contains multiple rows, then the test method is executed
        ///         multiple times (once with each data row). Data is provided by attributes which
        ///         derive from <see cref="Xunit.Sdk.DataAttribute"/> (notably,
        ///         <see cref="InlineDataAttribute"/> and <see cref="Xunit.MemberDataAttribute"/>).
        ///     </para>
        ///     <para>
        ///         The name of the method will be used as the test method's
        ///         <see cref="Xunit.FactAttribute.DisplayName"/> after being reformatted by
        ///         replacing specific characters in the method's name with other characters.
        ///     </para>
        /// </summary>
        /// <param name="charsToReplace">
        ///     A <see cref="string"/> containing the characters
        ///     to replace in the test method's name (e.g. "_").
        /// </param>
        /// <param name="replacementChars">
        ///     A <see cref="string"/> containing the characters (e.g. " ") that will
        ///     replace those specified by the <paramref name="charsToReplace"/> parameter
        ///     that are found in the test method's name.
        /// </param>
        /// <param name="testMethodName">
        ///     This is automatically set to the name of the current method;
        ///     there's no need to set a value for this parameter.
        /// </param>
        public TheoryWithAutomaticDisplayNameAttribute(string charsToReplace = "_",
                                                       string replacementChars = " ",
                                                       [CallerMemberName] string testMethodName = "")
            : base(charsToReplace, replacementChars, testMethodName) { }
    }
}
