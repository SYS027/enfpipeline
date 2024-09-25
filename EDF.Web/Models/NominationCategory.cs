using System;
using System.Collections.Generic;

namespace ENF.Models
{
    public class NomineeEntry
    {
        public string RelationshipCode { get; set; }
        public string RelationshipName { get; set; }
        public string flag { get; set; }
        public int FamilyId { get; set; } // Corrected to match casing convention
        public float Percentage { get; set; }
        public string guardianName { get; set; }
    }

    public class NominationCategory
    {
        public string NominationId { get; set; }

        public List<NomineeEntry> Entries { get; set; }
    }
}
