using System;
using System.Collections.Generic;
using System.Text;

namespace CounterIntelligenceCommand.Domain.Core
{
    public static class Operation
    {
        public static bool Successful = true;
        public static bool Failed = false;

        public static string UpdateStaffSuccessfully = "staff {0} with number {1} has been successfully updated";

        public static string AddStaffSuccessfully = "staff {0} with number {1} has been successfully saved";

        public static string StaffExists = "staff with number {0} already exists";

        public static string NotFFound = "{0} not found";

        public static string AddStaffFailed = "error occured while saving staff {0}";
    }

}
