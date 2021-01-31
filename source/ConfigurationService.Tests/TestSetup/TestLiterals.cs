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
        }
    }
}
