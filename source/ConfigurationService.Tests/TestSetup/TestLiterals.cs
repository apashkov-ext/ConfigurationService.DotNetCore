using System;

namespace ConfigurationService.Tests.TestSetup
{
    internal static class TestLiterals
    {
        public static class Project
        {
            public static class Name
            {
                public const string Correct = "TestProject";
                public const string Incorrect = "_Test_Project";
            }

            public static class ApiKeys
            {
                /// <summary>
                /// d1509252-0769-4119-b6cb-7e7dff351384
                /// </summary>
                public static readonly Guid Correct = new Guid("d1509252-0769-4119-b6cb-7e7dff351384");
            }
        }

        public static class Environment
        {
            public static class Name 
            {
                public const string Correct = "Dev";
                public const string Correct2 = "Last";
                public const string Incorrect = "_dev";
            }
        }

        public static class OptionGroup
        {
            public static class Name
            {
                public const string Correct = "Validation";
                public const string Incorrect = "_validation";
            }

            public static class Description
            {
                public const string Correct = "Option group description";
            }
        }

        public static class Option
        {
            public static class Name
            {
                public const string Correct = "Enabled";
                public const string Incorrect = "_enabled";
            }

            public static class Description
            {
                public const string Correct = "Option description";
            }

            public static class Value
            {
                public const string StringValue = "Value";
            }
        }

        public static class Guids
        {
            /// <summary>
            /// b6e7462d-48f6-4a5d-87f5-f899fa4217ec
            /// </summary>
            public static readonly Guid Id1 = new Guid("b6e7462d-48f6-4a5d-87f5-f899fa4217ec");
            /// <summary>
            /// 56bca498-705f-4764-9122-066f82bd73ff
            /// </summary>
            public static readonly Guid Id2 = new Guid("56bca498-705f-4764-9122-066f82bd73ff");

            /// <summary>
            /// 0c9d93ea-ee7a-4106-b254-db5693d26587
            /// </summary>
            public static readonly Guid Id3 = new Guid("0c9d93ea-ee7a-4106-b254-db5693d26587");

            /// <summary>
            /// 13c1d5ac-56f6-4eb5-b21b-0644afbfbe01
            /// </summary>
            public static readonly Guid Id4 = new Guid("13c1d5ac-56f6-4eb5-b21b-0644afbfbe01");
        }
    }
}
