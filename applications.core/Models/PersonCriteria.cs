namespace Applications.Core.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class PersonCriteria : BaseModel, IODataCriteria
    {
        public string[] NetworkIds { get; set; }

        public string[] TimekeeperIds { get; set; }

        public string Keyword { get; set; }

        public bool CurrentOnly { get; set; }

        public bool PartnerOnly { get; set; }

        public string[] DepartmentCodes { get; set; }

        public string[] PositionNames { get; set; }

        public ProfileTypes[] ProfileTypes { get; set; }

        public string GetServiceUrl()
        {
            var filter = new List<string>();

            var idFilter = new List<string>();
            if (NetworkIds != null && NetworkIds.Length > 0)
            {
                var networkIdFilter = NetworkIds.Where(n => !string.IsNullOrWhiteSpace((n ?? string.Empty))).Select(n => string.Format("toupper(NetworkID) eq '{0}'", n.ToUpperInvariant().Replace("'", "''"))).ToArray();
                if ((networkIdFilter?.Length ?? 0) > 0)
                {
                    idFilter.AddRange(networkIdFilter);
                    //filter.Add(string.Format("({0})", string.Join(" or ", networkIdFilter)));
                }
            }

            if (TimekeeperIds != null && TimekeeperIds.Length > 0)
            {
                var timekeeperIdFilter = TimekeeperIds.Where(n => !string.IsNullOrWhiteSpace((n ?? string.Empty))).Select(n => string.Format("toupper(TimekeeperID) eq '{0}'", n.ToUpperInvariant().Replace("'", "''"))).ToArray();
                if ((timekeeperIdFilter?.Length ?? 0) > 0)
                {
                    idFilter.AddRange(timekeeperIdFilter);
                    //filter.Add(string.Format("({0})", string.Join(" or ", timekeeperIdFilter)));
                }
            }

            if ((idFilter?.Count ?? 0) > 0)
            {
                filter.Add(string.Format("({0})", string.Join(" or ", idFilter)));
            }

            if (DepartmentCodes != null && DepartmentCodes.Length > 0)
            {
                var departmentCodeFilter = DepartmentCodes.Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => string.Format("toupper(DepartmentCode) eq '{0}'", n.ToUpperInvariant().Replace("'", "''")));

                filter.Add(string.Format("({0})", string.Join(" or ", departmentCodeFilter)));
            }

            if (PositionNames != null && PositionNames.Length > 0)
            {
                var positionNameFilter = PositionNames.Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => string.Format("toupper(PositionName) eq '{0}'", n.ToUpperInvariant().Replace("'", "''")));

                filter.Add(string.Format("({0})", string.Join(" or ", positionNameFilter)));
            }

            if (CurrentOnly)
            {
                filter.Add("EmploymentStatusID eq 1 ");
            }

            if (PartnerOnly)
            {
                filter.Add("Partner eq TimekeeperID");
            }

            if (ProfileTypes != null && ProfileTypes.Length > 0)
            {
                var profileTypeFilter = ProfileTypes.Select(n => string.Format("EmpTypeID eq {0}", (int)n));

                filter.Add(string.Format("({0})", string.Join(" or ", profileTypeFilter)));
            }
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                string escapedString = Keyword.ToUpperInvariant().Replace("'", "''");
                string[] fieldsToPerformKeywordSearch = { "FirstName", "LastName", "MiddleName", "OfficeLocationName", "NetworkID", "TimekeeperID" };
                var keywordFilter = fieldsToPerformKeywordSearch.Select(f => string.Format("indexof(toupper({0}), '{1}') gt -1", f, escapedString));

                filter.Add(string.Format("({0})", string.Join(" or ", keywordFilter)));
            }

            return filter != null && filter.Count > 0 ? string.Join(" and ", filter) : null;
        }
    }
}