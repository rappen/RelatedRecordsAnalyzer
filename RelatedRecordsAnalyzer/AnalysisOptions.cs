﻿using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

namespace Rappen.XTB.RRA
{
    public class AnalysisOptions
    {
        internal EntityMetadataProxy Parent;
        public bool IsActivity { get; set; } = true;
        public bool IsNotValid4AF { get; set; } = false;
        public bool IsBPF { get; set; } = false;
        public bool IsPrivate { get; set; } = false;
        public bool Hidden { get; set; } = true;
        public bool OnlyData { get; set; } = true;
        public bool Regarding { get; set; } = true;
        public bool M2M { get; set; } = true;
        public bool UserOwned { get; set; } = true;
        public bool UserCreated { get; set; } = true;
        public bool UserModified { get; set; } = true;
        public bool UserOnBehalf { get; set; } = false;
        public List<CascadeType> AssignTypes { get; set; } = new List<CascadeType>();
        public List<CascadeType> ShareTypes { get; set; } = new List<CascadeType>();
        public List<CascadeType> DeleteTypes { get; set; } = new List<CascadeType>();

        public AnalysisOptions()
        {
        }

        public AnalysisOptions(EntityMetadataProxy parent)
        {
            Parent = parent;
        }
    }
}