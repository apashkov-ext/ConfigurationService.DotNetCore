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
                public const string Incorrect = "Dev";
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
        }
    }
}
